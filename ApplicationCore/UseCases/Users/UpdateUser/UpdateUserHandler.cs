using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Users.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, string>
    {
        private readonly IUserRepository _userRepository;
        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            IdentityUser user = new IdentityUser();
            user.Id = request.Id;
            user.UserName = request.UserName;
            user.Email = request.Email;
            var result = await _userRepository.UpdateUser(user);
            return result;
        }
    }
}
