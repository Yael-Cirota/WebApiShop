# WebApiShop

A RESTful Web API project built with **.NET 9** and **C#**, following a clean layered architecture with strong separation of concerns.

---

## 🏗️ Architecture

The project is structured into **3 main layers**, each with a distinct responsibility:

| Layer | Project | Responsibility |
|---|---|---|
| **Application** | `WebApiShop` | Controllers, Middleware, entry point |
| **Services** | `UserServices` | Business logic, validation, orchestration |
| **Repository** | `UserRepository` | Data access, database queries |

> The layers communicate with each other through **Dependency Injection**, ensuring loose coupling and high testability.

---

## 📦 Additional Layers

### DTO Layer (`DTO's`)
- Decouples the **data layer** from the rest of the application
- Prevents **circular dependencies**
- Uses **C# Records**, which are well-suited for immutable data transfer objects
- Conversion between **Entities** and **DTOs** is handled automatically via **AutoMapper**

### Entities Layer (`Entities`)
- Contains the database model classes
- Used exclusively by the Repository layer

---

## 🗄️ Database

- Connected via **Entity Framework Core** using the **Database First** approach
- All database access is performed **asynchronously** to free up threads and improve scalability

---

## ⚙️ Configuration

- All configurations (connection strings, settings, etc.) are stored in `appsettings.json` file
- Code and configuration are kept **fully separated**

---

## 📋 Logging & Error Handling

- Logging is implemented using the **NLog** library (`nlog.config`)
- Errors are caught and handled centrally by a dedicated **Error Handling Middleware**, keeping controllers clean

---

## 📊 Request Tracking

- All server requests are logged into a **Rating table** in the database for tracking and monitoring purposes

---

## 🧪 Testing

- The `TestProject` contains both **Unit Tests** and **Integration Tests**
- Tests are written using the **xUnit** framework
- A `DatabaseFixture` is used to manage shared database state across integration tests

---

## 🛠️ Tech Stack

| Technology | Purpose |
|---|---|
| .NET 9 / C# | Runtime & language |
| ASP.NET Core Web API | RESTful API framework |
| Entity Framework Core | ORM (Database First) |
| AutoMapper | Entity ↔ DTO mapping |
| NLog | Logging |
| xUnit | Unit & Integration testing |
| SQL Server | Database |
