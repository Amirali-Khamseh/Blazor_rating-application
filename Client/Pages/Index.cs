using FinalBlazorIndivisualUser.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalBlazorIndivisualUser.Client.Pages
{
    public partial class Index
    {
        List<User> users;
        public bool ShowMessage { get; set; } = false;
        public string Message { get; set; }
        protected async override Task OnInitializedAsync()
        {
            Message = "Loading...";
            var result = await userRepository.Users();
            if (result.Status)
            {
                ShowMessage = true;
                if (result.Response != null)
                {
                    users = result.Response;
                    Message = "Fetch Data Successfully";
                }else
                {
                    Message = "The List is Empty";
                }
            }else
            {
                Message = "Some thing Went Wrong ";
            }
        }
        public void Login()
        {
            navigationManager.NavigateTo($"authentication/login?returnUrl={Uri.EscapeDataString(navigationManager.Uri)}");
        }
    }
}
