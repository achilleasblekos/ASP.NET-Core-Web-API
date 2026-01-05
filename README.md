# ASP.NET Core Web API – Portfolio Project

## Overview

This repository contains a **RESTful ASP.NET Core Web API** developed as a personal portfolio project.

The project started as a guided learning exercise based on a YouTube tutorial and was later extended to reflect real-world backend development practices such as authentication, database persistence, and containerized deployment.

---

## Motivation

The purpose of this project was to:

- Gain hands-on experience with **ASP.NET Core Web APIs**
- Understand **JWT-based authentication and authorization**
- Work with **Entity Framework Core** and SQL Server
- Apply clean backend architecture principles
- Prepare an API for real deployment scenarios using **Docker**

The initial implementation was inspired by the following tutorial:

👉 **YouTube Tutorial**  
https://www.youtube.com/playlist?list=PL82C6-O4XrHfrGOCPmKmwTO7M0avXyQKc

The final result goes beyond the tutorial and reflects personal extensions and refinements.

---

## How the Application Works

- The API is built using **ASP.NET Core 8** following REST principles
- Authentication is handled via **JWT tokens**
- Users authenticate and receive a token, which is required for protected endpoints
- **ASP.NET Core Identity** manages users and roles
- **Entity Framework Core** handles database access and migrations
- The database schema is automatically created and updated on startup
- Initial data is seeded to allow immediate interaction with the API
- External financial data is fetched via an HTTP client
- The entire system runs inside Docker containers for consistency and portability

---

## Tech Stack

- **ASP.NET Core 8**
- **Entity Framework Core**
- **ASP.NET Core Identity**
- **JWT (JSON Web Tokens)**
- **SQL Server**
- **Docker & Docker Compose**
- **Swagger / OpenAPI**

---

## Authentication & Authorization

- JWT Bearer authentication
- Token validation (issuer, audience, lifetime, signing key)
- Role-based authorization
- Stateless and secure API communication

---

## Database & Persistence

- SQL Server as the relational database
- Entity Framework Core as the ORM
- Automatic execution of migrations on application startup
- Pre-seeded data for development and testing

---

## API Documentation

Swagger is enabled in development mode and provides:

- Interactive endpoint testing
- JWT authorization support
- Clear visibility of available API endpoints

---

## Running the Project (Docker)

The entire backend stack is containerized.

```bash
docker compose up --build
```

Once running:

### API available at:

http://localhost:8080

### Swagger UI:

http://localhost:8080/swagger

---

## Why This Project Matters

This project reflects my ability to:

- Build secure backend APIs
- Work with modern .NET tooling
- Design maintainable server-side architectures
- Prepare applications for real deployment scenarios

It represents both learning progression and practical backend development experience.

---

## License

This project is intended for educational and portfolio purposes.
