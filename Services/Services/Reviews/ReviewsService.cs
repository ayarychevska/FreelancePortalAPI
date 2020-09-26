using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;

namespace Services.Services.Reviews
{
    public class ReviewsService : BaseService<Review>
    {
        public ReviewsService(IRepository<Review> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
