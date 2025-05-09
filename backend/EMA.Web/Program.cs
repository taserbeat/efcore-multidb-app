using EMA.DB.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// データベース接続設定
var dbProvider = builder.Configuration["DbProvider"];
string? connectionStr = null;
switch (dbProvider)
{
    case "PostgreSQL":
        connectionStr = builder.Configuration.GetConnectionString("PostgreSQL");
        builder.Services.AddDbContext<EmaDbContextBase, EmaPostgresContext>((options) =>
        {
            options.UseNpgsql(connectionStr);
        });
        break;

    case "SQLServer":
        connectionStr = builder.Configuration.GetConnectionString("SQLServer");
        builder.Services.AddDbContext<EmaDbContextBase, EmaSqlServerContext>((options) =>
        {
            options.UseSqlServer(connectionStr);
        });
        break;

    default:
        throw new InvalidOperationException($"Unknown DbProvider: {dbProvider}");
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.Run();
