# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
RUN apt-get update && apt-get install -y iputils-ping
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SiteCheck.Web/SiteCheck.Web.csproj", "SiteCheck.Web/"]
COPY ["SiteCheck.Application/SiteCheck.Application.csproj", "SiteCheck.Application/"]
COPY ["SiteCheck.Domain/SiteCheck.Domain.csproj", "SiteCheck.Domain/"]
COPY ["SiteCheck.Persistence/SiteCheck.Persistence.csproj", "SiteCheck.Persistence/"]
RUN dotnet restore "./SiteCheck.Web/SiteCheck.Web.csproj"
COPY . .
WORKDIR "/src/SiteCheck.Web"
RUN dotnet build "./SiteCheck.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SiteCheck.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SiteCheck.Web.dll"]