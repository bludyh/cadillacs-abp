#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM base AS production
WORKDIR /app
COPY Services/StudyProgress/StudyProgress.EventHandler/bin/Release/netcoreapp3.1/publish .
ENTRYPOINT ["dotnet", "StudyProgress.EventHandler.dll"]

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/StudyProgress/StudyProgress.EventHandler/StudyProgress.EventHandler.csproj", "Services/StudyProgress/StudyProgress.EventHandler/"]
RUN dotnet restore "Services/StudyProgress/StudyProgress.EventHandler/StudyProgress.EventHandler.csproj"
COPY . .
WORKDIR "/src/Services/StudyProgress/StudyProgress.EventHandler"
RUN dotnet build "StudyProgress.EventHandler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudyProgress.EventHandler.csproj" -c Release -o /app/publish

FROM base AS development
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudyProgress.EventHandler.dll"]