using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyDevBox.Areas.Identity.Data;
using MyDevBox.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DevBoxDbContextConnection") ?? throw new InvalidOperationException("Connection string 'DevBoxDbContextConnection' not found.");

builder.Services.AddDbContext<DevBoxDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<MyDevBoxUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<DevBoxDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase=false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
