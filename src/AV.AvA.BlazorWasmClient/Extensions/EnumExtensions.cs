using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AV.AvA.BlazorWasmClient.Extensions
{
    public static class EnumExtensions
    {
        // taken from https://stackoverflow.com/a/26455406/1200847
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            ?.GetName() ?? enumValue.ToString();
        }
    }
}
