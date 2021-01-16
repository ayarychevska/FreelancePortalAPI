using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public List<Post> GetMyPosts(string id)
        {
            return Repository
                .FindQuery(x => x.UserId == id)
                .Include(s => s.User)
                .Include(m => m.Subject)
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
