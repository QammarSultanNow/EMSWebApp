using ApplicationCore.Interfaces;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<string> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {

            var user = _mapper.Map<IdentityUser>(request);

            var result = await _userRepository.UpdateUser(user);
            return result;
        }
    }
}
