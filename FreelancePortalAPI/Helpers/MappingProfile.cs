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
                .ForMember(d => d.RepeatPassword, s => s.Ignore());

            CreateMap<CreateModel, ApplicationUser>()
                .ForMember(d => d.UserName, s => s.MapFrom(m => m.Name));

            CreateMap<ApplicationUser, ViewModel>()
                .ForMember(d => d.Name, s => s.MapFrom(m => m.UserName));

            //Subject mappings
            CreateMap<Subject, SubjectModel>();
            CreateMap<SubjectModel, Subject>();

        }
    }
}
