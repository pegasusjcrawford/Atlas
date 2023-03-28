#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Okeanos.Atlas.Indexer/Okeanos.Atlas.Indexer.csproj", "src/Okeanos.Atlas.Indexer/"]
RUN dotnet restore "src/Okeanos.Atlas.Indexer/Okeanos.Atlas.Indexer.csproj"
COPY . .
WORKDIR "/src/src/Okeanos.Atlas.Indexer"
RUN dotnet build "Okeanos.Atlas.Indexer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Okeanos.Atlas.Indexer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Okeanos.Atlas.Indexer.dll"]