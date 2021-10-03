using AV.AvA.Model;

namespace AV.AvA.BlazorWasmClient.Services
{
    internal interface IPersonVersionAccessor
    {
        Task<PersonVersion> GetCurrentByAvIdAsync(int avId);
        Task<IReadOnlyCollection<PersonVersion>> GetCurrentAsync();
    }
}
