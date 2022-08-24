using Forum.Domain.Models;
using Forum.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services.Interfaces
{
    public interface IPostService
    {
        List<Post> FetchAllPosts();
        Post FetchPostById(Guid? id);
        List<PostViewModel> FetchPostsForTopic(Guid topicId);
        PostViewModel FetchPostViewModelById(Guid id);
        PostViewModel FetchPostViewModel(Post entity);
        PostViewModel CreatePost(Post entity);
        PostViewModel UpdatePost(Post entity);
        PostViewModel DeletePost(Post entity);
        bool PostExists(Guid id);
    }
}
