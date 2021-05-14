using FinalBlazorIndivisualUser.Server.Data;
using FinalBlazorIndivisualUser.Server.Models;
using FinalBlazorIndivisualUser.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalBlazorIndivisualUser.Server.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostController
    {
        private readonly ApplicationDbContext _applicationDb;
        private readonly UserManager<ApplicationUser> _userManager;
        public PostController(ApplicationDbContext appDb, UserManager<ApplicationUser> userManager)
        {
            _applicationDb = appDb;
            _userManager = userManager;
        }

        [HttpPost("create")]
        public async Task<Post> CreatePost([FromBody] Post post)
        {
            var userData = await _userManager.FindByIdAsync(post.UserId);
            if (userData != null)
            {
                _applicationDb.AspNetPosts.Add(new Models.PostModel
                {
                    Title = post.Title,
                    Description = post.Description,
                    CreatedDate = DateTime.Now,
                    Like = 0,
                    UserId = userData.Id,
                    CategoryId = post.CategoryId,
                    Image = post.Image
                });

                await _applicationDb.SaveChangesAsync();
                return await Task.FromResult(post);
            }
            else
            {
                return await Task.FromResult(post);
            }

        }

        [HttpGet("list")]

        public async Task<List<Post>> Posts()
        {
            List<Post> posts;
            var result = _applicationDb.AspNetPosts.Include(p => p.Category).ToList();

            if (result != null)
            {
                posts = new List<Post>();
                foreach (PostModel item in result)
                {
                    posts.Add(new Post
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Image = item.Image,
                        Like = item.Like,
                        CreatedDate = item.CreatedDate,
                        UserId = item.UserId,
                        Category = new Category
                        {
                            Id = item.Category.Id,
                            Name = item.Category.Name
                        },
                        Description = item.Description
                    });
                }
                
                return await Task.FromResult(posts);
            }
            else
            {
                return await Task.FromResult(new List<Post>());
            }
        }
        [HttpGet("likeOrder")]

        public async Task<List<Post>> LikeOrderPosts()
        {
            List<Post> posts;
            var result = _applicationDb.AspNetPosts.Include(p => p.Category).ToList();

            if (result != null)
            {
                posts = new List<Post>();
                foreach (PostModel item in result)
                {
                    posts.Add(new Post
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Image = item.Image,
                        Like = item.Like,
                        CreatedDate = item.CreatedDate,
                        UserId = item.UserId,
                        Category = new Category
                        {
                            Id = item.Category.Id,
                            Name = item.Category.Name
                        },
                        Description = item.Description
                    });
                }
                posts = posts.OrderByDescending(p => p.Like).ToList();
                return await Task.FromResult(posts);
            }
            else
            {
                return await Task.FromResult(new List<Post>());
            }
        }

        [HttpPost("delete")]
        public async Task<bool> DeletePost([FromBody] int Id)
        {
            var result = _applicationDb.AspNetPosts.Where(p => p.Id == Id).FirstOrDefault();
            if (result != null)
            {
                _applicationDb.Remove(result);
                await _applicationDb.SaveChangesAsync();
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }

        [HttpPost("detail")]
        public async Task<Post> GetPost([FromBody]int Id)
        {
            var result = _applicationDb.AspNetPosts.Include(p => p.Category).FirstOrDefault(p => p.Id == Id);
            Post post;
          
            if (result != null)
            {
                post = new Post
                {
                    Id = result.Id , 
                    Title = result.Title , 
                    Image = result.Image , 
                    Description = result.Description , 
                    CreatedDate = result.CreatedDate , 
                    CategoryId = result.CategoryId , 
                    UserId = result.UserId , 
                    Like = result.Like,
                    Category = new Category
                    {
                        Id = result.Category.Id , 
                        Name = result.Category.Name
                    }
                };
                return await Task.FromResult(post);
            }else
            {
                return await Task.FromResult(new Post());
            }
        }

        [HttpPost("update")]
        public async Task<bool> UpdatePost([FromBody] Post post)
        {
            PostModel model = new PostModel
            {
                Id = post.Id,
                Title = post.Title,
                Image = post.Image,
                Description = post.Description,
                CreatedDate = post.CreatedDate,
                CategoryId = post.CategoryId,
                UserId = post.UserId,
                Like = post.Like
            };
            _applicationDb.AspNetPosts.Update(model);
            await _applicationDb.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        [HttpPost("like")]
        public async Task<bool> LikePost([FromBody]int Id)
        {
            var post = _applicationDb.AspNetPosts.FirstOrDefault(p => p.Id == Id);

            if (post != null)
            {
                post.Like = post.Like + 1;
                await _applicationDb.SaveChangesAsync();
                return await Task.FromResult(true);
            }else
            {
                return await Task.FromResult(false);
            }
        }
    }
}
