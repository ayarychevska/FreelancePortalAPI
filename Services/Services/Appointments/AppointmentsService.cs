using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Services.Models.Appointments;
using Services.Models.Common;
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

        public List<Appointment> GetMyAppointments(string userId, FilterModel filter, Pager pager)
        {
            #region Filter

            var predicate = PredicateBuilder.New<Appointment>(false);

            if (!_applicationUsersService.IsTeacher())
                predicate.And(x => x.StudentId == userId);
            else
                predicate.And(x => x.TeacherId == userId);

            if (filter.SubjectId.HasValue)
                predicate.And(q => q.SubjectId == filter.SubjectId);

            if (filter.DateFromUTC.HasValue)
                predicate.And(q => q.StartDateUTC.Date >= filter.DateFromUTC.Value.Date);

            if (filter.DateUntilUTC.HasValue)
                predicate.And(q => q.EndDateUTC.Date <= filter.DateUntilUTC.Value.Date);

            if (!string.IsNullOrEmpty(filter.Title))
                predicate.And(q => q.Title.Contains(filter.Title));

            #endregion

            return Repository
                .FindQuery(predicate)
                .Include(y => y.Student)
                .Include(s => s.Subject)
                .Include(m => m.Teacher)
                .Paginate(pager)
                .ToList();
        }

        public List<Appointment> GetCalendarAppointments(string userId)
        {
            var predicate = PredicateBuilder.New<Appointment>(false);

            if (!_applicationUsersService.IsTeacher())
                predicate.And(x => x.StudentId == userId);
            else
                predicate.And(x => x.TeacherId == userId);

            return Repository
                .FindQuery(predicate)
                .Include(y => y.Student)
                .Include(s => s.Subject)
                .Include(m => m.Teacher)
                .ToList();
        }
    }
}
