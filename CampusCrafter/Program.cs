using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CampusCrafter.Data;
using CampusCrafter.Models;
using CampusCrafter.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
        options.SignIn.RequireConfirmedAccount = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(Roles.GetRoleName(Role.Admin), policyBuilder => policyBuilder.RequireRole(Roles.GetRoleName(Role.Admin)))
    .AddPolicy(Roles.GetRoleName(Role.Department), policyBuilder => policyBuilder.RequireRole(Roles.GetRoleName(Role.Department)))
    .AddPolicy(Roles.GetRoleName(Role.Candidate), policyBuilder => policyBuilder.RequireRole(Roles.GetRoleName(Role.Candidate)));

builder.Services.AddScoped<ApplicationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { Role.Admin, Role.Department, Role.Candidate };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(Roles.GetRoleName(role)))
            await roleManager.CreateAsync(new IdentityRole(Roles.GetRoleName(role)));
    }
}

app.Run();