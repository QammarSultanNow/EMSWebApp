using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<IdentityUser>> GetUserLists()
        {
            var user = await _userManager.Users.ToListAsync();
            return user.ToList();
        }

        public async Task<IdentityUser> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        public async Task<string> UpdateUser(IdentityUser identityUser)
        {
            var user = await _userManager.FindByIdAsync(identityUser.Id);
           
                user.UserName = identityUser.UserName;
                user.Email = identityUser.Email;

                await _userManager.UpdateAsync(user);
               
                return user.Id;
        }

        public async Task<string> DeleteUsers(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.DeleteAsync(user);

            return user.Id;
        }
    }
}
