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

        public static readonly string AvLogo = "<defs><clipPath id=\"a\"><path d=\"M213.7 3.9c123.2 61.8 209 197.1 209 353.4 0 7-.1 14.2-.5 21.3a135.5 135.5 0 0 0-78.3 74 362.3 362.3 0 0 0-260.5 0 135.4 135.4 0 0 0-78.2-74c-.4-7-.6-14.2-.6-21.3C4.6 201 90.4 65.7 213.6 3.9Z\"/></clipPath></defs><path fill=\"#ee2729\" d=\"M12 23.8A20.7 20.7 0 0 0 23 4.1a7.2 7.2 0 0 1-4-3.9A19.3 19.3 0 0 1 5.1.2a7.2 7.2 0 0 1-4.1 4v1c0 8.2 4.5 15.4 11 18.6\"/><path fill=\"none\" stroke=\"#050301\" stroke-width=\".3\" d=\"M12 23.8A20.7 20.7 0 0 0 23 4.1a7.2 7.2 0 0 1-4-3.9A19.3 19.3 0 0 1 5.1.2a7.2 7.2 0 0 1-4.1 4v1c0 8.2 4.5 15.4 11 18.6z\"/><g clip-path=\"url(#a)\" transform=\"matrix(.05224 0 0 -.05224 .8 24)\"><path fill=\"#fff\" d=\"M387.6 147 80.4 456 6.2 381.4l307.2-309 74.2 74.6\"/></g><path fill=\"none\" stroke=\"#050301\" stroke-width=\".3\" d=\"m5 .2 15.5 15.4M1 4.1 17 20\"/>";
    }
}
