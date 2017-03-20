using Forum.Models;
using System;
using System.Threading.Tasks;

namespace Forum.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> Get(Guid id);
        Task Add(Post post);
        Task Delete(Guid id);
        Task Update(Post post);
    }
}
