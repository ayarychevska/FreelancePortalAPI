using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models.Posts
{
    public class FilterModel
    {
        public string Title { get; set; }
        public DateTime? DateFromUTC { get; set; }
        public DateTime? DateUntilUTC { get; set; }
        public string TeacherId { get; set; }
        public long? SubjectId { get; set; }
    }
}
