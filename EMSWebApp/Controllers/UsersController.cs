using ApplicationCore.Interfaces;
using ApplicationCore.UseCases.Users.DeleteUsers;
using ApplicationCore.UseCases.Users.GetUserById;
using ApplicationCore.UseCases.Users.GetUsers;
using ApplicationCore.UseCases.Users.LockUser;
using ApplicationCore.UseCases.Users.UnlockUser;
using ApplicationCore.UseCases.Users.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMSWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
     
        private readonly IMediator _mediator;

        public UsersController(IUserRepository userRepository, IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetUsers()
        {
            if (User.IsInRole("Admin"))
            {
                var result = await _mediator.Send(new GetUsersRequest());
                return View(result);
            }
            else
            {
                return RedirectToAction("Index" , "Home");
            }
            
        }

        public async Task<IActionResult> GetUsersById(string id)
        {

            var result = await _mediator.Send(new GetUsersByIdRequest() { Id = id});
            return View(result);
        }

        [HttpPost]
        [Route("Users/UpdateUsers/{Id}")]
        public async Task<IActionResult> UpdateUsers(UpdateUserRequest request, IdentityUser identityUser)
        {
            var result = await _mediator.Send(request) ;
            return RedirectToAction("GetUsers");
        }

        
        [Route("Users/DeleteUsers/{userId}")]
        public async Task<IActionResult> DeleteUsers(string userId)
        {
            var result = await _mediator.Send(new DeleteUsersRequest() { Id = userId });
            return RedirectToAction("GetUsers");
        }

       public async Task<IActionResult> LockUser(string Id)
        {
           var result =  await _mediator.Send(new LockUserRequest() { Id = Id});
            return RedirectToAction("GetUsers");
        }

        public async Task<IActionResult> UnLockUser(string Id)
        {
            var result = await _mediator.Send(new UnlockUserRequest() { Id = Id});  
            return RedirectToAction("GetUsers");
        }


    }
}
