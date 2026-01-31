# Restaurant Order Management System

A console-based **Restaurant Order Management System** built with **C#**, **.NET 6**, **Entity Framework Core**, and **AutoMapper**.  
This system allows managing menu items and customer orders, including adding, editing, removing, and viewing details.

---

## Features

### Menu Management
- Add new menu items
- Edit existing menu items
- Remove menu items
- View all menu items
- Filter menu items by category
- Filter menu items by price range
- Search menu items by name

### Order Management
- Create new orders with multiple menu items
- Cancel orders
- View all orders
- Filter orders by date interval
- Filter orders by total price range
- View orders by specific date
- View order details by Id

---

## Technologies Used
- **.NET 6**
- **C#**
- **Entity Framework Core**
- **AutoMapper**
- **SQL Server** (LocalDB or SQLExpress)
- **Dependency Injection** with Microsoft.Extensions.DependencyInjection
- **Console Application** (CLI interface)

---

## Getting Started

### Prerequisites
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- SQL Server Express or LocalDB

### Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/restaurant-order-system.git
   cd restaurant-order-system
Configure database connection in Program.cs:

options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=RestaurantDb;Trusted_Connection=True;TrustServerCertificate=True")
Run the application:

dotnet run
The database will automatically be created and seeded if needed.

Usage
On running the app, a console menu is displayed:

--- Restaurant Management ---
1. Menu Operations
2. Orders Operations
0. Exit
Navigate through menu operations and order operations to manage items and orders.

Project Structure
Restaurant/
│
├─ BLL/              # Business Logic Layer
│  ├─ Dtos/          # Data Transfer Objects for MenuItem, Order, and OrderItem
│  ├─ Services/      # Service classes for MenuItem and Order management
│  └─ Interfaces/    # Service interfaces
│
├─ DAL/              # Data Access Layer
│  ├─ Data/          # DbContext
│
├─ Core/             # Core Models
│
├─ Profiles/         # AutoMapper profiles
│
└─ Program.cs        # Main application entry
AutoMapper
The project uses AutoMapper to map between entities and DTOs:

MenuItemCreateDto ↔ MenuItem

OrderCreateDto ↔ Order

OrderItemCreateDto ↔ OrderItem

Database Migrations
The project uses EF Core migrations for database setup and updates:

dotnet ef migrations add InitialCreate
dotnet ef database update
Contributing
Contributions are welcome!

Fork the repository

Create a new branch (git checkout -b feature/FeatureName)

Commit changes (git commit -m 'Add new feature')

Push to branch (git push origin feature/FeatureName)

Create a Pull Request

License
This project is licensed under the MIT License.
