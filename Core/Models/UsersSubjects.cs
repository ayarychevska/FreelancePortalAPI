using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class UsersSubjects
    {
        public long Id { get; set; }
        public ApplicationUser User { get; set; }
        public Subject Subject { get; set; }
    }
}
