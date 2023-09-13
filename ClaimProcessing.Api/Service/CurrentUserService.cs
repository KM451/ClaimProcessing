using ClaimProcessing.Application.Common.Interfaces;
using IdentityModel;
using System.Security.Claims;

namespace ClaimProcessing.Api.Service
{
    public class CurrentUserService : ICurrentUserService
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public bool IsAuthenticated { get; set; }
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var email = httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.Email);
            Email = email;
            IsAuthenticated = !string.IsNullOrEmpty(email);

            var name = httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.Name);
            Name = name;
        }
    }
}
    