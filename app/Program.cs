using aspnet.Data;
using aspnet.Interfaces;
using Microsoft.EntityFrameworkCore;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=locationvoiture.db"));
builder.Services.AddScoped<ICarBrandRepository, CarBrandSqlliteRepository>();
builder.Services.AddScoped<ICarModelRepository, CarModelSqlliteRepository>();
builder.Services.AddScoped<ICarRepository, CarSqlliteRepository>();
builder.Services.AddScoped<ICarCustomerRepository, CarCustomerSqlliteRepository>();
builder.Services.AddScoped<ICarReservationRepository, CarReservationSqlliteRepository>();
builder.Services.AddHealthChecks();

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    DbSeeder.Seed(db);
}

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

app.UseHttpMetrics();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHealthChecks("/health");

app.MapMetrics();

app.Run();
