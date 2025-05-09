using System.Reflection;
using EMA.DB.Contexts;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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

// MVCアプリの設定
builder.Services.AddControllersWithViews();
builder.Services.AddMvc(options =>
{
    options.EnableEndpointRouting = false;
});
builder.Services.AddControllers().AddNewtonsoftJson();

// Cookieポリシーの設定
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.HttpOnly = HttpOnlyPolicy.Always;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

// ルーティングの設定
builder.Services.Configure<RouteOptions>(options =>
{
    // ルーティングに小文字を許可する
    options.LowercaseUrls = true;
});

// Swaggerの設定
// https://learn.microsoft.com/ja-jp/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-8.0&tabs=visual-studio-code
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("sample", new OpenApiInfo()
    {
        Version = "sample",
        Title = "efcore-multidb-app",
        Description = "Entity Framework Core を使用して複数のデータベースを切り替えるサンプル",
    });

    options.EnableAnnotations();

    // アノテーションコメントをXMLに出力し、Swaggerドキュメントとして反映させる
    // (.csprojでGenerateDocumentationFileをtrueにする必要がある)
    var xmlFile = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
})
.AddSwaggerGenNewtonsoftSupport();

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

// Swaggerのミドルウェアを適用
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/sample/swagger.json", "サンプル");
    options.DisplayRequestDuration();
});

app.Run();
