using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace FreeloadMap.Lib.Data.PictureItemTypes
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PIT_Unknown : IPictureItemData
    {
        public string TypeName { get { return KnownPictureItemTypeNames.Unknown; } }

        public string Name { get; set; }

        public string Path { get; set; }

        public string SerializeData()
        {
            return null;
        }

        public void DeserializeData(string data)
        {
            return;
        }

        public Dictionary<string, string> ExValues { get; set; }
    }
}
