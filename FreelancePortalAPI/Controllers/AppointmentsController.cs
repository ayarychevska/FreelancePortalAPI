using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using FreelancePortalAPI.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Models.Appointments;
using Services.Services.ApplicationUsers;
using Services.Services.Appointments;

namespace FreelancePortalAPI.Controllers
{
    [Authorize/*(Policy = Policies.Admin)*/]
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private IMapper Mapper;
        private IRepository<Appointment> Repository { get; }
        private AppointmentsService AppointmentsService { get; }
        private ApplicationUsersService ApplicationUsersService;

        public AppointmentsController(IRepository<Appointment> repository, AppointmentsService appointmentsService, IMapper mapper, ApplicationUsersService applicationUsersService)
        {
            Repository = repository;
            AppointmentsService = appointmentsService;
            Mapper = mapper;
            ApplicationUsersService = applicationUsersService;
        }

        [HttpPost]
        public async Task<ActionResult<CreateModel>> Create([FromBody] CreateModel createModel)
        {
            var result = AppointmentsService.Create(createModel);

            CreateModel appointmentCreateModel = Mapper.Map<CreateModel>(result);

            return Ok(appointmentCreateModel);
        }

        [HttpGet("list")]
        public async Task<ActionResult<ListViewModel>> GetAppointments([FromQuery] string userId)
        {
            try
            {
                var appointments = AppointmentsService.GetMyAppointments(userId);
                return Ok(new ListViewModel { IsTeacher = ApplicationUsersService.IsTeacher(), ViewModels = Mapper.Map<List<ViewModel>>(appointments) });
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("calendar")]
        public async Task<ActionResult<CalendarListViewModel>> GetCalendarAppointments([FromQuery] string userId)
        {
            try
            {
                var appointments = AppointmentsService.GetCalendarAppointments(userId);
                return Ok(new CalendarListViewModel { IsTeacher = ApplicationUsersService.IsTeacher(), ViewModels = Mapper.Map<List<CalendarViewModel>>(appointments) });
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<CreateModel>> Update([FromBody] CreateModel createModel)
        {
            var result = AppointmentsService.Update(createModel);

            CreateModel appointmentCreateModel = Mapper.Map<CreateModel>(result);

            return Ok(appointmentCreateModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] long id)
        {
            var appointment = Repository.GetSingleOrDefault(x => x.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            Repository.Remove(appointment);
            return Ok();
        }
    }
}
