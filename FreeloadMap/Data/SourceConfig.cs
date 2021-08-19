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
    public class SourceConfig
    {
        private string path = null;
        public string Path { get { return path; } set { path = value; } }

        private List<SourceConfigItem> sourceConfigItems = new List<SourceConfigItem>();
        [JsonProperty(nameof(SourceConfigItems))]
        public List<SourceConfigItem> SourceConfigItems { get { return sourceConfigItems; } set { sourceConfigItems = value; } }

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
                this.SourceConfigItems = csv.GetRecords<SourceConfigItem>().ToList();
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
                csv.WriteRecords(SourceConfigItems);
            }

            this.path = path;
        }
    }
}
