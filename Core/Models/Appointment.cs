using Core.Models.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class Appointment : IAuditableEntity
    {
        public long Id { get; set; }
        public DateTime StartDateUTC { get; set; }
        public DateTime EndDateUTC { get; set; }
        [ForeignKey("Teacher")]
        public string TeacherId { get; set; }
        public ApplicationUser Teacher { get; set; }
        [ForeignKey("Student")]
        public string StudentId { get; set; }
        public ApplicationUser Student { get; set; }
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
