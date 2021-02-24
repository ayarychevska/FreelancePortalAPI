using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models.Appointments
{
    public class CalendarViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }

        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string SubjectTitle { get; set; }
    }

    public class CalendarListViewModel
    {
        public bool IsTeacher { get; set; }
        public IEnumerable<CalendarViewModel> ViewModels { get; set; }
    }
}
