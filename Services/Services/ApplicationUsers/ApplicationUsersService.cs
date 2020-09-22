using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Services.Models.ApplicationUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services.ApplicationUsers
{
    public class ApplicationUsersService
    {
        private IRepository<ApplicationUser> Repository;
        private IMapper Mapper;

        public ApplicationUsersService(IRepository<ApplicationUser> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

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
