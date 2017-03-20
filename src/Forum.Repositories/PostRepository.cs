using Forum.Interfaces;
using System;
using Forum.Models;
using System.Threading.Tasks;

namespace Forum.Repositories
{
    public class PostRepository : IPostRepository
    {
        public Task Add(Post post)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Post> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
