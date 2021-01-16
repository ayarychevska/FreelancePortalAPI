using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Services
{
    public class UsersSubjectsService : BaseService<UsersSubjects>
    {
        public UsersSubjectsService(IRepository<UsersSubjects> repository, IMapper mapper) : base(repository, mapper) { }

        public void EraseUserSubjects(string id)
        {
            var entities = Repository.FindQuery(x => x.UserId == id).ToList();

            Repository.RemoveRange(entities);
        }

        public void AddUserSubjects(string id, long[] subjects)
        {
            List<UsersSubjects> usersSubjects = new List<UsersSubjects>();

            foreach(long subject in subjects)
            {
                usersSubjects.Add(new UsersSubjects { SubjectId = subject, UserId = id });
            }

            Repository.AddRange(usersSubjects);
        }
    }
}
