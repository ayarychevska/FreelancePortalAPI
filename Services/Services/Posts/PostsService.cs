using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;
using Services.Models.Posts;

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

        public Post Update(CreateModel createModel)
        {
            var baseEntity = Repository.GetSingleOrDefault(x => x.Id == createModel.Id);
            Post post = Mapper.Map(createModel, baseEntity);

            var result = Repository.Update(post);
            return result;
        }
    }
}
