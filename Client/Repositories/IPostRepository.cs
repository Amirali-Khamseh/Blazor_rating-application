using FinalBlazorIndivisualUser.Shared.Entities;
using FinalBlazorIndivisualUser.Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalBlazorIndivisualUser.Client.Repositories
{
    public interface IPostRepository
    {
        Task<Result<Post>> CreatePost(Post post);
        Task<Result<bool>> DeletePost(int id);
        Task<Result<List<Post>>> GetPosts();
        Task<Result<List<Post>>> GetPostsOrderByLike();
        Task<Result<Post>> GetPostWithId(int Id);
        Task<Result<bool>> LikePost(int Id);
        Task<Result<bool>> UpdatePost(Post post);
    }
}
