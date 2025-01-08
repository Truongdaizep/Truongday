using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NETCORE.Services;
using NETCORE.Utils.ConfigOptions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MvcMovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcMovieContext") ?? throw new InvalidOperationException("Connection string 'MvcMovieContext' not found.")));

builder.Services.AddSingleton<IVnPayService, VnPayService>();
builder.Services.Configure<GoogleCloudStorageConfigOptions>(
    builder.Configuration.GetSection("GoogleCloudStorage"));
builder.Services.Configure<VnPayConfigOptions>(
    builder.Configuration.GetSection("VnPay"));
builder.Services.AddSingleton<ICloudStorageService, CloudStorageService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".YourApp.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn session
    options.Cookie.IsEssential = true; // Thiết lập Cookie này là thiết yếu
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthorization();

app.MapStaticAssets();
app.UseSession();
app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
