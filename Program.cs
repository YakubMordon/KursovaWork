using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using KursovaWorkBLL.Services.AdditionalServices;
using KursovaWorkBLL.Services.MainServices.CardService;
using KursovaWorkBLL.Services.MainServices.CarService;
using KursovaWorkBLL.Services.MainServices.OrderService;
using KursovaWorkBLL.Services.MainServices.UserService;
using KursovaWorkDAL.Entity;
using KursovaWorkDAL.Repositories.UserRepository;
using KursovaWorkDAL.Repositories.CarRepository;
using KursovaWorkDAL.Repositories.CardRepository;
using KursovaWorkDAL.Repositories.OrderRepository;


System.Console.OutputEncoding = System.Text.Encoding.UTF8;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CarSaleContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("KursovaWorkContext"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IDRetriever>();

builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<ICarRepository,CarRepository>();
builder.Services.AddScoped<ICardRepository,CardRepository>();
builder.Services.AddScoped<IOrderRepository,OrderRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ICardService,CardService>();
builder.Services.AddScoped<IOrderService,OrderService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.LoginPath = "/Home/Index";
    options.LogoutPath = "/Home/Index";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<CarSaleContext>();

    dbContext.FillDB();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
