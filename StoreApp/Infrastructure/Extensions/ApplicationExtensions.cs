using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace StoreApp.Infrastructure.Extensions
{
    public static class ApplicationExtensions
    {
        public static void ConfigureAndCheckMigration(this IApplicationBuilder app)

        {
            RepositoryContext context = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<RepositoryContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }

        public static void ConfigureLocalization(this WebApplication app)
        {
            app.UseRequestLocalization(options =>
            {
                options.AddSupportedCultures("tr-Tr")
                .AddSupportedUICultures("tr-Tr")
                .SetDefaultCulture("tr-Tr");
            });
        }
        public static async void ConfigureDefaultAdminUser(this IApplicationBuilder app)
        {
            const string adminUser="Admin";
            const string adminPassword="Admin123456+";
            UserManager<IdentityUser> userManager=app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<UserManager<IdentityUser>>();
            
            RoleManager<IdentityRole> roleManager=app
                .ApplicationServices
                .CreateAsyncScope()
                .ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            IdentityUser user= await userManager.FindByNameAsync(adminUser);
            if(user is null)
            {
                user=new IdentityUser()
                {
                    Email="deneme@mail.com",
                    PhoneNumber="5087277882",
                    UserName=adminUser
                };
                var result=await userManager.CreateAsync(user,adminPassword);
                if(!result.Succeeded)
                    throw new Exception("Admin user not created");
                var roleResult=await userManager.AddToRolesAsync(user,
                    roleManager
                    .Roles
                    .Select(r=>r.Name)
                    .ToList());
                if(!roleResult.Succeeded)
                    throw new Exception("System have problems with role definition for admin.");
            }
        }
    }

}