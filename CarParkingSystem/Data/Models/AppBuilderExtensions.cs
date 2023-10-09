using Microsoft.AspNetCore.Identity;

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

                InitializeAccounts(testInitializeAccounts).Wait();
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
    }
}
