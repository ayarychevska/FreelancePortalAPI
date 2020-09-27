using Core.Models;
using System;

namespace Services.Models.Appointments
{
    public class CreateModel
    {
        public long Id { get; set; }
        public DateTime StartDateUTC { get; set; }
        public DateTime EndDateUTC { get; set; }
        public ApplicationUser Teacher { get; set; }
        public ApplicationUser Student { get; set; }
        public Subject Subject { get; set; }
    }
}
