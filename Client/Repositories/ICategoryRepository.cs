using FinalBlazorIndivisualUser.Shared.Entities;
using FinalBlazorIndivisualUser.Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalBlazorIndivisualUser.Client.Repositories
{
    public interface ICategoryRepository
    {
        Task<Result<bool>> CreateCategory(Category category);
        Task<Result<List<Category>>> GetCategories();
    }
}
