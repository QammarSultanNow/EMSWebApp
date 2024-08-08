using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Users.GetUserById
{
    internal class GetUsersByIdHandler : IRequestHandler<GetUsersByIdRequest, IdentityUser>
    {
        private readonly IUserRepository _userRepository;
        public GetUsersByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IdentityUser> Handle(GetUsersByIdRequest request, CancellationToken cancellationToken)
        {   
           var result = await  _userRepository.GetUserById(request.Id);
            return result;
        }
    }
}
