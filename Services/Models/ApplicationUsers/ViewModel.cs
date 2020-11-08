using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Models.ApplicationUsers
{
    public class ViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
    }
}
