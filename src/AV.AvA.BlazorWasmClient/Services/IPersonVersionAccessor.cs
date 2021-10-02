using AV.AvA.Model;

namespace AV.AvA.BlazorWasmClient.Services
{
    internal interface IPersonVersionAccessor
    {
        Task<PersonVersion> GetCurrentAsync(int avId);
    }
}
