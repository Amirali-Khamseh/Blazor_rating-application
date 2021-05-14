using FinalBlazorIndivisualUser.Client.Services;
using FinalBlazorIndivisualUser.Shared.Entities;
using FinalBlazorIndivisualUser.Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalBlazorIndivisualUser.Client.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly IHttpService _http;
        private readonly string _URL = "api/users";
        public UserRepository(IHttpService http)
        {
            _http = http;
        }

        public async Task<Result<List<User>>> Users()
        {
            // api/users/list => Request
            var response = await _http.GetData<List<User>>($"{_URL}/list");

            return response;
        }
    }
}
