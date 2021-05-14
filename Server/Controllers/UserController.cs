using FinalBlazorIndivisualUser.Server.Models;
using FinalBlazorIndivisualUser.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalBlazorIndivisualUser.Server.Controllers
{
    [ApiController]
    [Route("api/users")]
    // api/users/list
    public class UserController
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet("list")]
        // Listener
        public async Task<List<User>> Users()
        {
            var result = _userManager.Users.ToList();
            List<User> users;
            if (result != null && result.Count > 0)
            {
                users = new List<User>();
                foreach (var item in result)
                {
                    users.Add(new User { 
                      Id = item.Id , 
                      Email = item.Email ,
                      FullName = item.FullName
                    });
                }
            }else
            {
                users = new List<User>();
            }
            return await Task.FromResult(users);
        }
    }
}
