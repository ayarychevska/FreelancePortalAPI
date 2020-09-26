using AutoMapper;
using Core.Models;
using Core.Repositories.Interfaces;

namespace Services.Services.Posts
{
    class PostsService : BaseService<Post>
    {
        public PostsService(IRepository<Post> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
