# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["AntigravityAiTraderV2.sln", "./"]
COPY ["AntigravityAiTraderV2.Core/AntigravityAiTraderV2.Core.csproj", "AntigravityAiTraderV2.Core/"]
COPY ["AntigravityAiTraderV2.Application/AntigravityAiTraderV2.Application.csproj", "AntigravityAiTraderV2.Application/"]
COPY ["AntigravityAiTraderV2.Infrastructure/AntigravityAiTraderV2.Infrastructure.csproj", "AntigravityAiTraderV2.Infrastructure/"]
COPY ["AntigravityAiTraderV2.WebAPI/AntigravityAiTraderV2.WebAPI.csproj", "AntigravityAiTraderV2.WebAPI/"]
COPY ["AntigravityAiTraderV2.Worker/AntigravityAiTraderV2.Worker.csproj", "AntigravityAiTraderV2.Worker/"]

# Restore dependencies
RUN dotnet restore

# Copy everything and build
COPY . .
WORKDIR "/src/."
RUN dotnet build -c Release -o /app/build

# Publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Final Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Default to WebAPI. Railway can override this in 'Start Command' for Worker
ENTRYPOINT ["dotnet", "AntigravityAiTraderV2.WebAPI.dll"]
