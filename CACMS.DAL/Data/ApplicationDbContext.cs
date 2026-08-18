using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CACMS.DAL.Entities;
using CACMS.DAL.Entities.Enums;

namespace CACMS.DAL.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<EventType> EventTypes { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Invitation> Invitations { get; set; }
    public DbSet<Participation> Participations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure EventType
        modelBuilder.Entity<EventType>()
            .HasKey(et => et.Id);

        modelBuilder.Entity<EventType>()
            .Property(et => et.Name)
            .IsRequired()
            .HasMaxLength(100);

        // Configure Location
        modelBuilder.Entity<Location>()
            .HasKey(l => l.Id);

        modelBuilder.Entity<Location>()
            .Property(l => l.Name)
            .IsRequired()
            .HasMaxLength(150);

        modelBuilder.Entity<Location>()
            .Property(l => l.Address)
            .IsRequired()
            .HasMaxLength(300);

        modelBuilder.Entity<Location>()
            .Property(l => l.Capacity)
            .IsRequired();

        // Configure Event
        modelBuilder.Entity<Event>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<Event>()
            .Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<Event>()
            .Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(1000);

        modelBuilder.Entity<Event>()
            .Property(e => e.Date)
            .IsRequired();

        modelBuilder.Entity<Event>()
            .Property(e => e.Capacity)
            .IsRequired();

        modelBuilder.Entity<Event>()
            .Property(e => e.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        // Event -> Location (Many Events to One Location)
        modelBuilder.Entity<Event>()
            .HasOne(e => e.Location)
            .WithMany(l => l.Events)
            .HasForeignKey(e => e.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        // Event -> EventType (Many Events to One EventType)
        modelBuilder.Entity<Event>()
            .HasOne(e => e.EventType)
            .WithMany(et => et.Events)
            .HasForeignKey(e => e.EventTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Event -> ApplicationUser/Organizer (Many Events to One Organizer)
        modelBuilder.Entity<Event>()
            .HasOne(e => e.Organizer)
            .WithMany(u => u.OrganizedEvents)
            .HasForeignKey(e => e.OrganizerId)
            .OnDelete(DeleteBehavior.NoAction);

        // Configure Invitation
        modelBuilder.Entity<Invitation>()
            .HasKey(i => i.Id);

        modelBuilder.Entity<Invitation>()
            .Property(i => i.Status)
            .IsRequired()
            .HasDefaultValue(InvitationStatus.Pending);

        modelBuilder.Entity<Invitation>()
            .Property(i => i.SentAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        // Invitation -> Event (Many Invitations to One Event)
        modelBuilder.Entity<Invitation>()
            .HasOne(i => i.Event)
            .WithMany(e => e.Invitations)
            .HasForeignKey(i => i.EventId)
            .OnDelete(DeleteBehavior.NoAction);

        // Invitation -> ApplicationUser/Person (Many Invitations to One Person)
        modelBuilder.Entity<Invitation>()
            .HasOne(i => i.Person)
            .WithMany(u => u.ReceivedInvitations)
            .HasForeignKey(i => i.PersonId)
            .OnDelete(DeleteBehavior.NoAction);

        // Configure Participation
        modelBuilder.Entity<Participation>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Participation>()
            .Property(p => p.SeatNumber)
            .IsRequired()
            .HasMaxLength(10);

        // Participation -> Invitation (One-to-One)
        modelBuilder.Entity<Participation>()
            .HasOne(p => p.Invitation)
            .WithOne(i => i.Participation)
            .HasForeignKey<Participation>(p => p.InvitationId)
            .OnDelete(DeleteBehavior.NoAction);

        // Configure ApplicationUser
        modelBuilder.Entity<ApplicationUser>()
            .Property(u => u.FirstName)
            .HasMaxLength(100);

        modelBuilder.Entity<ApplicationUser>()
            .Property(u => u.LastName)
            .HasMaxLength(100);

        modelBuilder.Entity<ApplicationUser>()
            .Property(u => u.Role)
            .IsRequired()
            .HasDefaultValue(UserRole.Guest);

        // Seed Data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Create Admin user
        var adminUserId = "admin-user-id";
        var adminUser = new ApplicationUser
        {
            Id = adminUserId,
            UserName = "admin@cacms.com",
            Email = "admin@cacms.com",
            EmailConfirmed = true,
            PhoneNumber = "+1234567890",
            PhoneNumberConfirmed = true,
            FirstName = "Admin",
            LastName = "User",
            Role = UserRole.Admin,
            NormalizedUserName = "ADMIN@CACMS.COM",
            NormalizedEmail = "ADMIN@CACMS.COM",
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };

        modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

        // Create Organizer users
        var organizer1Id = "organizer-user-1";
        var organizer1 = new ApplicationUser
        {
            Id = organizer1Id,
            UserName = "organizer1@cacms.com",
            Email = "organizer1@cacms.com",
            EmailConfirmed = true,
            PhoneNumber = "+1234567891",
            PhoneNumberConfirmed = true,
            FirstName = "John",
            LastName = "Organizer",
            Role = UserRole.Organizer,
            NormalizedUserName = "ORGANIZER1@CACMS.COM",
            NormalizedEmail = "ORGANIZER1@CACMS.COM",
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };

        var organizer2Id = "organizer-user-2";
        var organizer2 = new ApplicationUser
        {
            Id = organizer2Id,
            UserName = "organizer2@cacms.com",
            Email = "organizer2@cacms.com",
            EmailConfirmed = true,
            PhoneNumber = "+1234567892",
            PhoneNumberConfirmed = true,
            FirstName = "Jane",
            LastName = "Smith",
            Role = UserRole.Organizer,
            NormalizedUserName = "ORGANIZER2@CACMS.COM",
            NormalizedEmail = "ORGANIZER2@CACMS.COM",
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };

        modelBuilder.Entity<ApplicationUser>().HasData(organizer1, organizer2);

        // Create regular users
        var user1Id = "user-1";
        var user1 = new ApplicationUser
        {
            Id = user1Id,
            UserName = "student1@cacms.com",
            Email = "student1@cacms.com",
            EmailConfirmed = true,
            PhoneNumber = "+1234567893",
            PhoneNumberConfirmed = true,
            FirstName = "Alice",
            LastName = "Student",
            Role = UserRole.Student,
            NormalizedUserName = "STUDENT1@CACMS.COM",
            NormalizedEmail = "STUDENT1@CACMS.COM",
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };

        var user2Id = "user-2";
        var user2 = new ApplicationUser
        {
            Id = user2Id,
            UserName = "teacher1@cacms.com",
            Email = "teacher1@cacms.com",
            EmailConfirmed = true,
            PhoneNumber = "+1234567894",
            PhoneNumberConfirmed = true,
            FirstName = "Bob",
            LastName = "Teacher",
            Role = UserRole.Teacher,
            NormalizedUserName = "TEACHER1@CACMS.COM",
            NormalizedEmail = "TEACHER1@CACMS.COM",
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };

        var user3Id = "user-3";
        var user3 = new ApplicationUser
        {
            Id = user3Id,
            UserName = "student2@cacms.com",
            Email = "student2@cacms.com",
            EmailConfirmed = true,
            PhoneNumber = "+1234567895",
            PhoneNumberConfirmed = true,
            FirstName = "Charlie",
            LastName = "Guest",
            Role = UserRole.Student,
            NormalizedUserName = "STUDENT2@CACMS.COM",
            NormalizedEmail = "STUDENT2@CACMS.COM",
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };

        var user4Id = "user-4";
        var user4 = new ApplicationUser
        {
            Id = user4Id,
            UserName = "student3@cacms.com",
            Email = "student3@cacms.com",
            EmailConfirmed = true,
            PhoneNumber = "+1234567896",
            PhoneNumberConfirmed = true,
            FirstName = "Diana",
            LastName = "Participant",
            Role = UserRole.Student,
            NormalizedUserName = "STUDENT3@CACMS.COM",
            NormalizedEmail = "STUDENT3@CACMS.COM",
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };

        var user5Id = "user-5";
        var user5 = new ApplicationUser
        {
            Id = user5Id,
            UserName = "student4@cacms.com",
            Email = "student4@cacms.com",
            EmailConfirmed = true,
            PhoneNumber = "+1234567897",
            PhoneNumberConfirmed = true,
            FirstName = "Eve",
            LastName = "Attendee",
            Role = UserRole.Student,
            NormalizedUserName = "STUDENT4@CACMS.COM",
            NormalizedEmail = "STUDENT4@CACMS.COM",
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };

        modelBuilder.Entity<ApplicationUser>().HasData(user1, user2, user3, user4, user5);

        // Create EventTypes
        var eventType1 = new EventType { Id = 1, Name = "Conference" };
        var eventType2 = new EventType { Id = 2, Name = "Workshop" };
        var eventType3 = new EventType { Id = 3, Name = "Webinar" };

        modelBuilder.Entity<EventType>().HasData(eventType1, eventType2, eventType3);

        // Create Locations
        var location1 = new Location { Id = 1, Name = "Main Hall", Address = "123 Business Street", Capacity = 500 };
        var location2 = new Location { Id = 2, Name = "Room A", Address = "456 Academy Avenue", Capacity = 100 };
        var location3 = new Location { Id = 3, Name = "Room B", Address = "789 Tech Boulevard", Capacity = 75 };

        modelBuilder.Entity<Location>().HasData(location1, location2, location3);

        // Create Events
        var now = DateTime.UtcNow;
        var event1 = new Event
        {
            Id = 1,
            Title = "Annual Tech Conference 2024",
            Description = "Join us for the biggest tech conference of the year",
            Date = now.AddDays(30),
            LocationId = 1,
            EventTypeId = 1,
            OrganizerId = organizer1Id,
            Capacity = 500,
            CreatedDate = now
        };

        var event2 = new Event
        {
            Id = 2,
            Title = "C# Advanced Workshop",
            Description = "Deep dive into advanced C# features",
            Date = now.AddDays(15),
            LocationId = 2,
            EventTypeId = 2,
            OrganizerId = organizer1Id,
            Capacity = 100,
            CreatedDate = now
        };

        var event3 = new Event
        {
            Id = 3,
            Title = "Web Development Webinar",
            Description = "Learn modern web development practices",
            Date = now.AddDays(7),
            LocationId = 2,
            EventTypeId = 3,
            OrganizerId = organizer2Id,
            Capacity = 100,
            CreatedDate = now
        };

        var event4 = new Event
        {
            Id = 4,
            Title = "Database Design Seminar",
            Description = "Mastering database design patterns",
            Date = now.AddDays(45),
            LocationId = 3,
            EventTypeId = 2,
            OrganizerId = organizer2Id,
            Capacity = 75,
            CreatedDate = now
        };

        var event5 = new Event
        {
            Id = 5,
            Title = "Cloud Architecture Conference",
            Description = "Enterprise cloud architecture solutions",
            Date = now.AddDays(60),
            LocationId = 1,
            EventTypeId = 1,
            OrganizerId = organizer1Id,
            Capacity = 500,
            CreatedDate = now
        };

        modelBuilder.Entity<Event>().HasData(event1, event2, event3, event4, event5);

        // Create Invitations
        var invitation1 = new Invitation
        {
            Id = 1,
            EventId = 1,
            PersonId = user1Id,
            Status = InvitationStatus.Accepted,
            SentAt = now.AddDays(-5)
        };

        var invitation2 = new Invitation
        {
            Id = 2,
            EventId = 1,
            PersonId = user2Id,
            Status = InvitationStatus.Pending,
            SentAt = now.AddDays(-5)
        };

        var invitation3 = new Invitation
        {
            Id = 3,
            EventId = 2,
            PersonId = user3Id,
            Status = InvitationStatus.Accepted,
            SentAt = now.AddDays(-10)
        };

        var invitation4 = new Invitation
        {
            Id = 4,
            EventId = 2,
            PersonId = user4Id,
            Status = InvitationStatus.Rejected,
            SentAt = now.AddDays(-10)
        };

        var invitation5 = new Invitation
        {
            Id = 5,
            EventId = 3,
            PersonId = user5Id,
            Status = InvitationStatus.Accepted,
            SentAt = now.AddDays(-2)
        };

        modelBuilder.Entity<Invitation>().HasData(invitation1, invitation2, invitation3, invitation4, invitation5);

        // Create Participations
        var participation1 = new Participation
        {
            Id = 1,
            InvitationId = 1,
            CheckInTime = now.AddDays(-1),
            SeatNumber = "A001"
        };

        var participation2 = new Participation
        {
            Id = 2,
            InvitationId = 3,
            CheckInTime = now.AddDays(-5),
            SeatNumber = "A001"
        };

        var participation3 = new Participation
        {
            Id = 3,
            InvitationId = 5,
            CheckInTime = null,
            SeatNumber = "A002"
        };

        modelBuilder.Entity<Participation>().HasData(participation1, participation2, participation3);
    }
}
