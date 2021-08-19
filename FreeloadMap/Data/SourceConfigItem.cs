using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CsvHelper;
using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;

namespace FreeloadMap.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SourceConfigItem
    {
        [JsonProperty(nameof(SourceType))]
        [Index(0)]
        [Name("sourcetype")]
        public SourceType SourceType { get; set; }

        [JsonProperty(nameof(Path))]
        [Index(1)]
        [Name("path")]
        public string Path { get; set; }
    }
}
