using FinalBlazorIndivisualUser.Server.Data;
using FinalBlazorIndivisualUser.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalBlazorIndivisualUser.Server.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController
    {
        private readonly ApplicationDbContext _applicationDb;
        public CategoryController(ApplicationDbContext app)
        {
            _applicationDb = app;
        }

        [HttpPost("create")]
        public async Task<bool> Create([FromBody] Category category)
        {
            _applicationDb.AspNetCategories.Add(new Models.CategoryModel
            {
                Name = category.Name
            });
            await _applicationDb.SaveChangesAsync();

            return true;
        }

        [HttpGet("list")]
        public async Task<List<Category>> Categories()
        {
            List<Category> categories;
            var result = _applicationDb.AspNetCategories.ToList();
            if (result != null && result.Count > 0)
            {
                categories = new List<Category>();
                foreach (var item in result)
                {
                    categories.Add(new Category
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
                return await Task.FromResult(categories);
            }
            else
            {
                return await Task.FromResult(new List<Category>());
            }
        }
    }
}
