using Forum.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Repository.Interfaces
{
    public interface IPostRepository
    {
        IEnumerable<Post> FetchAllPosts();
        IEnumerable<Post> FetchAllPostsByTopic(Guid? topicId);
        Post FetchPostById(Guid? id);
        Post Insert(Post entity);
        Post Update(Post entity);
        Post Delete(Post entity);
    }
}
