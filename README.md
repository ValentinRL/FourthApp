# FourthApp

## Overview

This project is a solution to the .NET Software Engineer Technical Assessment.

It consists of:
- A .NET backend API exposing customer and order data from the Northwind database
- A minimal React frontend to demonstrate API usage over HTTP

The solution prioritizes clarity, simplicity, and clean architecture over unnecessary complexity.

---

## Features

### Backend API

- Retrieve customers:
  - Search by name
  - Pagination support
  - Order count per customer

- Retrieve customer details:
  - Customer information
  - Order history
  - For each order:
    - Total value
    - Total number of items

### Frontend

- Customer list with search
- Customer details view
- Simple UI for interacting with the API

---

## Architecture

The backend follows a Clean Architecture approach:

API → Application → Infrastructure → Domain

- API: Controllers, request/response models
- Application: Query handlers, DTOs, business logic
- Infrastructure: Entity Framework Core, database access
- Domain: Entities

A lightweight CQRS-style approach is used for queries via MediatR.

---

## Technologies

### Backend
- .NET
- Entity Framework Core
- MediatR
- SQL Server (Northwind database)
- xUnit

### Frontend
- React
- TypeScript
- Vite

---

## How to Run

### 1. Database Setup

This project uses the Northwind sample database.

You need to:
- Install SQL Server (or use an existing instance)
- Restore/import the Northwind database

---

### 2. Configure Backend

Update the connection string in:

FourthApp.Api/appsettings.Development.json

Example:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=Northwind;Trusted_Connection=True;TrustServerCertificate=True;"
}

---

### 3. Run Backend

dotnet run --project FourthApp.Api

---

### 4. Configure Frontend

Navigate to:

FourthApp.Web

Create a .env file based on .env.example:

VITE_API_BASE_URL=http://localhost:5297/api

---

### 5. Run Frontend

NOTE: Make sure that the frontend address is added to the AllowedOrigins in appsettings.Development in the backend to avoid CORS policy issues.

npm install
npm run dev

---

## API Endpoints

GET /api/customers?search=&page=&pageSize=
GET /api/customers/{customerId}

---

## Testing

Run tests with:

dotnet test

---

## Notes

- Frontend communicates with backend over HTTP
- CORS is configured in backend
- Config values are externalized via appsettings.json and .env

---

## Final Thoughts

This solution prioritizes clarity, maintainability, and correctness over over-engineering.
