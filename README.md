# JobApplicationLogger

Lightweight ASP.NET Core (Controllers + Views) application to track job applications for companies. Provides create/read/update/delete operations, searching, sorting and CSV export.

## Tech stack
- .NET 8 (C# 12)
- ASP.NET Core MVC (Controllers + Views)
- Entity Framework Core 8 (SQL Server provider)
- EPPlus (Excel export) and CsvHelper (CSV generation)
- Projects:
  - JobApplicationLogger (web UI)
  - Entities (EF Core models + DbContext)
  - ServiceContracts (DTOs, interfaces)
  - Services (business logic)
  - CRUDTests (xUnit tests)

## Prerequisites
- .NET 8 SDK
- Visual Studio 2022 (latest updates recommended) or VS Code
- SQL Server or LocalDB
- (Optional) EF Core tools: `dotnet tool install --global dotnet-ef` or use __Package Manager Console__ in Visual Studio

## Configuration
Set your database connection string in JobApplicationLogger/appsettings.json under the `DefaultConnection` key. Example: