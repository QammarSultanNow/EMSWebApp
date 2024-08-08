﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UseCases.Users.UnlockUser
{
    public class UnlockUserRequest : IRequest<IdentityUser>
    {
        public string Id { get; set; }
    }
}
