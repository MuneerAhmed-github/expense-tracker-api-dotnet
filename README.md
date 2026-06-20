# Expense Tracker API

A RESTful Web API built with ASP.NET Core, backed by SQL Server using Entity Framework Core.

## Features
- Full CRUD operations (Create, Read, Update, Delete)
- Entity Framework Core with Code-First migrations
- SQL Server (LocalDB) for persistent data storage
- Swagger UI for interactive API documentation
- Dependency Injection for clean architecture

## Tech Stack
- ASP.NET Core 8.0 Web API
- Entity Framework Core
- SQL Server LocalDB
- Swagger / OpenAPI

## Endpoints
| Method | Route | Description |
|--------|-------|--------------|
| GET | /api/Expenses | Get all expenses |
| POST | /api/Expenses | Add a new expense |
| PUT | /api/Expenses/{id} | Update an existing expense |
| DELETE | /api/Expenses/{id} | Delete an expense |

## How to Run
1. Clone this repository
2. Update the connection string in `appsettings.json` if needed
3. Run `Update-Database` in Package Manager Console
4. Press F5 to run — Swagger UI opens automatically

## Author
~~ MUNEER AHMED.