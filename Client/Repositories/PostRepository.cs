using FinalBlazorIndivisualUser.Client.Services;
using FinalBlazorIndivisualUser.Shared.Entities;
using FinalBlazorIndivisualUser.Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalBlazorIndivisualUser.Client.Repositories
{
    public class PostRepository : IPostRepository 
    {
        private readonly IHttpService _http;
        private readonly string _URL = "api/posts";

        public PostRepository(IHttpService http)
        {
            _http = http;
        }

        public async Task<Result<Post>> CreatePost(Post post)
        {
            var response = await _http.PostAsync<Post , Post>($"{_URL}/create" , post);

            return response;
        } 

        public async Task<Result<List<Post>>> GetPosts()
        {
            var response = await _http.GetData<List<Post>>($"{_URL}/list");

            return response;
        }
        public async Task<Result<List<Post>>> GetPostsOrderByLike()
        {
            var response = await _http.GetData<List<Post>>($"{_URL}/likeOrder");

            return response;
        }

        public async Task<Result<bool>> DeletePost(int id)
        {
            var response = await _http.PostAsync<int , bool>($"{_URL}/delete" , id);

            return response;
        }

        public async Task<Result<Post>> GetPostWithId(int Id)
        {
            //var response = await _http.GetData<Post>($"{_URL}/detail/{Id}");
            var response = await _http.PostAsync<int, Post>($"{_URL}/detail" , Id);
            return response;
        }

        public async Task<Result<bool>> UpdatePost(Post post)
        {
            var response = await _http.PostAsync<Post, bool>($"{_URL}/update" , post);

            return response;
        }

        public async Task<Result<bool>> LikePost(int Id)
        {
            var response = await _http.PostAsync<int, bool>($"{_URL}/like" , Id);

            return response;
        }
    }
}
