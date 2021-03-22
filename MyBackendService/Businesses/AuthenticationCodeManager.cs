using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyBackendService.Models;
using MyBackendService.Models.DTOs;
using MyBackendService.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace MyBackendService.Businesses
{
    public class AuthenticationCodeManager : IAuthenticationCodeManager
    {
        private readonly ITotpSetupGenerator _totpSetupGenerator;
        private readonly ITotpValidator _totpValidator;
        private readonly RepositoryContext _context;

        public AuthenticationCodeManager(
            ITotpSetupGenerator totpSetupGenerator,
            ITotpValidator totpValidator,
            RepositoryContext context)
        {
            _totpSetupGenerator = totpSetupGenerator;
            _totpValidator = totpValidator;
            _context = context;
        }

        public async Task AuthenticateCode(HttpRequest request, AuthenticationCodeDto authenticationCodeDto,
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

            var userProfile = await _context.UserProfiles
                .SingleOrDefaultAsync(i => i.Username == authenticationCodeDto.Username && i.IsActive == true);

            if (userProfile == null)
            {
                onError("Invalid Input");
                return;
            }

            userProfile.IsTotpValid = true;
            await _context.SaveChangesAsync();

            onSuccess();
        }
    }
}