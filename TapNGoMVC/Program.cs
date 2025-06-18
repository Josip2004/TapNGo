using Microsoft.EntityFrameworkCore;
using TapNGo.DAL.Models;
using TapNGo.DAL.Repositories.Categories;
using TapNGo.DAL.Repositories.MenuItems;
using TapNGo.DAL.Repositories.Orders;
using TapNGo.DAL.Repositories.Reviews;
using TapNGo.DAL.Repositories.Users;
using TapNGo.DAL.Services.CategoryService;
using TapNGo.DAL.Services.MenuItemService;
using TapNGo.DAL.Services.OrderService;
using TapNGo.DAL.Services.ReviewService;
using TapNGo.DAL.Services.UserService;
using TapNGo.DAL.SessionServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

builder.Services.AddAuthentication()
  .AddCookie(options =>
  {
      options.LoginPath = "/User/Login";
      options.LogoutPath = "/User/Logout";
      options.AccessDeniedPath = "/User/Forbidden";
      options.SlidingExpiration = true;
      options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
  });

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<TapNgoV1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IMenuItemService, MenuItemService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartItemService, CartService>();

builder.Services.AddSignalR();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Category}/{action=Index}/{id?}");

app.Run();
