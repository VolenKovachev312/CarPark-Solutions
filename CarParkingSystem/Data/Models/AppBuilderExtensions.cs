using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarParkingSystem.Data.Models
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder InitializeDatabase(this IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.CreateScope())
            {
                var testInitializeAccounts = serviceScope.ServiceProvider;
                var data = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                data.Database.Migrate();
                if(data.ParkingLots.Count()==0)
                {
                    var parkingLots = CreateParkingLots();
                    data.ParkingLots.AddRange(parkingLots);
                }
                InitializeAccounts(testInitializeAccounts).Wait();
                data.SaveChanges();
            }

            return app;
        }
        private static async Task InitializeAccounts(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            await CreateRoleAsync(roleManager);

            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            await CreateAdmin(userManager);
            await CreateUser(userManager);
        }

        private static async Task CreateRoleAsync(RoleManager<IdentityRole<Guid>> roleManager)
        {
            bool adminRoleExists = await roleManager.RoleExistsAsync("Admin");
            bool userRoleExists = await roleManager.RoleExistsAsync("User");

            if (adminRoleExists || userRoleExists)
            {
                return;
            }
            await roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
            await roleManager.CreateAsync(new IdentityRole<Guid>("User"));
        }
        private static async Task CreateUser(UserManager<User> userManager)
        {
            User testUser = await userManager.FindByEmailAsync("testuser@gmail.com");
            if (testUser != null)
            {
                return;
            }
            testUser = new User()
            {
                UserName = "testuser@gmail.com",
                Email = "testuser@gmail.com",
                FirstName = "User",
                LastName = "User",
                PhoneNumber = "0999888777",
                LicensePlateNumber="US1111ER"
            };
            await userManager.CreateAsync(testUser, "testpassword");
            await userManager.AddToRoleAsync(testUser, "User");
        }
        private static async Task CreateAdmin(UserManager<User> userManager)
        {
            User testAdmin = await userManager.FindByEmailAsync("testadmin@gmail.com");
            if (testAdmin != null)
            {
                return;
            }
            testAdmin = new User()
            {
                UserName = "testadmin@gmail.com",
                Email = "testadmin@gmail.com",
                FirstName = "Admin",
                LastName = "Admin",
                PhoneNumber = "0999888777",
                LicensePlateNumber="AD2222MN"
            };
            await userManager.CreateAsync(testAdmin, "testpassword");
            await userManager.AddToRoleAsync(testAdmin, "Admin");
        }

        private static List<ParkingLot> CreateParkingLots()
        {
            var parkingLots = new List<ParkingLot>()
            {
                new ParkingLot()
                {
                    Name="Rostov Parking",
                    Address="pl. \"Svobodata\" 3, 5800 Pleven Center, Pleven, Bulgaria",
                    Latitude=43.41057390340824M,
                    Longitude=24.61796279989513M,
                    Capacity=12,
                    AvailableSpaces=12,
                    HourlyRate=5,
                    ImageUrl="https://visitpleven.com/wp-content/uploads/2018/02/obshtina-2.2-33.jpg",
                    IsNonStop=true,
                    OpeningHour=new TimeOnly(10,0,0),
                    ClosingHour=new TimeOnly(10,0,0)
                },
                new ParkingLot()
                {
                    Name="Mall Bulgaria Parking",
                    Address="Todor Kableshkov St 72, 1680 Manastirski Livadi, Sofia, Bulgaria",
                    Latitude=42.66316381861941M,
                    Longitude=23.288571182522723M,
                    Capacity=16,
                    AvailableSpaces=16,
                    HourlyRate=2.5M,
                    ImageUrl="https://cdn.cordeel.eu/sites/cordeel_cdn/files/styles/og_image/public/2019-05/shopping_bulgariamall_sofia_01.jpg?h=10d202d3&itok=O4kNe4WL",
                    IsNonStop=false,
                    OpeningHour=new TimeOnly(10,0,0),
                    ClosingHour=new TimeOnly(20,0,0)
                },
                new ParkingLot()
                {
                    Name="MatHaus Parking",
                    Address="Calea Vitan 122, București, Romania",
                    Latitude=44.415175897005696M,
                    Longitude=26.131281852722168M,
                    Capacity=40,
                    AvailableSpaces=40,
                    HourlyRate=3,
                    ImageUrl="https://www.bricoretail.ro/wp-content/uploads/2021/12/Magazin-MatHaus-e1525942546707.png",
                    IsNonStop=false,
                    OpeningHour=new TimeOnly(8,0,0),
                    ClosingHour=new TimeOnly(23,0,0)
                },
                new ParkingLot()
                {
                    Name="Jumbo Skopje",
                    Address="Boulevard Kocho Racin 1, Skopje 1000, North Macedonia",
                    Latitude=41.99277750048468M,
                    Longitude=21.442191223992662M,
                    Capacity=10,
                    AvailableSpaces=10,
                    HourlyRate=1.50M,
                    ImageUrl="https://vero.com.mk/wp-content/uploads/2017/12/jumbo_big.jpg",
                    IsNonStop=false,
                    OpeningHour=new TimeOnly(6,0,0),
                    ClosingHour=new TimeOnly(3,0,0)
                },
                new ParkingLot()
                {
                    Name="Belgrade Lidl",
                    Address="Tošin bunar 41, Beograd 200297, Serbia",
                    Latitude=44.83712582365491M,
                    Longitude=20.402812957763672M,
                    Capacity=32,
                    AvailableSpaces=32,
                    HourlyRate=2.30M,
                    ImageUrl="https://upload.wikimedia.org/wikipedia/commons/1/1d/Lidl-Belgrade%282%29.jpg",
                    IsNonStop=true,
                    OpeningHour=new TimeOnly(10,0,0),
                    ClosingHour=new TimeOnly(10,0,0)
                },
            };
            return parkingLots;
        }
    }
}
