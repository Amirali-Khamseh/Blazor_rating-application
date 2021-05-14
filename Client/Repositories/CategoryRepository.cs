using FinalBlazorIndivisualUser.Client.Services;
using FinalBlazorIndivisualUser.Shared.Entities;
using FinalBlazorIndivisualUser.Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalBlazorIndivisualUser.Client.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IHttpService _http;
        private readonly string _URL = "api/categories";
        public CategoryRepository(IHttpService http)
        {
            _http = http;
        }

        public async Task<Result<bool>> CreateCategory(Category category)
        {
            var response = await _http.PostAsync<Category, bool>($"{_URL}/create" ,category);
            return response;
        }

        public async Task<Result<List<Category>>> GetCategories()
        {
            var response = await _http.GetData<List<Category>>($"{_URL}/list");

            return response;
        }
    }
}
