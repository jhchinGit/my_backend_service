using Microsoft.AspNetCore.Http;
using MyBackendService.Models;
using MyBackendService.Models.DTOs;
using MyBackendService.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace MyBackendService.Businesses
{
    public class AuthenticationCodeManager : IAuthenticationCodeManager
    {
        private readonly ITotpSetupGenerator _totpSetupGenerator;
        private readonly ITotpValidator _totpValidator;

        public AuthenticationCodeManager(
            ITotpSetupGenerator totpSetupGenerator,
            ITotpValidator totpValidator)
        {
            _totpSetupGenerator = totpSetupGenerator;
            _totpValidator = totpValidator;
        }

        public void AuthenticateCode(HttpRequest request, AuthenticationCodeDto authenticationCodeDto,
            Action onSuccess, Action<string> onError)
        {
            if (request == null || !request.Headers.ContainsKey("Authorization") ||
                authenticationCodeDto == null || authenticationCodeDto.AuthenticationCode == null)
            {
                onError("Invalid Input");
                return;
            }

            JwtSecurityToken token = null;

            try
            {
                var jwt = request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                token = new JwtSecurityToken(jwt);
            }
            catch (Exception ex)
            {
                MessageQueueService.Send(TopicKey.TraceLog,
                "AuthenticationCodeManager.cs - AuthenticateCode(HttpRequest request, AuthenticationCodeDto authenticationCodeDto," +
                "Action onSuccess, Action<string> onError), fail to decode token, error: " + ex.Message);
                onError("Invalid Input");
                return;
            }

            if (token == null || token.Payload.Count != 11 || !token.Payload.ContainsKey("client_id") ||
                string.IsNullOrWhiteSpace(token.Payload["client_id"] as string))
            {
                onError("Invalid Input");
                return;
            }

            var key = token.Payload["client_id"] as string;

            var setupGeneratorKey = _totpSetupGenerator.Generate($"{key}muffinsdnbhdwestworld");
            var isValidTotp = _totpValidator.IsValidTotp($"{key}muffinsdnbhdwestworld", 
                int.Parse(authenticationCodeDto.AuthenticationCode));

            if (!isValidTotp)
            {
                onError("Invalid Input");
                return;
            }

            onSuccess();
        }
    }
}