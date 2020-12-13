using Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models
{
    public class Message : IAuditableEntity
    {
        public long Id { get; set; }
        public DateTime DateTimeSendedUTC { get; set; }
        public string Text { get; set; }
        public int Status { get; set; }
        [ForeignKey("Sender")]
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }
        [ForeignKey("Receiver")]
        public string ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; }

        /// <summary>
        /// Implementation of <see cref="IAuditableEntity"/> interface
        /// </summary>
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
