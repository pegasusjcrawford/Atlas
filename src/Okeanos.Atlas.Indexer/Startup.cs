using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Okeanos.Atlas.Indexer.Client;
using Okeanos.Atlas.Indexer.Crypto;
using Okeanos.Atlas.Indexer.Extensions;
using Okeanos.Atlas.Indexer.Handlers;
using Okeanos.Atlas.Indexer.Operations;
using Okeanos.Atlas.Indexer.Operations.Types;
using Okeanos.Atlas.Indexer.Paging;
using Okeanos.Atlas.Indexer.Settings;
using Okeanos.Atlas.Indexer.Storage;
using Okeanos.Atlas.Indexer.Storage.Mongo;
using Okeanos.Atlas.Indexer.Sync;
using Okeanos.Atlas.Indexer.Sync.SyncTasks;
using Okeanos.Chain.Utilities;
using ConcurrentCollections;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Okeanos.Atlas.Indexer
{
   public class Startup
   {
      public static void AddIndexerServices(IServiceCollection services, IConfiguration configuration)
      {
         services.Configure<ChainSettings>(configuration.GetSection("Chain"));
         services.Configure<NetworkSettings>(configuration.GetSection("Network"));
         services.Configure<IndexerSettings>(configuration.GetSection("Indexer"));
         services.Configure<InsightSettings>(configuration.GetSection("Insight"));


         services.AddSingleton(_ =>
         {
            var indexerConfiguration = _.GetService(typeof(IOptions<IndexerSettings>))as IOptions<IndexerSettings> ;// configuration.GetSection("Indexer") as IndexerSettings;
            var chainConfiguration  = _.GetService(typeof(IOptions<ChainSettings>)) as IOptions<ChainSettings>;//  configuration.GetSection("Chain") as ChainSettings;

            var mongoClient = new MongoClient(indexerConfiguration.Value.ConnectionString.Replace("{Symbol}",
               chainConfiguration.Value.Symbol.ToLower()));

            string dbName = indexerConfiguration.Value.DatabaseNameSubfix
               ? $"Blockchain{chainConfiguration.Value.Symbol}"
               : "Blockchain";

            return mongoClient.GetDatabase(dbName);
         });

         // services.AddSingleton<QueryHandler>();
         services.AddSingleton<StatsHandler>();
         services.AddSingleton<CommandHandler>();
         services.AddSingleton<IStorage, MongoData>();
         services.AddSingleton<IMongoDb, MongoDb>();
         services.AddSingleton<IUtxoCache, UtxoCache>();
         services.AddSingleton<IStorageOperations, MongoStorageOperations>();
         services.AddSingleton<TaskStarter, MongoBuilder>();
         services.AddTransient<SyncServer>();
         services.AddSingleton<SyncConnection>();
         services.AddSingleton<ISyncOperations, SyncOperations>();
         services.AddSingleton<IPagingHelper, PagingHelper>();
         services.AddScoped<Runner>();

         services.AddSingleton<GlobalState>();
         services.AddSingleton<IScriptInterpeter, ScriptToAddressParser>();


         services.AddScoped<TaskRunner, MempoolPuller>();
         services.AddScoped<TaskRunner, Notifier>();
         services.AddScoped<TaskRunner, StatsSyncer>(); // Update peer information every 5 minute.

         services.AddScoped<TaskRunner, BlockPuller>();
         services.AddScoped<TaskRunner, BlockStore>();
         services.AddScoped<TaskStarter, BlockStartup>();

         services.AddScoped<TaskRunner, BlockIndexer>();
         services.AddScoped<TaskRunner, RichListSync>();

         // TODO: Verify that it is OK we add this to shared Startup for Blockcore and Cirrus.
         services.AddScoped<TaskRunner, HistoryComputer>();
         services.AddSingleton<IComputeHistoryQueue, ComputeHistoryQueue>();

         services.AddResponseCompression();
         services.AddMemoryCache();
         services.AddHostedService<SyncServer>();

         services.AddControllers(options =>
         {
            options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
            options.Conventions.Add(new ActionHidingConvention());
         }).AddJsonOptions(options =>
         {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase,
               allowIntegerValues: false));
         }).AddNewtonsoftJson(options =>
         {
            options.SerializerSettings.FloatFormatHandling = FloatFormatHandling.DefaultValue;
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; // Don't include null fields.
         });

         services.AddSwaggerGen(
            options =>
            {
               string assemblyVersion = typeof(Startup).Assembly.GetName().Version.ToString();

               options.SwaggerDoc("indexer",
                  new OpenApiInfo
                  {
                     Title = "Blockcore Indexer API",
                     Version = assemblyVersion,
                     Description = "Blockchain index database that can be used for blockchain based software and services.",
                     Contact = new OpenApiContact
                     {
                        Name = "Blockcore",
                        Url = new Uri("https://www.blockcore.net/")
                     }
                  });

               // integrate xml comments
               if (File.Exists(XmlCommentsFilePath))
               {
                  options.IncludeXmlComments(XmlCommentsFilePath);
               }

               // options.EnableAnnotations();
            });

         services.AddSwaggerGenNewtonsoftSupport(); // explicit opt-in - needs to be placed after AddSwaggerGen()

         services.AddCors(o => o.AddPolicy("IndexerPolicy", builder =>
         {
            builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
         }));

         services.AddTransient<IMapMongoBlockToStorageBlock, MapMongoBlockToStorageBlock>();
         services.AddSingleton<ICryptoClientFactory, CryptoClientFactory>();
         services.AddSingleton<ISyncBlockTransactionOperationBuilder, SyncBlockTransactionOperationBuilder>();

         // TODO: Verify that it is OK we add this to shared Startup for Blockcore and Cirrus.
         services.AddTransient<IBlockRewindOperation, BlockRewindOperation>();
      }

      public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         app.UseExceptionHandler("/error");

         // Enable Cors
         app.UseCors("IndexerPolicy");

         app.UseResponseCompression();

         //app.UseMvc();

         app.UseDefaultFiles();

         app.UseStaticFiles();

         app.UseRouting();

         app.UseSwagger(c =>
         {
            c.RouteTemplate = "docs/{documentName}/openapi.json";
         });

         app.UseSwaggerUI(c =>
         {
            c.RoutePrefix = "docs";
            c.SwaggerEndpoint("/docs/indexer/openapi.json", "Blockcore Indexer API");
         });

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }

      private static string XmlCommentsFilePath
      {
         get
         {
            string basePath = PlatformServices.Default.Application.ApplicationBasePath;
            string fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
            return Path.Combine(basePath, fileName);
         }
      }

      /// <summary>
      /// Hide Stratis related endpoints in Swagger shown due to using Nuget packages
      /// in WebApi project for serialization.
      /// </summary>
      public class ActionHidingConvention : IActionModelConvention
      {
         public void Apply(ActionModel action)
         {
            // Replace with any logic you want
            if (!action.Controller.DisplayName.Contains("Blockcore.Indexer"))
            {
               action.ApiExplorer.IsVisible = false;
            }
         }
      }
   }
}
