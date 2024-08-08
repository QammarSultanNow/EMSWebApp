using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Users.UpdateUser
{
    public class UpdateUserRequest : IRequest<string>
    {
        public string Id { get; set; }
        public virtual string? UserName { get; set; }
        public virtual string? Email { get; set; }
    }
}
