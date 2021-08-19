using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace FreeloadMap.Lib.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DataFile <TData>
    {
        private string path = null;
        public string Path { get { return path; } }

        private List<TData> datas = new List<TData>();
        [JsonProperty(nameof(Data))]
        public List<TData> Data { get { return datas; } set { datas = value; } }

        private JsonSerializerSettings jsonSerializerSettings;

        public void Load(string path)
        {
            this.path = path;

            DataFile<TData> dataFile = JsonConvert.DeserializeObject<DataFile<TData>>(System.IO.File.ReadAllText(path), jsonSerializerSettings);
            this.Data = dataFile.Data;
        }

        /// <remarks>
        /// 不检查文件存在。
        /// </remarks>
        public void Save(string path)
        {
            this.path = path;

            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(this, jsonSerializerSettings));
        }

        public DataFile(IList<JsonConverter> converters = null)
        {
             jsonSerializerSettings = new JsonSerializerSettings()
             {
                 Formatting = Formatting.Indented,
                 Converters = converters
             };
        }
    }
}
