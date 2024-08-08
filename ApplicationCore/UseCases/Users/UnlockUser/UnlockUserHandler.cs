using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Users.UnlockUser
{
    public class UnlockUserHandler : IRequestHandler<UnlockUserRequest, IdentityUser>
    {
        private readonly IUserRepository _userRepository;
        public UnlockUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IdentityUser> Handle(UnlockUserRequest request, CancellationToken cancellationToken)
        {
           var result = await _userRepository.UnclockUserRepo(request.Id);
            return result;
        }
    }
}
