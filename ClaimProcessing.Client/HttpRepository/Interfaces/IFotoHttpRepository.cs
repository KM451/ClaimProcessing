using ClaimProcessing.Shared.FotoUrls.Commands.CreateFotoUrl;

namespace ClaimProcessing.Client.HttpRepository.Interfaces
{
    public interface IFotoHttpRepository
    {
        Task UploadFoto(CreateFotoUrlCommand command);
        Task Delete(int id);
    }
}
