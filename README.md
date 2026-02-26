# 🚀 Project: Product Catalog (ASP.NET Core MVC)

### 🛤️ The Rails-to-ASP.NET Transition
This project served as a Rosetta Stone for applying **Ruby on Rails** concepts to the **.NET 10.0** framework. By leveraging existing knowledge of MVC architecture, I successfully mapped Rails conventions to their ASP.NET equivalents:

| Feature | Ruby on Rails | ASP.NET Core MVC |
| :--- | :--- | :--- |
| **Data Layer** | ActiveRecord | Entity Framework Core (EF Core) |
| **Database Setup** | `db/migrate` | EF Migrations (`Add-Migration`) |
| **Service Wiring** | Initializers / Autoloading | Dependency Injection (`Program.cs`) |
| **Templating** | ERB (`.html.erb`) | Razor (`.cshtml`) |
| **URL Helpers** | `link_to "Home", root_path` | Tag Helpers (`asp-controller`, `asp-action`) |

---

### ✅ Accomplishments

#### 1. Data Modeling & Persistence
* Defined a `Product` **Model** with properties for `Name`, `Price`, and `ImageUrl`.
* Configured the `ApplicationDbContext` to act as the gateway to the SQL Server database.
* Managed the schema using **EF Core Migrations** to generate the database table.

#### 2. Automated Scaffolding
* Utilized the ASP.NET Scaffolder to generate a **Controller** and **CRUD Views** (Create, Read, Update, Delete) based on the Product model.
* Resolved **Dependency Injection** errors by explicitly registering the DbContext in the application's builder services.

#### 3. Routing & Navigation
* Modified the **Shared Layout** (`_Layout.cshtml`) to include global navigation.
* Implemented **Tag Helpers** to create dynamic links between the Home page and the Product Catalog.
* Enhanced the landing page with a primary Call-to-Action (CTA) using Bootstrap styling.

#### 4. Frontend Integration
* Connected the UI to the backend logic to display a list of products dynamically from the database.
* Utilized Razor syntax to inject C# data directly into HTML attributes.

---

### 🛠️ Tech Stack
* **Language:** C#
* **Framework:** ASP.NET Core 10.0 (MVC)
* **ORM:** Entity Framework Core
* **Database:** LocalDB / SQL Server
* **Frontend:** Razor Pages, HTML5, Bootstrap 5