namespace ClaimProcessing.Application.Common.Interfaces
{
    public interface IIntamiClient
    {
        Task<string> GetCity(string searchFilter, CancellationToken cancellationToken);
    }
}
