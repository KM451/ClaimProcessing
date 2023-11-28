using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Test;
using System.Security.Claims;

namespace ClaimProcessing.Api.Service
{
    public class TestProfileService : IProfileService
    {
        private TestUserStore _testUsers;
        public TestProfileService(TestUserStore testUsers)
        {
            _testUsers = testUsers;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = _testUsers.FindBySubjectId(context.Subject.GetSubjectId());
            var claims = new List<Claim>
            {
                new Claim("Email", "AliceSmith@email.com"),
                new Claim("Confirmation", "True"),
            };
            
            var userClaims = user.Claims;
            foreach (var userClaim in userClaims)
            {
                claims.Add(new Claim(userClaim.Type, userClaim.Value));
            }
      
            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = _testUsers.FindBySubjectId(context.Subject.GetSubjectId());
            context.IsActive = user != null;
        }

    }
}
