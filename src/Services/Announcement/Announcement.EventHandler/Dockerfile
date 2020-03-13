#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Services/Announcement/Announcement.EventHandler/Announcement.EventHandler.csproj", "Services/Announcement/Announcement.EventHandler/"]
RUN dotnet restore "Services/Announcement/Announcement.EventHandler/Announcement.EventHandler.csproj"
COPY . .
WORKDIR "/src/Services/Announcement/Announcement.EventHandler"
RUN dotnet build "Announcement.EventHandler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Announcement.EventHandler.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Announcement.EventHandler.dll"]