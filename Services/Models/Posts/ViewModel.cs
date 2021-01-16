using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models.Posts
{
    public class ViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime DateUtc { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int Status { get; set; }
        public int SubjectId { get; set; }
        public string SubjectTitle { get; set; }
    }
}
