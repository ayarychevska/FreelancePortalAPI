using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Services.Enums;
using Services.Models.Common;
using Services.Models.Posts;
using System.Collections.Generic;
using System.Linq;

namespace Services.Services.Posts
{
    public class PostsService : BaseService<Post>
    {
        public PostsService(IRepository<Post> repository, IMapper mapper) : base(repository, mapper) { }

        public Post Create(CreateModel createModel)
        {
            Post post = Mapper.Map<Post>(createModel);

            var result = Repository.Add(post);
            return result;
        }

        public List<Post> GetMyPosts(string id, FilterModel filter, Pager pager)
        {
            #region Filter

            var predicate = PredicateBuilder.New<Post>(false);

            if (filter.SubjectId.HasValue)
                predicate.And(q => q.SubjectId == filter.SubjectId);

            if (filter.DateFromUTC.HasValue)
                predicate.And(q => q.DateUTC.Date >= filter.DateFromUTC.Value.Date);

            if (filter.DateUntilUTC.HasValue)
                predicate.And(q => q.DateUTC.Date <= filter.DateUntilUTC.Value.Date);

            if (!string.IsNullOrEmpty(filter.Title))
                predicate.And(q => q.Title.Contains(filter.Title));

            predicate.And(x => x.UserId == id);

            #endregion

            return Repository
                .FindQuery(predicate)
                .Include(s => s.User)
                .Include(m => m.Subject)
                .Paginate(pager)
                .ToList();
        }

        public List<Post> GetList(FilterModel filter, Pager pager)
        {
            #region Filter

            var predicate = PredicateBuilder.New<Post>(false);

            if (filter.SubjectId.HasValue)
                predicate.And(q => q.SubjectId == filter.SubjectId);

            if (filter.DateFromUTC.HasValue)
                predicate.And(q => q.DateUTC.Date >= filter.DateFromUTC.Value.Date);

            if (filter.DateUntilUTC.HasValue)
                predicate.And(q => q.DateUTC.Date <= filter.DateUntilUTC.Value.Date);

            if (!string.IsNullOrEmpty(filter.Title))
                predicate.And(q => q.Title.Contains(filter.Title));

            predicate.And(q => q.Status == (int)PostStatus.Published);
            #endregion

            return Repository
                .FindQuery(predicate)
                .Include(s => s.User)
                .Include(m => m.Subject)
                .Paginate(pager)
                .ToList();
        }

        public Post GetPostViewModel(long id)
        {
            return Repository
                .FindQuery(x => x.Id == id)
                .Include(s => s.User)
                .Include(m => m.Subject)
                .SingleOrDefault();
        }

        public Post UpdateStatus(long id, int status)
        {
            var entity = Repository.GetSingleOrDefault(x => x.Id == id);
            entity.Status = status;

            var result = Repository.Update(entity);
            return result;
        }

        public Post Update(CreateModel createModel)
        {
            var baseEntity = Repository.GetSingleOrDefault(x => x.Id == createModel.Id);
            Post post = Mapper.Map(createModel, baseEntity);

            var result = Repository.Update(post);
            return result;
        }
    }
}
