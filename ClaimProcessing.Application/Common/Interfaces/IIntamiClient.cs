namespace ClaimProcessing.Application.Common.Interfaces
{
    public interface IIntamiClient
    {
        Task<string> GetZipCode(string searchFilter, CancellationToken cancellationToken);
        Task<string> GetCity(string searchFilter, CancellationToken cancellationToken);
    }
}
