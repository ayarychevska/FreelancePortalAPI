using AutoMapper;
using Core.Models;
using Services.Models.ApplicationUsers;
using Services.Models.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreelancePortalAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //ApplicationUser mappings
            CreateMap<ApplicationUser, CreateModel>()
                .ForMember(d => d.Name, s => s.MapFrom(m => m.UserName))
                .ForMember(d => d.Password, s => s.Ignore())
                .ForMember(d => d.RepeatPassword, s => s.Ignore())
                .ForMember(d => d.SubjectsIds, s => s.MapFrom(m => m.UsersSubjects.Select(p => p.SubjectId)));

            CreateMap<CreateModel, ApplicationUser>()
                .ForMember(d => d.UserName, s => s.MapFrom(m => m.Name))
                .ForMember(d => d.UsersSubjects, s => s.Ignore())
                .ForMember(d => d.Login, s => s.Ignore());

            CreateMap<ApplicationUser, ListViewModel>()
                .ForMember(d => d.Name, s => s.MapFrom(m => m.UserName));

            CreateMap<ApplicationUser, ViewModel>()
                .ForMember(d => d.Name, s => s.MapFrom(m => m.UserName))
                .ForMember(d => d.Subjects, s => s.MapFrom(m => m.UsersSubjects.Select(p => p.Subject)));

            //Subject mappings
            CreateMap<Subject, SubjectModel>();
            CreateMap<SubjectModel, Subject>();


            //Posts mappings
            CreateMap<Post, Services.Models.Posts.CreateModel>();

            CreateMap<Services.Models.Posts.CreateModel, Post>()
                .ForMember(d => d.User, s => s.Ignore())
                .ForMember(d => d.Subject, s => s.Ignore());

            CreateMap<Post, Services.Models.Posts.ViewModel>()
                .ForMember(d => d.SubjectTitle, s => s.MapFrom(m => m.Subject.Title))
                .ForMember(d => d.UserName, s => s.MapFrom(m => m.User.UserName));

            //Review mappings
            CreateMap<Review, Services.Models.Reviews.CreateModel>();

            CreateMap<Services.Models.Reviews.CreateModel, Review>()
                .ForMember(d => d.Reviewer, s => s.Ignore())
                .ForMember(d => d.ReviewingUser, s => s.Ignore());

            CreateMap<Review, Services.Models.Reviews.ViewModel>()
                .ForMember(d => d.ReviewerName, s => s.MapFrom(m => m.Reviewer.UserName))
                .ForMember(d => d.ReviewingUserName, s => s.MapFrom(m => m.ReviewingUser.UserName));

            //Appointment mappings
            CreateMap<Appointment, Services.Models.Appointments.CreateModel>();

            CreateMap<Services.Models.Appointments.CreateModel, Appointment>()
                .ForMember(d => d.Student, s => s.Ignore())
                .ForMember(d => d.Teacher, s => s.Ignore())
                .ForMember(d => d.Subject, s => s.Ignore());

            CreateMap<Appointment, Services.Models.Appointments.ViewModel>()
                .ForMember(d => d.Date, s => s.Ignore())
                .ForMember(d => d.TimeRange, s => s.Ignore())
                .ForMember(d => d.StudentName, s => s.MapFrom(m => m.Student.UserName))
                .ForMember(d => d.TeacherName, s => s.MapFrom(m => m.Teacher.UserName))
                .ForMember(d => d.SubjectTitle, s => s.MapFrom(m => m.Subject.Title));

            CreateMap<Appointment, Services.Models.Appointments.CalendarViewModel>()
                .ForMember(d => d.Start, s => s.MapFrom(m => m.StartDateUTC.ToString("yyyy-MM-dd HH:mm")))
                .ForMember(d => d.End, s => s.MapFrom(m => m.EndDateUTC.ToString("yyyy-MM-dd HH:mm")))
                .ForMember(d => d.StudentName, s => s.MapFrom(m => m.Student.UserName))
                .ForMember(d => d.TeacherName, s => s.MapFrom(m => m.Teacher.UserName))
                .ForMember(d => d.SubjectTitle, s => s.MapFrom(m => m.Subject.Title));
        }
    }
}
