using FinalBlazorIndivisualUser.Client.Repositories;
using FinalBlazorIndivisualUser.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlazorIndivisualUser.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("FinalBlazorIndivisualUser.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                ;
            //.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>()
            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("FinalBlazorIndivisualUser.ServerAPI"));
            builder.Services.AddScoped<IHttpService, HttpService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddApiAuthorization();

            await builder.Build().RunAsync();
        }
    }
}
