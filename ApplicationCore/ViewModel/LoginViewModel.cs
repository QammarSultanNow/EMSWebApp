using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ViewModel
{
    public class LoginViewModel
    {
         
                
        public string? ReturnUrl { get; set; }
        public List<AuthenticationScheme>? ExternalLogins { get; set; }
    }
}
