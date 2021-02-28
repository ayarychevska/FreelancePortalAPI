using Core.Models;
using Services.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Services.Models.Appointments
{
    public class ViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDateUTC { get; set; }
        public DateTime EndDateUTC { get; set; }
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }

        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string SubjectId { get; set; }
        public string SubjectTitle { get; set; }
        public string Date { get { return StartDateUTC.ToString("d MMM yyyy"); } }
        public string TimeRange { get { return StartDateUTC.ToString("hh:mm") + " - " + EndDateUTC.ToString("hh:mm"); } }
    }

    public class ListViewModel
    {
        public bool IsTeacher { get; set; }
        public Pager Pager { get; set; }
        public IEnumerable<ViewModel> ViewModels { get; set; }
    }
}
