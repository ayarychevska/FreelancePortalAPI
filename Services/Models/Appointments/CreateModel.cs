using Core.Models;
using System;

namespace Services.Models.Appointments
{
    public class CreateModel
    {
        public long Id { get; set; }
        public DateTime StartDateUTC { get; set; }
        public DateTime EndDateUTC { get; set; }
        public string TeacherId { get; set; }
        public string StudentId { get; set; }
        public long SubjectId { get; set; }
    }
}
