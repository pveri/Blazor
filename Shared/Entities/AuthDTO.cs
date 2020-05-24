using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BlazorMovies.Shared.Entities
{
    public class UserInfo
    {
        public string Email { get; set; }
        public string UserName => Email;
        public string Password { get; set; }
    }

    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
