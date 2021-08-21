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

using FreeloadMap.Data.SourceTypes;
using FreeloadMap.Lib.Data;

namespace FreeloadMap.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ProgramConfigFile
    {
        private string path = null;
        public string Path { get { return path; } set { path = value; } }

        //private List<ProgramConfigItem> programConfigItems = new List<ProgramConfigItem>();
        //[JsonProperty(nameof(ProgramConfigItems))]
        //public List<ProgramConfigItem> ProgramConfigItems { get { return programConfigItems; } set { programConfigItems = value; } }

        private Dictionary<string, string> configs = new Dictionary<string, string>();
        [JsonProperty(nameof(Configs))]
        public Dictionary<string, string> Configs { get { return configs; } set { configs = value; } }

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
                this.Configs = ProgramConfigItem.ToDictionary(csv.GetRecords<ProgramConfigItem>());
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
                csv.WriteRecords(ProgramConfigItem.ToArray(this.Configs));
            }

            this.path = path;
        }
    }
}
