using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMSWebApp.Controllers
{
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
            var result = await _userRepository.GetUserLists();
            return View(result);
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
    }
}
