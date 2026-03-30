# 📢 Cloud-Native Bulletin Board Platform

A full-stack, decoupled classified advertisements platform built with **.NET 10**, **C#**, and hosted entirely on **Microsoft Azure**. The application allows users to browse, create, edit, and delete announcements across a strictly typed hierarchical category system.

This project demonstrates a modern **Clean Architecture** approach, advanced database optimization using raw SQL Stored Procedures via **Dapper**, secure third-party authentication via **Google Identity (OAuth 2.0)**, and is backed by automated unit tests.

---

## ✅ Core Features & Implementation

| Feature | Implementation Status & Details |
| :--- | :--- |
| **1. Announcements Management** | ✔️ Full CRUD capabilities. Users can create, view, update, and delete their own announcements. Each post tracks Title, Description, UTC Creation Date, and Status. |
| **2. Categorization & Filtering** | ✔️ Implemented a hierarchical category structure (Home Appliances, Computer Hardware, Smartphones, Miscellaneous). Real-time filtering on the UI. |
| **3. Clean Architecture** | ✔️ Strict separation of concerns into Domain, Application, Infrastructure, API, and UI layers. |
| **4. Database Interaction** | ✔️ Bypassed heavy ORMs in favor of highly optimized, raw **SQL Stored Procedures** executed via the **Dapper** micro-ORM for maximum performance. |
| **5. Google OAuth & Security 🌟** | ✔️ Integrated Google Identity for user authentication. Utilizes stateless **JWT (JSON Web Tokens)** for secure API authorization. |

---

## 🏗️ Architectural Decisions & Solution Structure

The repository is structured following enterprise best practices, organized into `database`, `src`, and `tests` directories.

### 1. The `src/` Directory (Clean Architecture)
* **`BulletinBoard.Core` (Domain & Application Layers):** Logically split via folders. 
  * The `Domain` folder contains enterprise logic, entities, and enums (Categories/Statuses).
  * The `Application` folder contains business use cases, DTOs, Mappers, and Service/Repository interfaces.
* **`BulletinBoard.Infrastructure`:** Implements the core repository interfaces. Handles data access using **Dapper** to map Stored Procedure results directly into C# POCOs, ensuring ultra-low latency.
* **`BulletinBoard.Api`:** The presentation layer for the backend. Acts as a RESTful gateway, validates JWT tokens, and routes requests to the Application services.
* **`BulletinBoard.UI`:** The Frontend Client (ASP.NET Core MVC). Handles view rendering and Google OAuth flow. Utilizes a strongly typed HTTP Client (`AnnouncementApiClient`) to communicate securely with the API.

*💡 **Design Pattern Highlight:** The solution uses **Decentralized Dependency Injection**. Each layer contains its own `DependencyInjection.cs` extension class, ensuring that the API layer doesn't need to know about the internal implementations of the Infrastructure layer.*

### 2. The `database/` Directory (SQL Scripts)
This folder serves as the repository for all raw SQL scripts required to initialize the database schema, indexes, and procedures:
* `000_Create_Database.sql`, `001_Create_Announcements_Table.sql`, `002_Create_StoredProcedures.sql`.
* **Non-Clustered Indexing:** Implemented `IX_Announcements_Category` to make category-based searches lightning-fast.

### 3. Secure Identity Flow (Google ➡️ UI ➡️ API)
1. User logs in via Google on the Frontend.
2. The UI retrieves the user's unique Google `AuthorId`.
3. The UI requests a securely signed **JWT** from the API using a shared `SecretKey`.
4. The JWT is attached to the `Authorization: Bearer` header for all subsequent CRUD requests.

---

## 🧪 Testing

The application's reliability is ensured through automated **Unit Testing** located in the `tests/` directory.
* **`BulletinBoard.Tests`:** Contains unit tests (e.g., `AnnouncementServiceTests`) focusing on verifying core business logic and data validation rules within the Application layer.
* Dependencies are mocked to ensure tests run in isolation without relying on the physical database.

---

## 🚀 Deployment & Cloud Infrastructure

The application is fully hosted on **Microsoft Azure** using Platform as a Service (PaaS) offerings.

* **API App Service:** Deployed to a dedicated Azure Web App instance.
* **UI App Service:** Deployed to a separate Azure Web App instance, communicating server-to-server with the API.
* **Database:** Hosted on Azure SQL Database.
* **⚠️ Note on Performance (Cold Start):** The application is hosted on Free Tier (F1). If the platform has been inactive, the initial request may experience a noticeable delay (up to a minute) or result in a temporary timeout while the API server wakes up. Refreshing the page will resolve this and restore optimal performance.

---

## 💻 Setup Instructions & Local Development

### Prerequisites
* **.NET 10 SDK**
* **Microsoft SQL Server** (or LocalDB)
* Google Cloud Console account (for OAuth Client ID & Secret)

### 1. Database Setup
Execute the SQL scripts located in the `database/scripts` folder in sequential order to create the schema and procedures.

### 2. Secure Configuration (User Secrets)
To avoid hardcoding sensitive data, use the .NET Secret Manager.

**For the API Project (`BulletinBoard.Api`):**
```bash
cd src/BulletinBoard.Api
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=YOUR_SERVER;Database=BulletinBoard;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
dotnet user-secrets set "JwtSettings:SecretKey" "YOUR_SUPER_LONG_SECURE_JWT_SECRET_KEY"
```

**For the UI Project (`BulletinBoard.UI`):**
```bash
cd src/BulletinBoard.UI
dotnet user-secrets init
dotnet user-secrets set "Authentication:Google:ClientId" "YOUR_GOOGLE_CLIENT_ID"
dotnet user-secrets set "Authentication:Google:ClientSecret" "YOUR_GOOGLE_CLIENT_SECRET"
dotnet user-secrets set "JwtSettings:SecretKey" "YOUR_SUPER_LONG_SECURE_JWT_SECRET_KEY"
```

*Note: Ensure the `ApiSettings:BaseUrl` in the UI's `appsettings.json` points to your local API (e.g., `https://localhost:5001/`).*

### 3. Running the Application
Set both `BulletinBoard.Api` and `BulletinBoard.UI` as **Multiple Startup Projects** in Visual Studio. Run the solution, and the UI will automatically launch in your browser.

---

## 🔗 Deliverables

- **Live Web Application:** [https://bulletinboard.azurewebsites.net/](https://bulletinboard.azurewebsites.net/)
- **API Endpoint (Sample Data):** [https://bulletin-board-api-fadnbkggb6d6h4gd.uaenorth-01.azurewebsites.net/api/Announcements](https://bulletin-board-api-fadnbkggb6d6h4gd.uaenorth-01.azurewebsites.net/api/Announcements)
- **Source Code:** [Provided in this repository](https://github.com/valentyn-b/BulletinBoard)