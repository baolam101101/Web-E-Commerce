# 📌 Web E-Commerce API

A backend RESTful API for a mini e-commerce system, built with ASP.NET Core 8. The project follows Clean Architecture and focuses on scalability, maintainability, and real-world features such as role-based authentication and cart management.

---

## 🧠 Project Goals

- Design and implement a complete RESTful API for an e-commerce platform
- Learn and apply:
  - **Authentication & Authorization** with JWT and role-based access
  - **Entity Framework Core** and relational data modeling
  - **Clean Architecture** with Service–Repository layers
  - **Standardized API responses** and error handling
- Evolve into a **fullstack project** by developing the client side with React

---

## 🛠️ Tech Stack

- **Backend:** ASP.NET Core 8, Entity Framework Core
- **Database:** SQL Server
- **Authentication:** JWT (JSON Web Token)
- **Object Mapping:** AutoMapper
- **API Documentation:** Swagger (Swashbuckle)
- **Validation:** FluentValidation (planned)
- **Testing:** xUnit, Moq (planned)
- **Frontend:** ReactJS (in progress)
- **Development Environment:** Visual Studio 2022

---

## 📁 Project Structure (Simplified)

- `/Models`: Entity definitions (User, Product, Order, etc.)
- `/Data`: `AppDbContext` and database setup
- `/Repositories`: Interfaces & implementations for business logic
- `/Services`: Business logic layer between controllers and repositories
- `/Controllers`: API endpoints
- `/DTOs`: Request/response models
- `/Middleware`: Exception handling, JWT authentication, response wrapping

---

## ✅ Features Implemented

- User registration & login with JWT authentication
- Product & category management (CRUD)
- Cart management and item operations
- Order placement and status tracking
- Order total calculation with validation
- Unified API responses using a standard response wrapper
- Role-based access (User, Admin, Seller – via database roles)
- Global exception handling middleware

---

## 🚧 In Progress / Upcoming Features

- Admin dashboard for managing users, roles, and seller requests
- Seller registration request & approval workflow
- Email notification after successful order placement
- Payment gateway integration (mock or third-party)
- Frontend development with React (client UI)
- Unit & integration tests using xUnit and WebApplicationFactory
- Full documentation and deployment guide

---

## 🔗 GitHub

Feel free to explore the source code: [https://github.com/baolam101101/Web-E-Commerce](https://github.com/baolam101101/Web-E-Commerce)
