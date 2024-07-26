using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMSWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetUsers()
        {
            if (User.IsInRole("Admin"))
            {
                var result = await _userRepository.GetUserLists();
                return View(result);
            }
            else
            {
                return RedirectToAction("Index" , "Home");
            }
            
        }

        public async Task<IActionResult> GetUsersById(string id)
        {
            var result = await _userRepository.GetUserById(id);
            return View(result);
        }

        [HttpPost]
        [Route("Users/UpdateUsers/{Id}")]
        public async Task<IActionResult> UpdateUsers(IdentityUser identityUser)
        {
            var result = await _userRepository.UpdateUser(identityUser);
            return RedirectToAction("GetUsers");
        }

        
        [Route("Users/DeleteUsers/{userId}")]
        public async Task<IActionResult> DeleteUsers(string userId)
        {
            var result = await _userRepository.DeleteUsers(userId);
            return RedirectToAction("GetUsers");
        }

       public async Task<IActionResult> LockUser(string Id)
        {
           var result =  await _userRepository.LockUserRepo(Id);

            return RedirectToAction("GetUsers");
        }

        public async Task<IActionResult> UnLockUser(string Id)
        {
            var result = await _userRepository.UnclockUserRepo(Id);
            return RedirectToAction("GetUsers");
        }


    }
}
