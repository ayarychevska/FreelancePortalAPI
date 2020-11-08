using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using Services.Models.Subjects;

namespace Services.Services.Subjects
{
    public class SubjectsService : BaseService<Subject>
    {
        public SubjectsService(IRepository<Subject> repository, IMapper mapper) : base(repository, mapper) { }

        public Subject Create(SubjectModel subjectModel)
        {
            Subject subject = Mapper.Map<Subject>(subjectModel);

            var result = Repository.Add(subject);
            return result;
        }

        public Subject Update(SubjectModel subjectModel)
        {
            var baseEntity = Repository.GetSingleOrDefault(x => x.Id == subjectModel.Id);
            Subject subject = Mapper.Map(subjectModel, baseEntity);

            var result = Repository.Update(subject);
            return result;
        }
    }
}
