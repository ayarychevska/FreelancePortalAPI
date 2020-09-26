using AutoMapper;
using Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public class BaseService<T> where T : class
    {
        protected IRepository<T> Repository;
        protected IMapper Mapper;

        public BaseService(IRepository<T> repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }
    }
}
