# Baiomy.FCI2

A modern web application for managing departments, employees, users, and roles built with ASP.NET Core Razor Pages and .NET 8.

## Overview

Baiomy.FCI2 is an enterprise-level management system designed to streamline operations related to department and employee administration. The application provides a user-friendly interface with role-based access control, allowing administrators to manage users, assign roles, and oversee employee and department data efficiently.

## Features

- **Department Management**: Create, read, update, and delete department information
- **Employee Management**: Manage employee records and assignments
- **User Management**: Administrative control over user accounts
- **Role-Based Access Control**: Implement role-based authorization for different user types
- **Authentication & Authorization**: Secure sign-in/sign-out functionality
- **Responsive Design**: Mobile-friendly interface with Bootstrap 5
- **Modern UI**: Beautiful glassmorphism navbar with smooth animations

## Technology Stack

- **.NET 8**: Latest .NET framework
- **ASP.NET Core Razor Pages**: Web application framework
- **Entity Framework Core**: ORM for database operations
- **Bootstrap 5**: Responsive UI framework
- **Font Awesome**: Icon library
- **C# 12.0**: Programming language

## Project Structure

```
Baiomy.FCI2/
├── Baiomy.FCI2.PL/          # Presentation Layer (Razor Pages)
│   ├── Controllers/          # Page controllers
│   ├── Views/               # Razor pages and layouts
│   ├── Pages/               # Razor page files
│   └── Program.cs           # Application startup configuration
├── Baiomy.FCI2.DAL/         # Data Access Layer
│   ├── Persistence/
│   │   └── Data/            # DbContext and seeding
│   └── Repositories/        # Data access patterns
└── README.md
```

## Prerequisites

- .NET 8 SDK or later
- SQL Server (or configured database provider)
- Visual Studio 2022 or later (or VS Code)
- Git

## Installation & Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/MohammedBaioumy/Baiomy.FCI2.git
   cd Baiomy.FCI2
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Configure the database**
   - Update the connection string in `appsettings.json` in the `Baiomy.FCI2.PL` project
   - Example:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=BaiomyFCI2Db;Trusted_Connection=true;"
     }
     ```

4. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run --project Baiomy.FCI2.PL
   ```
   The application will be available at `https://localhost:7001` (or the configured port)

## Usage

### Navigation
- **Home**: Landing page with general information
- **Privacy**: Privacy policy
- **Departments**: View and manage all departments
- **Employees**: View and manage all employees
- **Users** (Admin): Manage user accounts (Admin role only)
- **Roles** (Admin): Manage user roles (Admin role only)
- **Sign Out**: Secure logout

### User Roles

The application supports role-based access control:
- **Admin**: Full access to all features including user and role management
- **Employee**: Standard access to view information

## Architecture

The application follows a layered architecture pattern:

- **Presentation Layer (PL)**: Handles UI and user interactions using Razor Pages
- **Data Access Layer (DAL)**: Manages database operations through Entity Framework Core
- **Database**: Stores application data

## Key Components

### Controllers
- `AccountController`: Authentication and account management
- `DepartmentController`: Department CRUD operations
- `EmployeeController`: Employee CRUD operations
- `UserController`: User management
- `RoleController`: Role management

### Views
- `_Layout.cshtml`: Master layout template with responsive navigation
- Shared components and partial views for common UI elements

### Data
- `BaiomyFCI2DbContext`: Entity Framework Core context
- `BaiomyFCI2DbContextSeed`: Database seeding for initial data

## Styling & Customization

The application uses a modern gradient background and glassmorphism effects:
- Primary colors: Purple (#4A0A8F) and Teal (#11CBA3)
- Background gradient: Light blue to light pink
- Responsive navbar with blur effects

CSS customizations can be found in `_Layout.cshtml` and `site.css`.

## Database Seeding

Initial data is populated through the `BaiomyFCI2DbContextSeed` class. This includes default users, roles, departments, and employees.

## Security

- User authentication is required for all operations
- Role-based authorization controls feature access
- Secure sign-out functionality
- Password security best practices implemented

## Future Enhancements

- [ ] Add email notifications
- [ ] Implement audit logging
- [ ] Add report generation
- [ ] Enhance filtering and search capabilities
- [ ] Add data export functionality

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is open source and available under the MIT License.

## Support

For issues and questions, please visit the [GitHub repository](https://github.com/MohammedBaioumy/Baiomy.FCI2).

---

**Built with ❤️ using .NET 8 and ASP.NET Core Razor Pages**
