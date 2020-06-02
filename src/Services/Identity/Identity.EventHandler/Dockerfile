#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM base AS production
WORKDIR /app
COPY Services/Identity/Identity.EventHandler/bin/Release/netcoreapp3.1/publish .
ENTRYPOINT ["dotnet", "Identity.EventHandler.dll"]

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Identity/Identity.EventHandler/Identity.EventHandler.csproj", "Services/Identity/Identity.EventHandler/"]
COPY ["Infrastructure/Infrastructure.Common/Infrastructure.Common.csproj", "Infrastructure/Infrastructure.Common/"]
COPY ["Services/Identity/Identity.Common/Identity.Common.csproj", "Services/Identity/Identity.Common/"]
RUN dotnet restore "Services/Identity/Identity.EventHandler/Identity.EventHandler.csproj"
COPY . .
WORKDIR "/src/Services/Identity/Identity.EventHandler"
RUN dotnet build "Identity.EventHandler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.EventHandler.csproj" -c Release -o /app/publish

FROM base AS development
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.EventHandler.dll"]