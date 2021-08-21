using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace FreeloadMap.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MapConfigFile
    {
        private string path = null;
        public string Path { get { return path; } set { path = value; } }

        private List<MapConfigItem> mapConfigItems = new List<MapConfigItem>();
        [JsonProperty(nameof(MapConfigItems))]
        public List<MapConfigItem> MapConfigItems { get { return mapConfigItems; } set { mapConfigItems = value; } }

        private static readonly CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            AllowComments = true,
            Encoding = Encoding.UTF8,
            PrepareHeaderForMatch = args => args.Header.ToLower()
        };

        public void Load(string path)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, csvConfiguration))
            {
                this.MapConfigItems = csv.GetRecords<MapConfigItem>().ToList();
            }

            this.path = path;
        }

        /// <remarks>
        /// 不检查文件存在。
        /// </remarks>
        public void Save(string path)
        {
            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(MapConfigItems);
            }

            this.path = path;
        }
    }
}
