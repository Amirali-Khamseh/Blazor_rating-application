using FinalBlazorIndivisualUser.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalBlazorIndivisualUser.Client.Pages.CategoryComponents
{
    public partial class List
    {
        public List<Category> categories { get; set; }
        public string Message = null;
        public bool ShowMessage = false;
        protected async override Task OnInitializedAsync()
        {
            var result = await categoryRepository.GetCategories();
            ShowMessage = true;
            if (result.Status)
            {
                if (result.Response != null)
                {
                    if (result.Response.Count > 0)
                    {
                        Message = $"Total Categories : {result.Response.Count} ";
                        categories = result.Response;
                    }
                    else
                    {
                        Message = "There is no Category";
                        categories = new List<Category>();
                    }
                }else
                {
                    Message = $"Pending...🥱";
                }
              
            }else
            {
                Message = "Something Went Wrong 🤕";
                categories = new List<Category>();
            }
        }
    }
}
