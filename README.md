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
* Defined a `Product` **Model** with properties for `Name`, `Price`, `ImageUrl`, and `StockQty`.
* Configured the `ApplicationDbContext` to act as the gateway to the SQL Server database.
* Managed the schema using **EF Core Migrations** to generate and update database tables.

#### 2. Automated Scaffolding & Customization
* Utilized the ASP.NET Scaffolder to generate a **Controller** and **CRUD Views**.
* Resolved **Dependency Injection** errors by explicitly registering the DbContext in the application's builder services.
* Overrode default scaffolding to implement custom admin logic for inventory management.

#### 3. Inventory & Stock Control
* **Admin Restock Tools:** Created "One-Click" increment/decrement methods (`IncQty`/`DecQty`) to allow rapid stock updates from the product list.
* **Conditional UI:** Developed logic to toggle "In Stock" vs. "Out of Stock" labels and disable purchase buttons dynamically when inventory hits zero.

#### 4. Real-Time Shopping Cart
* **Session-Based State:** Engineered a cart system using JSON-serialized Session storage to maintain user selections without requiring a persistent database login.
* **Optimistic UI Updates:** Integrated **Vanilla JavaScript (AJAX)** to provide instant feedback on quantity changes in the mini-cart, reducing perceived latency.
* **Sync Patterns:** Implemented a "Ghost Trigger" pattern to ensure global elements (navbar badges, sub-totals) stay synchronized with the server-side state.

#### 5. Routing & Layout
* Modified the **Shared Layout** (`_Layout.cshtml`) to include a persistent navigation bar and a dynamic mini-cart modal.
* Utilized **Tag Helpers** to create type-safe links between the Storefront and Administrative views.

---

### 🛠️ Tech Stack
* **Language:** C#
* **Framework:** ASP.NET Core 10.0 (MVC)
* **ORM:** Entity Framework Core
* **Database:** LocalDB / SQL Server
* **Frontend:** Razor Pages (CSHTML), JavaScript (ES6+), Bootstrap 5, Material Symbols
* **State Management:** ASP.NET Core Sessions & JSON Serialization