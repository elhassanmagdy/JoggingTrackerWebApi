# Jogging Tracker Web API

A RESTful Web API built with ASP.NET Core for tracking jogging activities.  
The system supports authentication, role-based authorization, and full CRUD operations with advanced features like filtering, and reporting.

---

##  Features

-  Authentication using JWT
-  Role-Based Authorization (User / Admin)
-  CRUD operations for jogging entries
-  Filtering by date range
-  Reporting (total distance, average duration, etc.)
-  Clean Architecture (Controller - Service - Repository)
-  Entity Framework Core with SQL Server

---

## User Roles

###  User
- Manage own jogging records (Create / Read / Update / Delete)
- View personal reports

###  Admin
- View all users' jogging data
- Access all records (Read-only for jogging entries)

---

##  Architecture

- Controllers → Handle HTTP requests
- Services → Business logic
- Repositories → Data access layer
- DTOs → Data transfer between layers

---

##  Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication
- LINQ

---

##  API Endpoints Overview

### Auth
- POST `/api/auth/register`
- POST `/api/auth/login`

### Jogging
- GET `/api/jogging`
- POST `/api/jogging`
- PUT `/api/jogging/{id}`
- DELETE `/api/jogging/{id}`
- GET `/api/jogging/filter`
- GET `/api/jogging/report`
- GET `/api/jogging/paged`




