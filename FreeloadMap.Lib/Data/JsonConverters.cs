using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace FreeloadMap.Lib.Data
{
    public class JSONConverter_Version : JsonConverter<Version>
    {
        public override Version ReadJson(JsonReader reader, Type objectType, Version existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (hasExistingValue)
            {
                return null;
            }
            else if (reader.TokenType == JsonToken.String)
            {
                string versionText = reader.Value.ToString();
                return Version.Parse(versionText);
            }
            else
            {
                throw new ArgumentException("Non-support format.");
            }
        }

        public override void WriteJson(JsonWriter writer, Version value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteValue(value.ToString());
        }
    }
}
