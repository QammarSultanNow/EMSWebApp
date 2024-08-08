using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Users.DeleteUsers
{
    public class DeleteUsersRequest : IRequest<string>
    {
        public string Id { get; set; }
    }
}
