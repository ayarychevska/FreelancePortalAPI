using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Models.Common;
using Services.Models.Reviews;
using System.Collections.Generic;
using System.Linq;

namespace Services.Services.Reviews
{
    public class ReviewsService : BaseService<Review>
    {
        public ReviewsService(IRepository<Review> repository, IMapper mapper) : base(repository, mapper) { }

        public Review Create(CreateModel createModel)
        {
            Review review = Mapper.Map<Review>(createModel);

            var result = Repository.Add(review);
            return result;
        }

        public List<Review> GetReviewsForUser(string id, Pager pager)
        {
            return Repository
                .FindQuery(x => x.ReviewingUserId == id)
                .Include(s => s.ReviewingUser)
                .Include(m => m.Reviewer)
                .OrderByDescending(x => x.DatePostedUTC)
                .Paginate(pager)
                .ToList();
        }

        public Review Update(CreateModel createModel)
        {
            var baseEntity = Repository.GetSingleOrDefault(x => x.Id == createModel.Id);
            Review review = Mapper.Map(createModel, baseEntity);

            var result = Repository.Update(review);
            return result;
        }
    }
}
