using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using Services.Models.Appointments;

namespace Services.Services.Appointments
{
    public class AppointmentsService : BaseService<Appointment>
    {
        public AppointmentsService(IRepository<Appointment> repository, IMapper mapper) : base(repository, mapper) { }

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
    }
}
