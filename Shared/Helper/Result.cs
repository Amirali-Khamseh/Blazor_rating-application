using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FinalBlazorIndivisualUser.Shared.Helper
{
    public class Result<T>
    {
        public Result(T response , bool status , HttpResponseMessage httpResponse)
        {
            Status = status;
            Response = response;
            HttpResponse = httpResponse;
        }
        public bool Status { get; set; }
        public T Response { get; set; }
        public HttpResponseMessage HttpResponse { get; set; }
        public async Task<string> GetBody()
        {
            return await HttpResponse.Content.ReadAsStringAsync();
        }

    }
}
