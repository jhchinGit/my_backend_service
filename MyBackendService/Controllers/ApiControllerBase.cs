using Microsoft.AspNetCore.Mvc;
using MyBackendService.Models;

namespace MyBackendService.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected Response GenerateResponse(object data, string error = null, string warning = null)
        {
            return new Response
            {
                Data = data,
                Error = error,
                Warning = warning
            };
        }
    }
}