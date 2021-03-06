#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM base AS production
WORKDIR /app
COPY Services/Schedule/Schedule.Api/bin/Release/netcoreapp3.1/publish .
ENTRYPOINT ["dotnet", "Schedule.Api.dll"]

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Schedule/Schedule.Api/Schedule.Api.csproj", "Services/Schedule/Schedule.Api/"]
COPY ["Services/Schedule/Schedule.Common/Schedule.Common.csproj", "Services/Schedule/Schedule.Common/"]
COPY ["Infrastructure/Infrastructure.Common/Infrastructure.Common.csproj", "Infrastructure/Infrastructure.Common/"]
RUN dotnet restore "Services/Schedule/Schedule.Api/Schedule.Api.csproj"
COPY . .
WORKDIR "/src/Services/Schedule/Schedule.Api"
RUN dotnet build "Schedule.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Schedule.Api.csproj" -c Release -o /app/publish

FROM base AS development
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Schedule.Api.dll"]