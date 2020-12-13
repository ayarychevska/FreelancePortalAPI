using Core.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Review : IAuditableEntity
    {
        public long Id { get; set; }
        public DateTime DatePostedUTC { get; set; }
        public string Text { get; set; }
        public int Rate { get; set; }
        [ForeignKey("Reviewer")]
        public string ReviewerId { get; set; }
        public ApplicationUser Reviewer { get; set; }
        [ForeignKey("ReviewingUser")]
        public string ReviewingUserId { get; set; }
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
