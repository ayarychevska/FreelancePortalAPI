using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models.ApplicationUsers
{
    public class ListViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
    }
}
