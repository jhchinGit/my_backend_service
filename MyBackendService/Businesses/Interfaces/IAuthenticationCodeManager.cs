using Microsoft.AspNetCore.Http;
using MyBackendService.Models.DTOs;
using System;
using System.Threading.Tasks;

namespace MyBackendService.Businesses
{
    public interface IAuthenticationCodeManager
    {
        Task AuthenticateCode(HttpRequest request, AuthenticationCodeDto authenticationCodeDto,
            Action onSuccess, Action<string> onError);
    }
}