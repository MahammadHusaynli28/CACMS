# CACMS - Quick Start Guide

## ⚡ 30-Second Setup

1. **Open Solution**: `CACMS.sln` in Visual Studio 2022
2. **Set Startup Project**: Right-click `CACMS.MVC` → "Set as Startup Project"
3. **Run**: Press `F5`
4. **Database**: Automatically created on first run
5. **Login**: Use test credentials below

## 🔑 Test Accounts

| Role | Email | Password |
|------|-------|----------|
| Admin | admin@cacms.com | password |
| Organizer | organizer1@cacms.com | password |
| Student | student1@cacms.com | password |
| Teacher | teacher1@cacms.com | password |

## 📍 Key Pages

| Page | URL | Role |
|------|-----|------|
| Home | `/` | All |
| Events | `/Event/Index` | All |
| My Invitations | `/Invitation/Index` | All |
| My Events | `/Event/MyEvents` | Organizer |
| Admin Dashboard | `/Dashboard/Index` | Admin |
| Locations | `/Location/Index` | Admin |
| Event Types | `/EventType/Index` | Admin |

## 🗂️ Project Structure

```
CACMS/
├── CACMS.DAL/          # Database layer
├── CACMS.BLL/          # Business logic
└── CACMS.MVC/          # Web application
```

## 🔧 Common Tasks

### Create New Event (Organizer)
1. Login as Organizer
2. Click "My Events"
3. Click "+ Create Event"
4. Fill form and submit

### Send Invitation (Organizer)
1. Navigate to event
2. Click "Manage Invitations"
3. Click "+ Send Invitation"
4. Enter user ID and submit

### Accept/Reject Invitation (User)
1. Click "My Invitations"
2. Click "View"
3. Click "Accept" or "Reject"

### Check-In (User)
1. Go to "My Invitations"
2. Find accepted invitation
3. Click "Check In"
4. View assigned seat number

## 💾 Database Info

- **Type**: SQL Server LocalDB
- **Name**: CACMS
- **Location**: `(localdb)\mssqllocaldb`
- **Connection String**: See `appsettings.json`

## 🆘 Troubleshooting

| Issue | Solution |
|-------|----------|
| Database not created | Run app - auto-created on startup |
| Login fails | Check credentials, ensure DB migrated |
| Port conflict | Change port in `launchSettings.json` |
| Missing tables | Run `Update-Database -Project CACMS.DAL` |

## 📚 File Locations

| File | Purpose | Location |
|------|---------|----------|
| Database Config | Connection String | `CACMS.MVC/appsettings.json` |
| DI Setup | Service Registration | `CACMS.MVC/Program.cs` |
| Entity Models | Data Models | `CACMS.DAL/Entities/` |
| API Endpoints | Services | `CACMS.BLL/Services/` |
| UI Views | Page Templates | `CACMS.MVC/Views/` |

## 🎯 Feature Overview

### Events ✅
- Create, Edit, Delete events
- Filter by type, date, organizer
- View participants
- Manage capacity

### Invitations ✅
- Send to users
- Track status (Pending/Accepted/Rejected)
- Prevent re-accepting
- View responses

### Check-In ✅
- Only from accepted invitations
- Auto seat assignment
- One per person per event
- Timestamp tracking

### Admin ✅
- Dashboard with statistics
- Manage locations
- Manage event types
- System monitoring

## 🔐 Authentication

- **Type**: Cookie-based
- **Provider**: ASP.NET Core Identity
- **Roles**: 5 role types supported
- **Login**: `/Account/Login`
- **Register**: `/Account/Register`

## 📊 Sample Data Included

- 1 Admin user
- 2 Organizer users
- 5 Student/Teacher users
- 3 Locations
- 3 Event Types
- 5 Sample Events
- 5 Sample Invitations

## 🚀 Ready to Deploy?

For production deployment, update:
1. `appsettings.json` - Production connection string
2. `Program.cs` - Security settings (if needed)
3. Configure backups
4. Enable logging
5. Set up monitoring

## 📖 Documentation

- **Setup Guide**: `SETUP_GUIDE.md`
- **System Docs**: `SYSTEM_DOCUMENTATION.md`
- **This File**: `QUICK_START.md`

## ✅ System Status

- [x] Build: **SUCCESSFUL**
- [x] Database: **READY**
- [x] Features: **COMPLETE**
- [x] Security: **CONFIGURED**
- [x] Documentation: **PROVIDED**
- [x] Ready to Use: **YES**

---

**Happy Coding! 🎉**

For detailed documentation, see `SYSTEM_DOCUMENTATION.md`
For setup instructions, see `SETUP_GUIDE.md`
