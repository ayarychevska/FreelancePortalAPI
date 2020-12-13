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

        public AppointmentsController(IRepository<Appointment> repository, AppointmentsService appointmentsService, IMapper mapper)
        {
            Repository = repository;
            AppointmentsService = appointmentsService;
            Mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ViewModel>> Create([FromBody] CreateModel createModel)
        {
            var result = AppointmentsService.Create(createModel);

            ViewModel appointmentViewModel = Mapper.Map<ViewModel>(result);

            return Ok(appointmentViewModel);
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<ViewModel>>> GetAppointments()
        {
            var appointments = Repository.GetAll();

            return Ok(Mapper.Map<List<ViewModel>>(appointments));
        }

        [HttpPut]
        public async Task<ActionResult<ViewModel>> Update([FromBody] CreateModel createModel)
        {
            var result = AppointmentsService.Update(createModel);

            ViewModel appointmentViewModel = Mapper.Map<ViewModel>(result);

            return Ok(appointmentViewModel);
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
