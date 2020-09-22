using Core.Models.Interfaces;
using System;

namespace Core.Models
{
    public class Appointment : IAuditableEntity
    {
        public long Id { get; set; }
        public DateTime StartDateUTC { get; set; }
        public DateTime EndDateUTC { get; set; }
        public ApplicationUser Teacher { get; set; }
        public ApplicationUser Student { get; set; }
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
