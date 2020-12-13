using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    public class UsersSubjects
    {
        public long Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [ForeignKey("Subject")]
        public long SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
