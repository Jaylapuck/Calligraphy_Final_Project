using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Calligraphy.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ServiceType
    {
        Calligraphy,
        Engraving,
        Event
    }
}