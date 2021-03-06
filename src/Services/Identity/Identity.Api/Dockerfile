#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM base AS production
WORKDIR /app
COPY Services/Identity/Identity.Api/bin/Release/netcoreapp3.1/publish .
ENTRYPOINT ["dotnet", "Identity.Api.dll"]

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Identity/Identity.Api/Identity.Api.csproj", "Services/Identity/Identity.Api/"]
COPY ["Infrastructure/Infrastructure.Common/Infrastructure.Common.csproj", "Infrastructure/Infrastructure.Common/"]
COPY ["Services/Identity/Identity.Common/Identity.Common.csproj", "Services/Identity/Identity.Common/"]
RUN dotnet restore "Services/Identity/Identity.Api/Identity.Api.csproj"
COPY . .
WORKDIR "/src/Services/Identity/Identity.Api"
RUN dotnet build "Identity.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.Api.csproj" -c Release -o /app/publish

FROM base AS development
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.Api.dll"]