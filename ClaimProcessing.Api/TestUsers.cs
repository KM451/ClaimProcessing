using ClaimProcessing.Infrastructure.Identity;
using Duende.IdentityServer.Test;
using IdentityModel;
using System.Security.Claims;

namespace ClaimProcessing.Api
{
    public class TestUsers : ApplicationUser
    {
        public static List<TestUser> Users
        {
            get
            {
                return new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "4B434A88-212D-4A4D-A17C-F35102D73CBB",
                        Username = "alice",
                        Password = "Pass123$",
                        Claims = new List<Claim>
                        {
                            new Claim("Email", "AliceSmith@email.com"),
                            new Claim("Confirmation", "True"),
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.Role, "Admin"),
                        }
                    }
                };
            }
        }
    }
}
