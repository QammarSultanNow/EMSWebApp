using ApplicationCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Users.DeleteUsers
{
    public class DeleteUsersHandler : IRequestHandler<DeleteUsersRequest, string>
    {
        private readonly IUserRepository _userRepository;
        public DeleteUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> Handle(DeleteUsersRequest request, CancellationToken cancellationToken)
        {
           var result = await _userRepository.DeleteUsers(request.Id);
           return result;
        }
    }
}
