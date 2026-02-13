using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class LoginResponse
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
