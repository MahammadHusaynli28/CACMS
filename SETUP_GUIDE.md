# CACMS - Conference Management System Setup Guide

## 🚀 Getting Started

### Prerequisites
- SQL Server LocalDB installed
- .NET 10 SDK installed
- Visual Studio 2022 or higher

### Database Setup

The database will be automatically created on the first run. Follow these steps:

#### Option 1: Automatic (Recommended)
Just run the application - it will automatically:
1. Create the database `CACMS`
2. Apply all migrations
3. Seed sample data

#### Option 2: Manual Database Creation
Open Package Manager Console and run:
```powershell
Update-Database -Project CACMS.DAL
```

### Running the Application

1. **Set Startup Project**: Right-click `CACMS.MVC` → Set as Startup Project
2. **Run**: Press `F5` or Click Start
3. **Access**: Navigate to `https://localhost:5001`

## 🔐 Test Credentials

### Admin Account
- **Email**: admin@cacms.com
- **Password**: password

### Organizer Accounts
- **Email**: organizer1@cacms.com
- **Password**: password

- **Email**: organizer2@cacms.com
- **Password**: password

### Student Accounts
- **Email**: student1@cacms.com
- **Password**: password

- **Email**: student2@cacms.com
- **Password**: password

- **Email**: student3@cacms.com
- **Password**: password

- **Email**: student4@cacms.com
- **Password**: password

### Teacher Account
- **Email**: teacher1@cacms.com
- **Password**: password

## 📊 Database Schema

### Tables
- **AspNetUsers**: User accounts with roles
- **AspNetRoles**: Identity roles
- **Events**: Conference events
- **EventTypes**: Event classifications
- **Locations**: Event venues
- **Invitations**: Event invitations with status
- **Participations**: Check-in records with seat allocation

### Relationships
```
ApplicationUser (1) ──→ (Many) Event (Organizer)
ApplicationUser (1) ──→ (Many) Invitation
Event (1) ──→ (Many) Invitation
Invitation (1) ──→ (0/1) Participation
Event (Many) ──→ (1) Location
Event (Many) ──→ (1) EventType
```

## ✨ Features

### For All Users
- Browse all events
- View event details
- Manage personal invitations
- Accept/Reject invitations
- Check-in to accepted events

### For Organizers
- Create new events
- Edit owned events
- Delete owned events
- Send invitations to events
- View invitation responses
- Track participants

### For Admins
- Full user management
- Create/Edit/Delete locations
- Create/Edit/Delete event types
- View admin dashboard with statistics
- System-wide event management

## 🏗️ Project Architecture

### Layered Architecture
```
CACMS.MVC (Presentation Layer)
    ↓
CACMS.BLL (Business Logic Layer)
    ↓
CACMS.DAL (Data Access Layer)
    ↓
SQL Server Database
```

### Design Patterns
- **Repository Pattern**: Data access abstraction
- **Service Pattern**: Business logic encapsulation
- **DTO Pattern**: Data transfer objects
- **Dependency Injection**: Loose coupling

### Technologies
- ASP.NET Core 8 MVC
- Entity Framework Core 8
- SQL Server LocalDB
- Bootstrap 5
- AutoMapper
- ASP.NET Core Identity

## 🐛 Troubleshooting

### Database Not Updating
```powershell
# Remove existing migrations (if needed)
Remove-Migration -Project CACMS.DAL

# Apply fresh migration
Update-Database -Project CACMS.DAL
```

### Connection String Issues
Check `CACMS.MVC/appsettings.json`:
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CACMS;Trusted_Connection=true;"
}
```

### Port Already in Use
Change the port in `Properties/launchSettings.json`:
```json
"applicationUrl": "https://localhost:5002;http://localhost:5001"
```

## 📝 Default Seed Data

### Users
- 1 Admin
- 2 Organizers
- 5 Students/Users

### Content
- 3 Locations
- 3 Event Types
- 5 Events
- 5 Invitations (Various statuses)

## 🔄 Development Workflow

### Adding New Features
1. Create Entity in `CACMS.DAL/Entities`
2. Update `ApplicationDbContext`
3. Create Migration: `Add-Migration FeatureName -Project CACMS.DAL`
4. Create Repository Interface in `CACMS.DAL/Repositories/Interfaces`
5. Create Repository Implementation in `CACMS.DAL/Repositories/Implementations`
6. Create DTO in `CACMS.BLL/DTOs`
7. Update AutoMapper Profile in `CACMS.BLL/Mapper`
8. Create Service Interface in `CACMS.BLL/Services/Interfaces`
9. Create Service Implementation in `CACMS.BLL/Services/Implementations`
10. Create Controller in `CACMS.MVC/Controllers`
11. Create Views in `CACMS.MVC/Views`

## 📚 Additional Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core)
- [Bootstrap Documentation](https://getbootstrap.com/docs)

---

**System Built**: 2024
**Version**: 1.0.0
**Status**: Production Ready
