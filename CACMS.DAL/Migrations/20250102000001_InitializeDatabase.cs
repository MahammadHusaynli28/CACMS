using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CACMS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false, defaultValue: 4),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    EventTypeId = table.Column<int>(type: "int", nullable: false),
                    OrganizerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_AspNetUsers_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Events_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Events_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_AspNetUsers_PersonId",
                        column: x => x.PersonId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Invitations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Participations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvitationId = table.Column<int>(type: "int", nullable: false),
                    CheckInTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SeatNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participations_Invitations_InvitationId",
                        column: x => x.InvitationId,
                        principalTable: "Invitations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "admin-user-id", 0, "0ee64acb-95ea-40a9-9096-32cb00bf72e5", "admin@cacms.com", true, "Admin", "User", false, null, "ADMIN@CACMS.COM", "ADMIN@CACMS.COM", null, "+1234567890", true, "ed8a3b63-2490-47f9-98bb-5c2d4516caee", false, "admin@cacms.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "organizer-user-1", 0, "d6adc468-d3f4-4a8b-80fa-3869d5bc676e", "organizer1@cacms.com", true, "John", "Organizer", false, null, "ORGANIZER1@CACMS.COM", "ORGANIZER1@CACMS.COM", null, "+1234567891", true, 1, "d495ff3d-2fde-4c47-9a07-55b35d1f9b1c", false, "organizer1@cacms.com" },
                    { "organizer-user-2", 0, "a0683c76-66b4-46e6-b0bd-45ad847bab51", "organizer2@cacms.com", true, "Jane", "Smith", false, null, "ORGANIZER2@CACMS.COM", "ORGANIZER2@CACMS.COM", null, "+1234567892", true, 1, "6b842827-8dc5-4d9c-8696-977ffbfadea2", false, "organizer2@cacms.com" },
                    { "user-1", 0, "c602f128-c04d-4c19-879d-99d27eb95b8f", "student1@cacms.com", true, "Alice", "Student", false, null, "STUDENT1@CACMS.COM", "STUDENT1@CACMS.COM", null, "+1234567893", true, 3, "ad34e281-2d73-4475-9567-c6bc2d2a8d79", false, "student1@cacms.com" },
                    { "user-2", 0, "89571d0e-f469-4c5c-a7f3-a735a7398958", "teacher1@cacms.com", true, "Bob", "Teacher", false, null, "TEACHER1@CACMS.COM", "TEACHER1@CACMS.COM", null, "+1234567894", true, 2, "667e8955-ddbe-4022-b394-2b8deab1954c", false, "teacher1@cacms.com" },
                    { "user-3", 0, "733699b2-206b-4167-b2bb-2a70d248cdb9", "student2@cacms.com", true, "Charlie", "Guest", false, null, "STUDENT2@CACMS.COM", "STUDENT2@CACMS.COM", null, "+1234567895", true, 3, "fe3b0770-a706-4861-a247-d5191a2b304a", false, "student2@cacms.com" },
                    { "user-4", 0, "818310ed-e172-4665-81c3-07ddebc3e828", "student3@cacms.com", true, "Diana", "Participant", false, null, "STUDENT3@CACMS.COM", "STUDENT3@CACMS.COM", null, "+1234567896", true, 3, "28c6fc79-55f2-4bb0-89a5-d6f0318b86b7", false, "student3@cacms.com" },
                    { "user-5", 0, "e5df68be-9389-48cd-bf7a-6e2cef4a8dd8", "student4@cacms.com", true, "Eve", "Attendee", false, null, "STUDENT4@CACMS.COM", "STUDENT4@CACMS.COM", null, "+1234567897", true, 3, "cd79cd60-3b81-420a-b823-3219f27ad5f6", false, "student4@cacms.com" }
                });

            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Conference" },
                    { 2, "Workshop" },
                    { 3, "Webinar" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "Capacity", "Name" },
                values: new object[,]
                {
                    { 1, "123 Business Street", 500, "Main Hall" },
                    { 2, "456 Academy Avenue", 100, "Room A" },
                    { 3, "789 Tech Boulevard", 75, "Room B" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Capacity", "CreatedDate", "Date", "Description", "EventTypeId", "LocationId", "OrganizerId", "Title" },
                values: new object[,]
                {
                    { 1, 500, new DateTime(2026, 7, 2, 13, 50, 15, 773, DateTimeKind.Utc).AddTicks(3196), new DateTime(2026, 8, 1, 13, 50, 15, 773, DateTimeKind.Utc).AddTicks(3196), "Join us for the biggest tech conference of the year", 1, 1, "organizer-user-1", "Annual Tech Conference 2024" },
                    { 2, 100, new DateTime(2026, 7, 2, 13, 50, 15, 773, DateTimeKind.Utc).AddTicks(3196), new DateTime(2026, 7, 17, 13, 50, 15, 773, DateTimeKind.Utc).AddTicks(3196), "Deep dive into advanced C# features", 2, 2, "organizer-user-1", "C# Advanced Workshop" },
                    { 3, 100, new DateTime(2026, 7, 2, 13, 50, 15, 773, DateTimeKind.Utc).AddTicks(3196), new DateTime(2026, 7, 9, 13, 50, 15, 773, DateTimeKind.Utc).AddTicks(3196), "Learn modern web development practices", 3, 2, "organizer-user-2", "Web Development Webinar" },
                    { 4, 75, new DateTime(2026, 7, 2, 13, 50, 15, 773, DateTimeKind.Utc).AddTicks(3196), new DateTime(2026, 8, 16, 13, 50, 15, 773, DateTimeKind.Utc).AddTicks(3196), "Mastering database design patterns", 2, 3, "organizer-user-2", "Database Design Seminar" },
                    { 5, 500, new DateTime(2026, 7, 2, 13, 50, 15, 773, DateTimeKind.Utc).AddTicks(3196), new DateTime(2026, 8, 31, 13, 50, 15, 773, DateTimeKind.Utc).AddTicks(3196), "Enterprise cloud architecture solutions", 1, 1, "organizer-user-1", "Cloud Architecture Conference" }
                });

            migrationBuilder.InsertData(
                table: "Invitations",
                columns: new[] { "Id", "EventId", "PersonId", "SentAt", "Status" },
                values: new object[] { 1, 1, "user-1", new DateTime(2026, 6, 27, 13, 50, 15, 773, DateTimeKind.Utc).AddTicks(3196), 1 });

            migrationBuilder.InsertData(
                table: "Invitations",
                columns: new[] { "Id", "EventId", "PersonId", "SentAt" },
                values: new object[] { 2, 1, "user-2", new DateTime(2026, 6, 27, 13, 50, 15, 773, DateTimeKind.Utc).AddTicks(3196) });

            migrationBuilder.InsertData(
                table: "Invitations",
                columns: new[] { "Id", "EventId", "PersonId", "SentAt", "Status" },
                values: new object[,]
                {
                    { 3, 2, "user-3", new DateTime(2026, 6, 22, 13, 50, 15, 773, DateTimeKind.Utc).AddTicks(3196), 1 },
                    { 4, 2, "user-4", new DateTime(2026, 6, 22, 13, 50, 15, 773, DateTimeKind.Utc).AddTicks(3196), 2 },
                    { 5, 3, "user-5", new DateTime(2026, 6, 30, 13, 50, 15, 773, DateTimeKind.Utc).AddTicks(3196), 1 }
                });

            migrationBuilder.InsertData(
                table: "Participations",
                columns: new[] { "Id", "CheckInTime", "InvitationId", "SeatNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 7, 1, 13, 50, 15, 773, DateTimeKind.Utc).AddTicks(3196), 1, "A001" },
                    { 2, new DateTime(2026, 6, 27, 13, 50, 15, 773, DateTimeKind.Utc).AddTicks(3196), 3, "A001" },
                    { 3, null, 5, "A002" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventTypeId",
                table: "Events",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_LocationId",
                table: "Events",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_OrganizerId",
                table: "Events",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_EventId",
                table: "Invitations",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_PersonId",
                table: "Invitations",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_InvitationId",
                table: "Participations",
                column: "InvitationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Participations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EventTypes");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
