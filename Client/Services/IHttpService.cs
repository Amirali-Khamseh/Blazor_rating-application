using FinalBlazorIndivisualUser.Shared.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalBlazorIndivisualUser.Client.Services
{
    public interface IHttpService
    {
        Task<Result<T>> GetData<T>(string url);
        Task<Result<TResponse>> PostAsync<T, TResponse>(string url, T data);
    }
}
