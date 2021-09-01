using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace FreeloadMap.Lib.Data.PictureItemTypes
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PIT_SVG : IPictureItemData
    {
        #region ==IPictureItemData==
        public string TypeName { get { return KnownPictureItemTypeNames.SVG; } }

        public string Name { get; set; }

        public string Path { get; set; }

        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented
        };

        public string SerializeData()
        {
            string json = JsonConvert.SerializeObject(this, jsonSerializerSettings);
            return json;
        }

        public void DeserializeData(string data)
        {
#warning HERE
            PIT_SVG template = JsonConvert.DeserializeObject<PIT_SVG>(data);

            this.Property = template.Property;
            //...
        }

        public Dictionary<string, string> ExValues { get; set; }
        #endregion
    }
}
