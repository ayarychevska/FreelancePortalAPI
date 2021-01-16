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
        }
    }
}
