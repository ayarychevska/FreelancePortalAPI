using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using Services.Models.ApplicationUsers;

namespace Services.Services.ApplicationUsers
{
    public class ApplicationUsersService : BaseService<ApplicationUser>
    {
        public ApplicationUsersService(IRepository<ApplicationUser> repository, IMapper mapper) : base(repository, mapper) { }

        public string Test()
        {
            return "Denis";
        }

        public ApplicationUser Create(CreateModel createModel)
        {
            ApplicationUser applicationUser = Mapper.Map<ApplicationUser>(createModel);

            var result = Repository.Add(applicationUser);
            return result;
        }
    }
}
