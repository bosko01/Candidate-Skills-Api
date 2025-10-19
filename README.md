# Candidate Skills API (ASP.NET Core / .NET 8)

A lightweight CRUD Web API for managing **candidates and their skills**.  
The project is built using **Clean/Onion architecture**, with **CQRS (MediatR)**, **Entity Framework Core**, and **FluentValidation**.

---

## 🚀 Features

- Create a new candidate (optionally with skills)
- Add and remove skills from a candidate
- Delete candidates
- Search candidates by name and/or skill(s)
- Manage the list of skills
- Full **Swagger/OpenAPI** documentation
- Centralized **ErrorHandlingMiddleware** with consistent HTTP status codes and messages

---

## 🧱 Technologies

- **.NET 8**, ASP.NET Core Web API  
- **Entity Framework Core** (SQL Server)  
- **CQRS** with **MediatR**  
- **FluentValidation** + ValidationBehavior (automatic 400 responses)  
- **Swagger/OpenAPI**  
- (Optional) **Serilog** for request and error logging  

---

## 🏗️ Architecture Overview
Domain
├─ Models (Candidate, Skill, CandidateSkill)
├─ ValueObjects (Email)
└─ Interfaces (Repositories, UnitOfWork)

Application
├─ Common (CQRS, Behaviors)
├─ DTOs (+ Mappers)
├─ Candidates (Commands / Queries)
└─ Skills (Commands / Queries)

Infrastructure
├─ AppDbContext + EF Core Configurations
├─ Repositories (+ UnitOfWork)
└─ DependencyInjection (AddInfrastructureLayer)

Api
├─ Controllers (thin, MediatR-based)
└─ Middleware (ErrorHandlingMiddleware)

---

## ⚙️ Setup & Run

### 1️⃣ Configure the database
In **Api/appsettings.json**, configure your SQL Server connection string (example for LocalDB):

```json
{
  "ConnectionStrings": {
    "Default": "Server=(localdb)\\MSSQLLocalDB;Database=CandidateSkillsDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
2️⃣ Apply migrations (required!)
  Make sure the dotnet-ef CLI tool is installed and run commands:
dotnet tool install --global dotnet-ef
dotnet ef database update --project Infrastructure --startup-project Api

3️⃣ Seed test data

Inside Infrastructure/DatabaseSeed there is a file named DmlScript.sql
→ It contains SQL insert statements for testing API functionality.
Run it in SQL Server Management Studio after creating the database.

4️⃣ Run the application
dotnet run --project Api


| Method   | Route                                    | Description                         |
| -------- | ---------------------------------------- | ----------------------------------- |
| `POST`   | `/api/candidates`                        | Create a new candidate              |
| `GET`    | `/api/candidates/all`                    | Get all candidates                  |
| `GET`    | `/api/candidates?name=Ana&skills=C#,SQL` | Search by name and/or skill(s)      |
| `GET`    | `/api/candidates/{id}`                   | Get candidate details (with skills) |
| `POST`   | `/api/candidates/{id}/skills`            | Add skills to a candidate           |
| `DELETE` | `/api/candidates/{id}/skills/{skillId}`  | Remove a skill from a candidate     |
| `DELETE` | `/api/candidates/{id}`                   | Delete a candidate                  |

| Method   | Route              | Description                       |
| -------- | ------------------ | --------------------------------- |
| `POST`   | `/api/skills`      | Create (or return existing) skill |
| `GET`    | `/api/skills`      | Get all skills                    |
| `DELETE` | `/api/skills/{id}` | Delete a skill                    |



# Author:
## Boško Vujanović
### Backend Developer (.NET / C#)

