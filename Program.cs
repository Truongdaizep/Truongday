using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NETCORE.Services;
using NETCORE.Utils.ConfigOptions;
using Microsoft.AspNetCore.Identity;
using NETCORE.Data;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký DbContext với Entity Framework Core
builder.Services.AddDbContext<MvcMovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcMovieContext") ?? throw new InvalidOperationException("Connection string 'MvcMovieContext' not found.")));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcMovieContext")));
// Đăng ký Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Đăng ký các dịch vụ tùy chỉnh
builder.Services.AddSingleton<IVnPayService, VnPayService>();
builder.Services.AddSingleton<ICloudStorageService, CloudStorageService>();

// Cấu hình các tùy chọn từ appsettings.json
builder.Services.Configure<GoogleCloudStorageConfigOptions>(builder.Configuration.GetSection("GoogleCloudStorage"));
builder.Services.Configure<VnPayConfigOptions>(builder.Configuration.GetSection("VnPay"));

// Cấu hình Session
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".YourApp.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});

// Cấu hình các tùy chọn cho Identity
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

// Cấu hình cookie cho Identity
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

// Thêm các dịch vụ Razor Pages và MVC
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// Tạo ứng dụng
var app = builder.Build();

// Sử dụng các phương thức cấu hình cho middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    if (app.Environment.IsProduction())
    {
        app.UseHsts();
    }
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthorization();
app.UseSession();

// Định tuyến các controller và page
app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
