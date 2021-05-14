using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinalBlazorIndivisualUser.Server.Models
{
    public class ProfileService : IProfileService
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _singIn;
        public ProfileService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signIn)
        {
            _userManager = userManager;
            _singIn = signIn;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {

            var user = await _userManager.GetUserAsync(context.Subject);
            List<Claim> claims = new List<Claim>();
            if (user != null && user.Email.Length > 0)
            {

                if (!string.IsNullOrEmpty(user.FullName))
                {
                    claims.Add(new Claim("FullName" , user.FullName));
                }

                claims.Add(new Claim("CurrentUserId", user.Id));

            }
            else
            {
                claims = new List<Claim>();
            }
            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            context.IsActive = (user != null);
        }
    }
}
