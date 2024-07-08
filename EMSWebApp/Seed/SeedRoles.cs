using EMSWebApp.IdentityModel;
using EMSWebApp.RolesEnum;
using Microsoft.AspNetCore.Identity;

namespace EMSWebApp.Seed
{
    public static class SeedRoles
    {
       
        public static async Task  DefaultRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var admin = new IdentityRole();
            admin.Name = AssignRole.Admin.ToString();
            admin.NormalizedName = AssignRole.Admin.ToString().ToUpper();
            await roleManager.CreateAsync(admin);

            var manager = new IdentityRole();
            manager.Name = AssignRole.Manager.ToString();
            manager.NormalizedName = AssignRole.Manager.ToString().ToUpper();
            await roleManager.CreateAsync(manager);

            var basic = new IdentityRole();
            basic.Name = AssignRole.User.ToString();
            basic.NormalizedName = AssignRole.User.ToString().ToUpper();
            await roleManager.CreateAsync(basic);
        }
    }
}
