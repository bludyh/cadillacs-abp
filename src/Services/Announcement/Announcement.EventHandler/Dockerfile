#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM base AS production
WORKDIR /app
COPY Services/Announcement/Announcement.EventHandler/bin/Release/netcoreapp3.1/publish .
ENTRYPOINT ["dotnet", "Announcement.EventHandler.dll"]

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Announcement/Announcement.EventHandler/Announcement.EventHandler.csproj", "Services/Announcement/Announcement.EventHandler/"]
COPY ["Services/Announcement/Announcement.Common/Announcement.Common.csproj", "Services/Announcement/Announcement.Common/"]
COPY ["Infrastructure/Infrastructure.Common/Infrastructure.Common.csproj", "Infrastructure/Infrastructure.Common/"]
RUN dotnet restore "Services/Announcement/Announcement.EventHandler/Announcement.EventHandler.csproj"
COPY . .
WORKDIR "/src/Services/Announcement/Announcement.EventHandler"
RUN dotnet build "Announcement.EventHandler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Announcement.EventHandler.csproj" -c Release -o /app/publish

FROM base AS development
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Announcement.EventHandler.dll"]