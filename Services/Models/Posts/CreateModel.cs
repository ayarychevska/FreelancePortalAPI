using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models.Posts
{
    public class CreateModel
    {
        public long Id { get; set; }
        public DateTime DateUTC { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public int Status { get; set; }
        public int SubjectId { get; set; }
    }
}
