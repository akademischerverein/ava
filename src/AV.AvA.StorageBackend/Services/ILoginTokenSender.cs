namespace AV.AvA.StorageBackend.Services
{
    public interface ILoginTokenSender
    {
        Task SendLoginTokenAsync(string loginToken);
    }
}
