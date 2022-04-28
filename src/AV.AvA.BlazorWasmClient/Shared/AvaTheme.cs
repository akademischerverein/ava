using MudBlazor;

namespace AV.AvA.BlazorWasmClient.Shared
{
    internal static class AvaTheme
    {
        public static readonly MudTheme Default = new()
        {
            Palette = new Palette()
            {
                Primary = Colors.Red.Darken2,
                Secondary = Colors.Grey.Darken1,
                AppbarBackground = Colors.Red.Darken2,
            },
        };
    }
}
