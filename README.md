# Bugify – Task Manager & Bug Tracker

**Bugify** is a Task Manager application built with **ASP.NET Core**.  
The project is divided into:

- **Bugify.API** – backend (REST API + EF Core)
- **Bugify.UI** – frontend in ASP.NET Core MVC (a simple kanban-style task board)

---

## 🎯 Features

- Task board view with columns based on task status.
- Creating new tasks:
  - title,
  - description,
  - due date (DueDate),
  - status (NotStarted, InProgress, Completed, OnHold, Cancelled).
- Editing existing tasks.
- Deleting tasks.
- Server-side validation in API + error mapping in UI (no raw 400 errors).
- Seeded task status dictionary (TaskProgress) using EF Core.

---

## 🧱 Architecture

### Bugify.API (backend)

- **ASP.NET Core Web API**
- **Entity Framework Core** + **Code First Migrations**
- **AutoMapper** for mapping between:
  - `AddTask` / `TaskProgress` (domain),
  - `TaskDto`, `CreateTaskDto`, `UpdateTaskRequestDto` (DTO).
- **Repository pattern** (`ITaskRepository`, `TaskRepository`).
- `BugifyDbContext`:
  - `DbSet<AddTask>` – tasks,
  - `DbSet<TaskProgress>` – task status dictionary with seeded values (“NotStarted”, “InProgress”, etc.).

Main endpoints:

- `GET /api/Task` – get all tasks  
- `GET /api/Task/{id}` – get task details  
- `POST /api/Task` – create a new task  
- `PUT /api/Task/{id}` – update a task  
- `DELETE /api/Task/{id}` – delete a task  

Swagger (OpenAPI) is also configured for easy testing.

---

### Bugify.UI (frontend)

- **ASP.NET Core MVC**
- Razor Views + **Bootstrap** (+ custom `board.css` for the task board layout)
- `TaskController` and `HomeController` communicate with the API using `HttpClient` (created via `IHttpClientFactory`)
- View models:
  - `CreateTaskViewModel` – create task form
  - `TaskDto` – task data received from the API
  - `TaskBoardViewModel` / `TaskBoardPageViewModel` – task board column representation

Validation:

- Validation attributes in API + `ApiBehaviorOptions` configuration that returns JSON like:

```json
{
  "message": "Validation failed",
  "errors": {
    "Title": ["Title is required"],
    "DueDate": ["Due date is required"]
  }
}

