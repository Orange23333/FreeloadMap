using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;

namespace FreeloadMap.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MapConfigItem
    {
        [JsonProperty(nameof(Title))]
        [Index(0)]
        [Name("title")]
        public string Title { get; set; }

        [JsonProperty(nameof(Width))]
        [Index(1)]
        [Name("width")]
        public string Width { get; set; }

        [JsonProperty(nameof(Height))]
        [Index(2)]
        [Name("height")]
        public string Height { get; set; }

        [JsonProperty(nameof(LocationFilter))]
        [Index(3)]
        [Name("locationfilter")]
        public string LocationFilter { get; set; }

        [JsonProperty(nameof(FooterComment))]
        [Index(4)]
        [Name("footercomment")]
        public string FooterComment { get; set; }
    }
}
