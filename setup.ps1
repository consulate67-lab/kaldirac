$ErrorActionPreference = "Stop"

Write-Host "Creating Solution..."
dotnet new sln -n AntigravityAiTraderV2

Write-Host "Creating Projects..."
dotnet new classlib -n AntigravityAiTraderV2.Core -f net8.0
dotnet new classlib -n AntigravityAiTraderV2.Application -f net8.0
dotnet new classlib -n AntigravityAiTraderV2.Infrastructure -f net8.0
dotnet new webapi -n AntigravityAiTraderV2.WebAPI -f net8.0
dotnet new worker -n AntigravityAiTraderV2.Worker -f net8.0

Write-Host "Adding Projects to Solution..."
dotnet sln add AntigravityAiTraderV2.Core/AntigravityAiTraderV2.Core.csproj
dotnet sln add AntigravityAiTraderV2.Application/AntigravityAiTraderV2.Application.csproj
dotnet sln add AntigravityAiTraderV2.Infrastructure/AntigravityAiTraderV2.Infrastructure.csproj
dotnet sln add AntigravityAiTraderV2.WebAPI/AntigravityAiTraderV2.WebAPI.csproj
dotnet sln add AntigravityAiTraderV2.Worker/AntigravityAiTraderV2.Worker.csproj

Write-Host "Setting up Project References..."
dotnet add AntigravityAiTraderV2.Application/AntigravityAiTraderV2.Application.csproj reference AntigravityAiTraderV2.Core/AntigravityAiTraderV2.Core.csproj

dotnet add AntigravityAiTraderV2.Infrastructure/AntigravityAiTraderV2.Infrastructure.csproj reference AntigravityAiTraderV2.Core/AntigravityAiTraderV2.Core.csproj
dotnet add AntigravityAiTraderV2.Infrastructure/AntigravityAiTraderV2.Infrastructure.csproj reference AntigravityAiTraderV2.Application/AntigravityAiTraderV2.Application.csproj

dotnet add AntigravityAiTraderV2.WebAPI/AntigravityAiTraderV2.WebAPI.csproj reference AntigravityAiTraderV2.Application/AntigravityAiTraderV2.Application.csproj
dotnet add AntigravityAiTraderV2.WebAPI/AntigravityAiTraderV2.WebAPI.csproj reference AntigravityAiTraderV2.Infrastructure/AntigravityAiTraderV2.Infrastructure.csproj

dotnet add AntigravityAiTraderV2.Worker/AntigravityAiTraderV2.Worker.csproj reference AntigravityAiTraderV2.Application/AntigravityAiTraderV2.Application.csproj
dotnet add AntigravityAiTraderV2.Worker/AntigravityAiTraderV2.Worker.csproj reference AntigravityAiTraderV2.Infrastructure/AntigravityAiTraderV2.Infrastructure.csproj

Write-Host "Adding NuGet Packages..."
dotnet add AntigravityAiTraderV2.Infrastructure/AntigravityAiTraderV2.Infrastructure.csproj package Microsoft.EntityFrameworkCore
dotnet add AntigravityAiTraderV2.Infrastructure/AntigravityAiTraderV2.Infrastructure.csproj package Microsoft.EntityFrameworkCore.Design
dotnet add AntigravityAiTraderV2.Infrastructure/AntigravityAiTraderV2.Infrastructure.csproj package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add AntigravityAiTraderV2.Infrastructure/AntigravityAiTraderV2.Infrastructure.csproj package StackExchange.Redis

dotnet add AntigravityAiTraderV2.WebAPI/AntigravityAiTraderV2.WebAPI.csproj package Microsoft.EntityFrameworkCore.Design

Write-Host "Done!"
