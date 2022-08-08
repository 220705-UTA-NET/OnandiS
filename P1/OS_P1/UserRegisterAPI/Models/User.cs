using System;
using System.Collections.Generic;

namespace UserRegisterAPI.Models
{
    public partial class User
    {
        public decimal IdUser { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string Email { get; set; } = null!;
    }
}
