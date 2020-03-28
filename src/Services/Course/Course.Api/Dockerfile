#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM base AS production
WORKDIR /app
COPY Services/Course/Course.Api/bin/Release/netcoreapp3.1/publish .
ENTRYPOINT ["dotnet", "Course.Api.dll"]

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Course/Course.Api/Course.Api.csproj", "Services/Course/Course.Api/"]
RUN dotnet restore "Services/Course/Course.Api/Course.Api.csproj"
COPY . .
WORKDIR "/src/Services/Course/Course.Api"
RUN dotnet build "Course.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Course.Api.csproj" -c Release -o /app/publish

FROM base AS development
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Course.Api.dll"]