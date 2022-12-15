using IssueTracker_CoreServices.Data;
using IssueTracker_CoreServices.Models;
using IssueTracker_CoreServices.Service;
using IssueTracker_CoreServices.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;

var builder = WebApplication.CreateBuilder(args);

// custom configuration root
IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory() + "/../Common/")
                   .AddJsonFile("appsettings.json")
                   .Build();

// Add services to the container.
var connectionString = configuration.GetConnectionString("default");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDefaultIdentity<IssueTrackerUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IServiceBase<Project, int>, ProjectService>();
builder.Services.AddScoped<IServiceBase<Issue, int>, IssueService>();
builder.Services.AddScoped<IServiceBase<IssueTrackerUser, string>, UsersService>();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
    if (dbContext != null)
        dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
