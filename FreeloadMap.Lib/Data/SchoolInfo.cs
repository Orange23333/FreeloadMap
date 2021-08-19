using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;

namespace FreeloadMap.Lib.Data
{
    // 默认不存在相同的学校
    [JsonObject(MemberSerialization.OptIn)]
    public class SchoolInfo
    {
        [JsonProperty(nameof(Name))]
        [Index(0)]
        [Name("name")]
        public string Name { get; set; }

        // 应当对应PictureSetItem中的Name
        [JsonProperty(nameof(Location))]
        [Index(1)]
        [Name("location")]
        [TypeConverter(typeof(LevelLocation.TC_LevelLocation))]
        public LevelLocation Location { get; set; }

        [JsonProperty(nameof(IconPath))]
        [Index(2)]
        [Name("iconpath")]
        [Default("")]
        public string IconPath { get; set; }

        public SchoolInfo GetCopy()
        {
            return (SchoolInfo)this.MemberwiseClone();
        }

        public static IEnumerable<SchoolInfo> FindBySchoolName(SchoolInfo[] schoolInfos, string schoolName)
        {
            IEnumerable<SchoolInfo> r = from val in schoolInfos
                                        where val.Name == schoolName
                                        select val;
            return r;
        }
        public static IEnumerable<SchoolInfo> FindByLocation(SchoolInfo[] schoolInfos, LevelLocation location)
        {
            IEnumerable<SchoolInfo> r = from val in schoolInfos
                                        where val.Location == location
                                        select val;
            return r;
        }
    }
}
