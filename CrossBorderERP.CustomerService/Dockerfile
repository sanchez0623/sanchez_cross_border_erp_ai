# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files
COPY CrossBorderERP.CustomerService.sln ./
COPY src/Core/CrossBorderERP.Core/CrossBorderERP.Core.csproj src/Core/CrossBorderERP.Core/
COPY src/Infrastructure/CrossBorderERP.Infrastructure/CrossBorderERP.Infrastructure.csproj src/Infrastructure/CrossBorderERP.Infrastructure/
COPY src/Agents/CrossBorderERP.Agents/CrossBorderERP.Agents.csproj src/Agents/CrossBorderERP.Agents/
COPY src/API/CrossBorderERP.API/CrossBorderERP.API.csproj src/API/CrossBorderERP.API/

# Restore dependencies
RUN dotnet restore

# Copy source code
COPY src/ src/

# Build and publish
RUN dotnet publish src/API/CrossBorderERP.API/CrossBorderERP.API.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose ports
EXPOSE 8080
EXPOSE 8081

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "CrossBorderERP.API.dll"]
