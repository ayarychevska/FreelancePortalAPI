using Core.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class ApplicationUser : IdentityUser, IAuditableEntity
    {
        /// <summary>
        /// Navigation property for the claims this user possesses.
        /// </summary>
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public string Login { get; set; }
        public string UserType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Description { get; set; }
        //public virtual ICollection<Review> Reviews { get; set; }
        //public virtual ICollection<Appointment> Appointments { get; set; }
        //public virtual ICollection<Post> Posts { get; set; }

        /// <summary>
        /// Implementation of <see cref="IAuditableEntity"/> interface
        /// </summary>
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
