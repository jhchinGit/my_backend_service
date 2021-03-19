using Microsoft.AspNetCore.Http;
using MyBackendService.Models.DTOs;
using System;

namespace MyBackendService.Businesses
{
    public interface IAuthenticationCodeManager
    {
        void AuthenticateCode(HttpRequest request, AuthenticationCodeDto authenticationCodeDto,
            Action onSuccess, Action<string> onError);
    }
}