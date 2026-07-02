using aspnet.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<CarBrand> CarBrands { get; set; }
    public DbSet<CarModel> CarModels { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarCustomer> CarCustomers { get; set; }
    public DbSet<CarReservation> CarReservations { get; set; }
}