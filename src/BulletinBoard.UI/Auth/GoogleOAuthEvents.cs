using BulletinBoard.UI.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Security.Claims;

namespace BulletinBoard.UI.Auth
{
    public class GoogleOAuthEvents : OAuthEvents
    {
        private readonly IJwtTokenGenerator _tokenGenerator;

        public GoogleOAuthEvents(IJwtTokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }

        public override Task TicketReceived(TicketReceivedContext context)
        {
            var identity = context.Principal?.Identity as ClaimsIdentity;
            var googleId = identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = identity?.FindFirst(ClaimTypes.Email)?.Value;
            var name = identity?.FindFirst(ClaimTypes.Name)?.Value;

            if (googleId != null && email != null && name != null)
            {
                var jwt = _tokenGenerator.GenerateToken(googleId, email, name);
                identity?.AddClaim(new Claim("access_token", jwt));
            }

            return base.TicketReceived(context);
        }
    }
}