namespace ClaimProcessing.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string Email { get; set; }
        string Name { get; set; }
        string UserId { get; set; } 
        bool IsAuthenticated { get; set; }
    }
}
