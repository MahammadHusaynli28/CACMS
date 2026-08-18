using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CACMS.BLL.Mapper;
using CACMS.BLL.Services.Interfaces;
using CACMS.BLL.Services.Implementations;
using CACMS.DAL.Data;
using CACMS.DAL.Entities;
using CACMS.DAL.Repositories.Interfaces;
using CACMS.DAL.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Register Repositories
builder.Services.AddScoped<IGenericRepository<Event>, GenericRepository<Event>>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IInvitationRepository, InvitationRepository>();
builder.Services.AddScoped<IParticipationRepository, ParticipationRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IEventTypeRepository, EventTypeRepository>();

// Register Services
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IEventTypeService, EventTypeService>();
builder.Services.AddScoped<IInvitationService, InvitationService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

// Add MVC services
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Apply migrations and seed data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
