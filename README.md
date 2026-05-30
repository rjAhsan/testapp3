# ASP.NET 8 CRUD App — Deployment Guide

## Prerequisites
Install these on your local machine before running the project:

1. **.NET 8 SDK** → https://dotnet.microsoft.com/download/dotnet/8.0
2. **MS SQL Server** (Express is free) → https://www.microsoft.com/en-us/sql-server/sql-server-downloads
3. **SQL Server Management Studio (SSMS)** (optional but recommended) → https://aka.ms/ssmsfullsetup
4. **Visual Studio 2022** (or VS Code with C# extension)

---

## Step-by-Step Deployment

### Step 1 — Configure the Connection String

Open `appsettings.json` and update the connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=ProductCrudDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Replace `YOUR_SERVER_NAME` with your SQL Server instance name.
- If you installed **SQL Server Express**, it is usually: `.\SQLEXPRESS` or `localhost\SQLEXPRESS`
- If you installed the **default SQL Server instance**, use: `.\` or `localhost`

**Example:**
```json
"DefaultConnection": "Server=.\SQLEXPRESS;Database=ProductCrudDb;Trusted_Connection=True;TrustServerCertificate=True;"
```

---

### Step 2 — Restore NuGet Packages

Open a terminal / command prompt in the project folder and run:

```bash
dotnet restore
```

---

### Step 3 — Create the Database & Apply Migrations

Run this command to create the database and tables automatically:

```bash
dotnet ef database update
```

> This will create the `ProductCrudDb` database in SQL Server with the `Products` table and 3 sample records.

If you get "command not found", install the EF Core tools first:
```bash
dotnet tool install --global dotnet-ef
```
Then run `dotnet ef database update` again.

---

### Step 4 — Run the Application

```bash
dotnet run
```

You will see output like:
```
Now listening on: https://localhost:5001
Now listening on: http://localhost:5000
```

Open your browser and go to → **http://localhost:5000**

---

### Step 5 — Using Visual Studio (Alternative)

1. Open `CrudApp.csproj` or the folder in **Visual Studio 2022**
2. Open **Package Manager Console** (Tools → NuGet → Package Manager Console)
3. Run: `Update-Database`
4. Press **F5** or click the green **Run** button

---

## Project Structure

```
CrudApp/
├── Controllers/
│   ├── HomeController.cs       # Redirects to products
│   └── ProductController.cs    # Full CRUD operations
├── Data/
│   └── ApplicationDbContext.cs # EF Core DbContext
├── Migrations/                 # Auto-generated DB migrations
├── Models/
│   └── Product.cs              # Product entity model
├── Views/
│   ├── Product/
│   │   ├── Index.cshtml        # List all products
│   │   ├── Create.cshtml       # Add new product
│   │   ├── Edit.cshtml         # Edit product
│   │   ├── Details.cshtml      # View product details
│   │   └── Delete.cshtml       # Confirm delete
│   └── Shared/
│       └── _Layout.cshtml      # Main layout template
├── wwwroot/
│   ├── css/site.css            # Custom styles
│   └── js/site.js              # Custom scripts
├── appsettings.json            # Configuration (connection string here!)
├── Program.cs                  # App entry point & DI setup
└── CrudApp.csproj              # Project file
```

---

## Features

- ✅ Create new products
- ✅ Read / list all products with search & filter
- ✅ Update existing products
- ✅ Delete products with confirmation
- ✅ Sort by Name or Price
- ✅ Filter by Category
- ✅ Search by Name or Description
- ✅ Dashboard stats (total products, total value, categories)
- ✅ Form validation (client-side + server-side)
- ✅ Success/error toast messages
- ✅ Responsive Bootstrap 5 UI

---

## Troubleshooting

| Problem | Solution |
|--------|---------|
| Cannot connect to SQL Server | Check your server name in `appsettings.json` |
| `dotnet ef` not found | Run `dotnet tool install --global dotnet-ef` |
| Port already in use | Change port in `Properties/launchSettings.json` |
| SSL certificate error | Add `TrustServerCertificate=True` to connection string |
