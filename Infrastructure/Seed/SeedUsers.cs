using EMSWebApp.RolesEnum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace EMSWebApp.Roles
{
    public static class SeedUsers
    {

        public static async Task DefaultUser(IServiceProvider serviceProvider)
        {
            try
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                var admin = new IdentityUser();

                admin.UserName = "hanifraza@gmail.com";
                admin.Email = "hanifraza@gmail.com";
                admin.EmailConfirmed = true;
                admin.PhoneNumberConfirmed = true;

                if (userManager.Users.All(x => x.Id != admin.Id))
                {
                    var result = await userManager.FindByEmailAsync(admin.Email);
                    if (result == null)
                    {
                        await userManager.CreateAsync(admin, "Test123@");
                        var res = await userManager.AddToRoleAsync(admin, AssignRole.Admin.ToString());
                        //      await userManager.UpdateAsync(admin);


                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
