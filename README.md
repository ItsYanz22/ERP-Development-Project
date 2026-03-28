# Hierarchical Item Processing System

A complete ASP.NET Core MVC assessment project demonstrating advanced hierarchical data architectures, complex business logic validation, and class-leading UI/UX design via custom-built CSS Grid and modern design implementations.

## 🌟 Project Overview
This application serves as a robust engine tailored for managing hierarchical entities (Parent-Child relationships). 
It tackles the core engineering problem of dynamically generating weighted outputs from generic inputs while ensuring that business constraints (such as cumulative weight capping) are strongly enforced at the server level. 

## ✨ Key Features
- **Hierarchical Tree Algorithm:** Deep recursive logic elegantly traversing unconstrained n-level depth item relationships.
- **Unified Operations Dashboard:** A highly interactive SPA-like console that pairs live hierarchy rendering with seamless localized form processing.
- **Deterministic Procedural Avatars:** Custom user identity hashing that dynamically assigns 1 of 5 distinct API-driven SVG avatars without bloating the SQL database.
- **Warm Glassmorphism UI:** A premium visually rich aesthetic, meticulously crafted with raw CSS tokens and zero heavy UI libraries.
- **Robust Security Flow:** Custom cookie-based authentication handling routing and state interceptors.

## 🛠 Technology Stack
- **Framework:** ASP.NET Core MVC (.NET 9)
- **Database Engine:** MySQL 8.0 via Pomelo Entity Framework Core
- **Architecture Pattern:** Repository Pattern & Dependency Injection (DI)
- **Frontend Logic:** Native JavaScript (Vanilla) + Scoped CSS Custom Properties
- **Rendering:** Razor Pages

---

## 🚀 Setup & Execution Guide

The following instructions offer a step-by-step roadmap for evaluators to clone, hydrate, and deploy the application locally.

### 1. Prerequisites
- **.NET 9.0 SDK** or later installed.
- **MySQL Server 8.0** running locally.
- Basic familiarity with terminal interfaces.

### 2. Clone the Repository
Open your preferred terminal configuration and navigate to your workspace location, then clone the codebase:
```bash
git clone <repository-url>
cd HierarchicalItemProcessingSystem
```

### 3. Database Configuration
Open the `appsettings.json` file situated in the project root. Ensure the database connection string format precisely aligns with your local MySQL credentials. The project expects the user `root`. Replace `your_mysql_password` with your password:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=HierarchicalItemDb;Uid=root;Pwd=your_mysql_password;"
}
```

### 4. Hydrate the Database Schema
This project utilizes EF Core Code-First Migrations alongside seeding mechanics to automatically build relations.
To push the schema and pre-populate the development data (Test User & Input Items):
```bash
dotnet ef database update
```
*(Alternative Setup Procedure: Execute the provided `DbScripts/Init.sql` script directly inside MySQL Workbench to instantly construct the internal tables and relationships).*

### 5. Compile and Execute
With the database wired up, boot the application via the .NET CLI:
```bash
dotnet build
dotnet run
```
The console will output the active listener port (typically `http://localhost:5180`). Navigate to this address in your browser.

---

## 🎯 Usage Flow

1. **Authentication:**
   Login using the system's seeded administrator credentials:
   - **Email:** `test@user.com`
   - **Password:** `pass`

2. **The Operations Dashboard:**
   Upon successful authentication, you are intelligently routed directly to the Dashboard. Here, interact fluidly:
   - **Right Column:** Select a seeded input item (e.g. `Raw Steel Block`), specify a few output derivations, assign respective weights securely, and tap Process. 
   - **Left Column:** Watch the recursive tree algorithm re-render and cascade your parent-child logic instantly.

3. **Identity Customization:**
   Navigate to the **Profile** tab in the global header to interact with the procedural avatar system. Type in an entirely new Display Name string and watch the system generate a unique SVG asset dynamically bound exactly to your input length and characters!
