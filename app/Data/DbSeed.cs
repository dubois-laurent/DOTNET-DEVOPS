using aspnet.Models;

namespace aspnet.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            SeedBrandsAndModels(context);
            SeedCars(context);
            SeedCustomers(context);
            SeedReservations(context);
        }

        private static void SeedBrandsAndModels(AppDbContext context)
        {
            if (context.CarBrands.Any()) return;

            var brands = new List<CarBrand>
            {
                new() { Name = "Toyota",        CarModels = new() { new() { Name = "Corolla" },   new() { Name = "Yaris" },    new() { Name = "RAV4" } } },
                new() { Name = "Volkswagen",    CarModels = new() { new() { Name = "Golf" },      new() { Name = "Polo" },     new() { Name = "Passat" } } },
                new() { Name = "BMW",           CarModels = new() { new() { Name = "Série 3" },   new() { Name = "Série 5" },  new() { Name = "X5" } } },
                new() { Name = "Mercedes-Benz", CarModels = new() { new() { Name = "Classe C" },  new() { Name = "Classe E" }, new() { Name = "GLC" } } },
                new() { Name = "Peugeot",       CarModels = new() { new() { Name = "208" },       new() { Name = "308" },      new() { Name = "3008" } } },
                new() { Name = "Renault",       CarModels = new() { new() { Name = "Clio" },      new() { Name = "Mégane" },   new() { Name = "Kadjar" } } },
                new() { Name = "Ford",          CarModels = new() { new() { Name = "Focus" },     new() { Name = "Fiesta" },   new() { Name = "Kuga" } } },
                new() { Name = "Honda",         CarModels = new() { new() { Name = "Civic" },     new() { Name = "CR-V" },     new() { Name = "Jazz" } } },
                new() { Name = "Audi",          CarModels = new() { new() { Name = "A3" },        new() { Name = "A4" },       new() { Name = "Q5" } } },
                new() { Name = "Citroën",       CarModels = new() { new() { Name = "C3" },        new() { Name = "C4" },       new() { Name = "C5 Aircross" } } },
            };

            context.CarBrands.AddRange(brands);
            context.SaveChanges();
        }

        private static void SeedCars(AppDbContext context)
        {
            if (context.Cars.Any()) return;

            var models = context.CarModels.ToDictionary(m => m.Name);

            var cars = new List<Car>
            {
                new() { PlateNumber = "FY-945-NT", CarModelId = models["Corolla"].Id      },
                new() { PlateNumber = "AB-123-CD", CarModelId = models["Yaris"].Id        },
                new() { PlateNumber = "BC-234-DE", CarModelId = models["RAV4"].Id         },
                new() { PlateNumber = "CD-345-EF", CarModelId = models["Golf"].Id         },
                new() { PlateNumber = "DE-456-FG", CarModelId = models["Polo"].Id         },
                new() { PlateNumber = "EF-567-GH", CarModelId = models["Passat"].Id       },
                new() { PlateNumber = "FG-678-HI", CarModelId = models["Série 3"].Id      },
                new() { PlateNumber = "GH-789-IJ", CarModelId = models["Série 5"].Id      },
                new() { PlateNumber = "HI-890-JK", CarModelId = models["X5"].Id           },
                new() { PlateNumber = "IJ-901-KL", CarModelId = models["Classe C"].Id     },
                new() { PlateNumber = "JK-012-LM", CarModelId = models["Classe E"].Id     },
                new() { PlateNumber = "KL-123-MN", CarModelId = models["GLC"].Id          },
                new() { PlateNumber = "LM-234-NO", CarModelId = models["208"].Id          },
                new() { PlateNumber = "MN-345-OP", CarModelId = models["308"].Id          },
                new() { PlateNumber = "NO-456-PQ", CarModelId = models["3008"].Id         },
                new() { PlateNumber = "OP-567-QR", CarModelId = models["Clio"].Id         },
                new() { PlateNumber = "PQ-678-RS", CarModelId = models["Mégane"].Id       },
                new() { PlateNumber = "QR-789-ST", CarModelId = models["Kadjar"].Id       },
                new() { PlateNumber = "RS-890-TU", CarModelId = models["Focus"].Id        },
                new() { PlateNumber = "ST-901-UV", CarModelId = models["Fiesta"].Id       },
                new() { PlateNumber = "TU-012-VW", CarModelId = models["Kuga"].Id         },
                new() { PlateNumber = "UV-123-WX", CarModelId = models["Civic"].Id        },
                new() { PlateNumber = "VW-234-XY", CarModelId = models["CR-V"].Id         },
                new() { PlateNumber = "WX-345-YZ", CarModelId = models["Jazz"].Id         },
                new() { PlateNumber = "XY-456-ZA", CarModelId = models["A3"].Id           },
                new() { PlateNumber = "YZ-567-AB", CarModelId = models["A4"].Id           },
                new() { PlateNumber = "ZA-678-BC", CarModelId = models["Q5"].Id           },
                new() { PlateNumber = "AB-789-CD", CarModelId = models["C3"].Id           },
                new() { PlateNumber = "BC-890-DE", CarModelId = models["C4"].Id           },
                new() { PlateNumber = "CD-901-EF", CarModelId = models["C5 Aircross"].Id  },
            };

            context.Cars.AddRange(cars);
            context.SaveChanges();
        }

        private static void SeedCustomers(AppDbContext context)
        {
            if (context.CarCustomers.Any()) return;

            var customers = new List<CarCustomer>
            {
                new() { Name = "Lucas Martin",  Email = "lucas.martin@locatic.fr",   PhoneNumber = "06 12 34 56 78" },
                new() { Name = "Emma Bernard",  Email = "emma.bernard@locatic.fr",   PhoneNumber = "06 23 45 67 89" },
                new() { Name = "Hugo Dubois",   Email = "hugo.dubois@locatic.fr",    PhoneNumber = "06 34 56 78 90" },
                new() { Name = "Chloe Thomas",  Email = "chloe.thomas@locatic.fr",   PhoneNumber = "06 45 67 89 01" },
                new() { Name = "Nathan Robert", Email = "nathan.robert@locatic.fr",  PhoneNumber = "06 56 78 90 12" },
                new() { Name = "Lea Petit",     Email = "lea.petit@locatic.fr",      PhoneNumber = "06 67 89 01 23" },
                new() { Name = "Jules Richard", Email = "jules.richard@locatic.fr",  PhoneNumber = "06 78 90 12 34" },
                new() { Name = "Ines Durand",   Email = "ines.durand@locatic.fr",    PhoneNumber = "06 89 01 23 45" },
            };

            context.CarCustomers.AddRange(customers);
            context.SaveChanges();
        }

        private static void SeedReservations(AppDbContext context)
        {
            if (context.CarReservations.Any()) return;

            var customers = context.CarCustomers.ToDictionary(c => c.Email);
            var cars = context.Cars.ToDictionary(car => car.PlateNumber);

            var reservations = new List<CarReservation>
            {
                new() { CarCustomerId = customers["lucas.martin@locatic.fr"].Id,   CarId = cars["FY-945-NT"].Id, StartDate = new DateTime(2026, 7, 1),  EndDate = new DateTime(2026, 7, 5)  },
                new() { CarCustomerId = customers["emma.bernard@locatic.fr"].Id,   CarId = cars["AB-123-CD"].Id, StartDate = new DateTime(2026, 7, 3),  EndDate = new DateTime(2026, 7, 6)  },
                new() { CarCustomerId = customers["hugo.dubois@locatic.fr"].Id,    CarId = cars["CD-345-EF"].Id, StartDate = new DateTime(2026, 7, 7),  EndDate = new DateTime(2026, 7, 10) },
                new() { CarCustomerId = customers["chloe.thomas@locatic.fr"].Id,   CarId = cars["FG-678-HI"].Id, StartDate = new DateTime(2026, 7, 8),  EndDate = new DateTime(2026, 7, 12) },
                new() { CarCustomerId = customers["nathan.robert@locatic.fr"].Id,  CarId = cars["LM-234-NO"].Id, StartDate = new DateTime(2026, 7, 10), EndDate = new DateTime(2026, 7, 13) },
                new() { CarCustomerId = customers["lea.petit@locatic.fr"].Id,      CarId = cars["OP-567-QR"].Id, StartDate = new DateTime(2026, 7, 14), EndDate = new DateTime(2026, 7, 18) },
                new() { CarCustomerId = customers["jules.richard@locatic.fr"].Id,  CarId = cars["UV-123-WX"].Id, StartDate = new DateTime(2026, 7, 15), EndDate = new DateTime(2026, 7, 20) },
                new() { CarCustomerId = customers["ines.durand@locatic.fr"].Id,    CarId = cars["BC-890-DE"].Id, StartDate = new DateTime(2026, 7, 21), EndDate = new DateTime(2026, 7, 24) },
            };

            context.CarReservations.AddRange(reservations);
            context.SaveChanges();
        }
    }
}