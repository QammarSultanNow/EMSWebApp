using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Users.GetUsers
{
    public class GetUserHandler : IRequestHandler<GetUsersRequest, IEnumerable<IdentityUser>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<IdentityUser>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
          var users = await _userRepository.GetUserLists();
          return users;
        }
    }
}
