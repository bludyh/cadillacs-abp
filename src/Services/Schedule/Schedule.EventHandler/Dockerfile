#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM base AS production
WORKDIR /app
COPY Services/Schedule/Schedule.EventHandler/bin/Release/netcoreapp3.1/publish .
ENTRYPOINT ["dotnet", "Schedule.EventHandler.dll"]

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Schedule/Schedule.EventHandler/Schedule.EventHandler.csproj", "Services/Schedule/Schedule.EventHandler/"]
RUN dotnet restore "Services/Schedule/Schedule.EventHandler/Schedule.EventHandler.csproj"
COPY . .
WORKDIR "/src/Services/Schedule/Schedule.EventHandler"
RUN dotnet build "Schedule.EventHandler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Schedule.EventHandler.csproj" -c Release -o /app/publish

FROM base AS development
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Schedule.EventHandler.dll"]