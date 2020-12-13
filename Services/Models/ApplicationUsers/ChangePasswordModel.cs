using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models.ApplicationUsers
{
    public class ChangePasswordModel
    {
        public string Id { get; set; }
        public string PreviousPassword { get; set; }
        public string Password { get; set; }
        public string RepeatedPassword { get; set; }
    }
}
