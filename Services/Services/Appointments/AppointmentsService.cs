using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Models.Appointments;
using Services.Services.ApplicationUsers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Services.Appointments
{

    public class AppointmentsService : BaseService<Appointment>
    {
        private ApplicationUsersService _applicationUsersService;

        public AppointmentsService(IRepository<Appointment> repository, IMapper mapper, ApplicationUsersService applicationUsersService) : base(repository, mapper)
        {
            _applicationUsersService = applicationUsersService;
        }

        public Appointment Create(CreateModel createModel)
        {
            Appointment appointment = Mapper.Map<Appointment>(createModel);

            var result = Repository.Add(appointment);
            return result;
        }

        public Appointment Update(CreateModel createModel)
        {
            var baseEntity = Repository.GetSingleOrDefault(x => x.Id == createModel.Id);
            Appointment appointment = Mapper.Map(createModel, baseEntity);

            var result = Repository.Update(appointment);
            return result;
        }

        public List<Appointment> GetMyAppointments(string userId)
        {
            if (!_applicationUsersService.IsTeacher())
                return Repository
                    .FindQuery(x => x.StudentId == userId)
                    .Include(y => y.Student)
                    .Include(s => s.Subject)
                    .Include(m => m.Teacher)
                    .ToList();
            if (_applicationUsersService.IsTeacher())
                return Repository
                    .FindQuery(x => x.TeacherId == userId)
                    .Include(y => y.Student)
                    .Include(s => s.Subject)
                    .Include(m => m.Teacher)
                    .ToList();
            else
                throw new NullReferenceException();
        }

        public List<Appointment> GetCalendarAppointments(string userId)
        {
            if (!_applicationUsersService.IsTeacher())
                return Repository
                    .FindQuery(x => x.StudentId == userId)
                    .Include(y => y.Student)
                    .Include(s => s.Subject)
                    .Include(m => m.Teacher)
                    .ToList();
            if (_applicationUsersService.IsTeacher())
                return Repository
                    .FindQuery(x => x.TeacherId == userId)
                    .Include(y => y.Student)
                    .Include(s => s.Subject)
                    .Include(m => m.Teacher)
                    .ToList();
            else
                throw new NullReferenceException();
        }

    }
}
