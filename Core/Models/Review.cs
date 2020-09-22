using Core.Models.Interfaces;
using System;

namespace Core.Models
{
    public class Review : IAuditableEntity
    {
        public long Id { get; set; }
        public DateTime DatePostedUTC { get; set; }
        public string Text { get; set; }
        public int Rate { get; set; }
        public ApplicationUser Reviewer { get; set; }
        public ApplicationUser ReviewingUser { get; set; }

        /// <summary>
        /// Implementation of <see cref="IAuditableEntity"/> interface
        /// </summary>
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
