using Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    public class Post : IAuditableEntity
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime DateUTC { get; set; }
        public string Text { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int Status { get; set; }
        [ForeignKey("Subject")]
        public long SubjectId { get; set; }
        public Subject Subject { get; set; }

        /// <summary>
        /// Implementation of <see cref="IAuditableEntity"/> interface
        /// </summary>
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
