using System;
using System.Collections.Generic;
using System.Text;

namespace eFolio.DTO.Common
{
    public class UserRegister
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool ConfirmedEmail { get; set; }
    }
}
