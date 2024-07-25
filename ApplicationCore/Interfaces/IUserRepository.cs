using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetUserLists();
        Task<IdentityUser> GetUserById(string userId);
        Task<string> UpdateUser(IdentityUser identityUser);
        Task<string> DeleteUsers(string userId);
    }
}
