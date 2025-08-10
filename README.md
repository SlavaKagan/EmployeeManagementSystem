# Employee Management System - Developer Interview Task (2025 Edition)

## Overview

** A full-stack **ASP.NET Core MVC** Employee Management System with employee and department CRUD and basic Dashboard that showcases clean architecture layered design basic validation, pagination, structured logging and ships as a Docker container.

## Contact

**Email:** <slava.kagan.ht@gmail.com>

## General info about the task and Tech Stack

**GitHub repository:** https://github.com/SlavaKagan/EmployeeManagementSystem <br />
**Programming Language:** C# <https://learn.microsoft.com/en-us/dotnet/csharp/> <br />
**Framework:** .NET8 <https://dotnet.microsoft.com/en-us/> <br />
**Database- Entity Framework Core:** (SQL Server) - LocalDB ((localdb)\MSSQLLocalDB) <br />
**Database Name:**  EmployeeDB <br />

** ![CI](https://github.com/<your-username>/<repo-name>/actions/workflows/ci.yml/badge.svg)

## How to use this service || Prerequisites

1. Docker Desktop, Install- https://www.docker.com/products/docker-desktop/
2. GIT Install -https://git-scm.com/
3. Open IDE- Visua Studio Code or Visual Studio
4. git clone https://github.com/SlavaKagan/EmployeeManagementSystem.git
   cd EmployeeManagementSystem
5. From the project root (where docker-compose.yml is located), run:
docker-compose up --build
6. Access the Site-
Open a browser and go to: http://localhost:8081
The API should be running and connected to the SQL Server container.
7. At the end of use, stop the Site -- docker-compose down

## Architecture rationale

API-
ASP.NET Core MVC controllers + Razor views.
Orchestrates requests, model binding, and validation surfacing.
ExceptionHandlingMiddleware for friendly errors, and Serilog request logging.

Application-
Use cases implemented as *Service classes behind I*Service interfaces.
Accept/return DTOs, not EF entities.
FluentValidation enforces business/field rules (server-side).
AutoMapper maps DTOs ⇄ domain entities.
Common helpers: PagedResult<T> for paging/sorting/search.

Domain-
Persistence-agnostic entities (Employee, Department) and simple invariants.
Optional base entity for audit columns (CreatedAt, UpdatedAt, IsDeleted).
No framework dependencies.

Infrastructure-
EF Core AppDbContext and repositories (*Repository) behind interfaces.
Soft delete (boolean field): IsDeleted column + query filters or explicit Where(e => !e.IsDeleted).
Migrations, connection strings, and data access optimizations.
Registered via DI; can be swapped without touching upper layers.

## License

No License
