using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;

namespace FreeloadMap.Lib.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class LocationPictureBinding
    {
        [JsonProperty(nameof(Location))]
        [Index(0)]
        [Name("location")]
        [TypeConverter(typeof(LevelLocation.TC_LevelLocation))]
        public LevelLocation Location { get; set; }

        [JsonProperty(nameof(PictureName))]
        [Index(1)]
        [Name("picturename")]
        public string PictureName { get; set; }

#warning 暂时不精细到市，所以不用
        ///// <remarks>
        ///// 相对于图像变换中心的位置。
        ///// </remarks>
        //[JsonProperty(nameof(RelativePosition))]
        //public Tuple<double,double> RelativePosition { get; set; }

        public static IEnumerable<LocationPictureBinding> FindByPictureName(LocationPictureBinding[] locationPictureBindings, string pictureName)
        {
            IEnumerable<LocationPictureBinding> r = from val in locationPictureBindings
                                                    where val.PictureName == pictureName
                                                    select val;
            return r;
        }

        public LocationPictureBinding GetCopy()
        {
            return (LocationPictureBinding)this.MemberwiseClone();
        }
    }
}
