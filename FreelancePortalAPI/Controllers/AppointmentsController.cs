using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Appointments;

namespace FreelancePortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private IMapper Mapper;
        private IRepository<Appointment> Repository { get; }
        private AppointmentsService ApplicationUsersService { get; }

        public AppointmentsController(IRepository<Appointment> repository, AppointmentsService applicationUsersService, IMapper mapper)
        {
            Repository = repository;
            ApplicationUsersService = applicationUsersService;
            Mapper = mapper;
        }
    }
}
