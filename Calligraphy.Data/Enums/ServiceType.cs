using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calligraphy.Data.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ServiceType
    {
        Calligraphy,
        Engraving,
        Event,
    }
}