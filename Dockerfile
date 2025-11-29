FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["src/Geometry.Presentation/CubeApi/CubeApi.csproj", "src/Geometry.Presentation/CubeApi/"]
COPY ["src/Geometry.Application/Geometry.Application.csproj", "src/Geometry.Application/"]
COPY ["src/Geometry.Domain/Geometry.Domain.csproj", "src/Geometry.Domain/"]
COPY ["src/Geometry.Infrastructure/Geometry.Infrastructure.csproj", "src/Geometry.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "src/Geometry.Presentation/CubeApi/CubeApi.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/src/Geometry.Presentation/CubeApi"
RUN dotnet build "CubeApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CubeApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

# Install PostgreSQL client for health checks
RUN apt-get update && apt-get install -y postgresql-client && rm -rf /var/lib/apt/lists/*

# Copy published files
COPY --from=publish /app/publish .

# Copy entrypoint script
COPY entrypoint.sh /app/entrypoint.sh
RUN chmod +x /app/entrypoint.sh

# Set environment to Development
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["/app/entrypoint.sh"]


