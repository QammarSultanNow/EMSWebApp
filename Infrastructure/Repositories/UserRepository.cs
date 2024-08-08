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

        public async Task<IdentityUser> LockUserRepo(string Id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(Id);
                if (user == null)
                {
                    return null;
                }

                DateTimeOffset? LockUserDate = DateTime.UtcNow.AddYears(1);
                var res = await _userManager.SetLockoutEndDateAsync(user, LockUserDate);
                await _userManager.SetLockoutEnabledAsync(user, true);
                await _userManager.UpdateAsync(user);
                return user;
            }
            catch (Exception ex) { 
                throw new Exception(ex.Message);
            }
        }

        public async Task<IdentityUser> UnclockUserRepo(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
           // await _userManager.SetLockoutEnabledAsync(user, false);
            DateTimeOffset? LockUserDate = DateTime.UtcNow;
            await _userManager.SetLockoutEndDateAsync(user, LockUserDate);
            await _userManager.UpdateAsync(user);
            return user;

        }
    }
}
