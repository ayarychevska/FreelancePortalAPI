using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;

namespace Services.Services.Appointments
{
    public class AppointmentsService : BaseService<Appointment>
    {
        public AppointmentsService(IRepository<Appointment> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
