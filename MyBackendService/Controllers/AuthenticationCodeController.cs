using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBackendService.Businesses;
using MyBackendService.Models.DTOs;
using System.Threading.Tasks;

namespace MyBackendService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationCodeController : ApiControllerBase
    {
        private readonly IAuthenticationCodeManager _authenticationCodeManager;

        public AuthenticationCodeController(IAuthenticationCodeManager authenticationCodeManager) =>
            _authenticationCodeManager = authenticationCodeManager;

        [HttpPost]
        public async Task<ActionResult> Post(AuthenticationCodeDto authenticationCodeDto)
        {
            var isSuccess = false;
            var responseMessage = string.Empty;

            await _authenticationCodeManager.AuthenticateCode(Request, authenticationCodeDto,
                onSuccess: () =>
                 {
                     isSuccess = true;
                 },
                onError: (errorMessage) =>
                {
                    isSuccess = false;
                    responseMessage = errorMessage;
                });

            return isSuccess ? Ok() : BadRequest(responseMessage);
        }
    }
}