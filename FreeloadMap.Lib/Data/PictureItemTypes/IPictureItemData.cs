using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace FreeloadMap.Lib.Data.PictureItemTypes
{
    [JsonObject(MemberSerialization.OptIn)]
    public interface IPictureItemData
    {
        //public bool Is占位符；Is不是图像
        [JsonProperty(nameof(TypeName))]
        string TypeName { get; }

        [JsonProperty(nameof(Name))]
        public string Name { get; set; }

        public static readonly string NoPath = null;
        [JsonProperty(nameof(Path))]
        public string Path { get; set; }

        public const string DataWarpperJsonObjectName = "PictureItemData";
        [JsonProperty(DataWarpperJsonObjectName)]
        virtual string DataWarpper
        {
            get
            {
                return SerializeData();
            }
            set
            {
                DeserializeData(value);
            }
        }

        [JsonProperty(nameof(ExValues))]
        public Dictionary<string, string> ExValues { get; set; }

        string SerializeData();

        void DeserializeData(string data);

        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented
        };

        public virtual string Serialize()
        {
            string json = JsonConvert.SerializeObject((IPictureItemData)this, jsonSerializerSettings);
            return json;
        }

        public virtual void Deserialize(string json)
        {
            IPictureItemData pictureItemData = JsonConvert.DeserializeObject<IPictureItemData>(json);

            this.Name = pictureItemData.Name;
            this.Path = pictureItemData.Path;
            this.DataWarpper = pictureItemData.DataWarpper;
            this.ExValues = pictureItemData.ExValues;
        }

        public static bool Equals(IPictureItemData x, IPictureItemData y)
        {
            return String.Equals(x.Name, y.Name);
        }

        public static Dictionary<string, IPictureItemData> ToDictionaryByName(IEnumerable<IPictureItemData> pictureItemStructures)
        {
            Dictionary<string, IPictureItemData> r = new Dictionary<string, IPictureItemData>();

            lock (pictureItemStructures)
            {
                foreach (IPictureItemData pictureItemStructure in pictureItemStructures)
                {
                    r.Add(pictureItemStructure.Name, pictureItemStructure);
                }
            }

            return r;
        }
    }
}
