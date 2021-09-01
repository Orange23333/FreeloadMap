using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using FreeloadMap.Lib.Utility;

namespace FreeloadMap.Lib.Data.PictureItemTypes
{
#warning 可以直接把这个改成接口，让PIControl继承。不过这样web段就需要一个类来实现这个接口会有点不方便。
    // 暂不支持自定义z序（我不暂时需要），不过这里还是应当保留z序字段。
    // Name(目前自动设置为文件名)|Position|TransformOrigin(目前自动设置为0.0,0.0)|Scale(目前自动设置为1.0,1.0)|RotateAngle(目前自动设置为0)|Opacity(目前自动设置为1.0)|ZIndex|Path|ExValues(目前自动设置为null)
    [JsonObject(MemberSerialization.OptIn)]
    public class PIT_Image : IPictureItemData
    {
        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented
        };

        #region ==IPictureItemData==
        public string TypeName { get { return KnownPictureItemTypeNames.Image; } }

        public string Name { get; set; }

        public string Path { get; set; }

        public string SerializeData()
        {
            string json = JsonConvert.SerializeObject(this, jsonSerializerSettings);
            return json;
        }

        public void DeserializeData(string data)
        {
            PIT_Image image = JsonConvert.DeserializeObject<PIT_Image>(data);

            this.Position = image.Position;
            this.TransformOrigin = image.TransformOrigin;
            this.Scale = image.Scale;
            this.RotateAngle = image.RotateAngle;
            this.Opacity = image.Opacity;
            this.ZIndex = image.ZIndex;
        }

        public Dictionary<string, string> ExValues { get; set; }
        #endregion

        [JsonProperty(nameof(Position))]
        public Tuple<double, double> Position { get; set; }

        [JsonProperty(nameof(TransformOrigin))]
        public Tuple<double, double> TransformOrigin { get; set; }

        [JsonProperty(nameof(Scale))]
        public Tuple<double, double> Scale { get; set; }

        [JsonProperty(nameof(RotateAngle))]
        public double RotateAngle { get; set; }

        [JsonProperty(nameof(Opacity))]
        public double Opacity { get; set; }

        [JsonProperty(nameof(ZIndex))]
        public int ZIndex { get; set; }

        /// <remarks>
        /// 实际调用Create(string,double,double,int)。这只是为了某种方便设置的函数
        /// </remarks>
        public static PIT_Image Create(string path, double x, double y, int zIndex)
        {
            return Create(x, y, zIndex, path);
        }
        public static PIT_Image Create(double x, double y, int zIndex, string path)
        {
            return new PIT_Image()
            {
                Name = System.IO.Path.GetFileName(path),
                Position = new Tuple<double, double>(x, y),
                TransformOrigin = new Tuple<double, double>(0.0, 0.0),
                Scale = new Tuple<double, double>(1.0, 1.0),
                RotateAngle = 0.0,
                Opacity = 1.0,
                ZIndex = zIndex,
                Path = path,
                ExValues = null
            };
        }

        public static bool Equals(PIT_Image x, PIT_Image y)
        {
            return String.Equals(x.Name, y.Name); // https://github.com/dotnet/core/issues/5078
        }

        public PIT_Image ToAbsolutePath(string basePath)
        {
            PIT_Image pictureItemStructure = (PIT_Image)this.MemberwiseClone();
            pictureItemStructure.Path = GetAbsolutePath(basePath, pictureItemStructure.Path);
            return pictureItemStructure;
        }
        public PIT_Image ToRelativePath(string basePath)
        {
            PIT_Image pictureItemStructure = (PIT_Image)this.MemberwiseClone();
            pictureItemStructure.Path = GetRelativePath(basePath, pictureItemStructure.Path);
            return pictureItemStructure;
        }
        public static string GetAbsolutePath(string basePath, string relativePath)
        {
#if _SYS_PATH_URI
            //return new Uri(new Uri(basePath, UriKind.Absolute), relativePath).AbsoluteUri;
            return new Uri(new Uri(basePath, UriKind.Absolute), new Uri(relativePath, UriKind.Relative)).LocalPath;
#elif _LIB_PATH_URI
            return FkPath.GetAbsolutePath(basePath, relativePath, FkPath.DirectorySeparator.Backslash, false);
#endif
        }
        /// <param name="basePath">应为工程文件位置。</param>
        public static string GetRelativePath(string basePath, string absolutePath)
        {
#if _SYS_PATH_URI
            return new Uri(basePath, UriKind.Absolute).MakeRelativeUri(new Uri(absolutePath, UriKind.Absolute)).ToString(); // 测试结果这是对的，但我也不知道Uri这什么语法、原理。
#elif _LIB_PATH_URI
            return FkPath.GetRelativePath(basePath, absolutePath, FkPath.DirectorySeparator.Backslash);
#endif
        }

        public static Dictionary<string, PIT_Image> ToDictionaryByName(IEnumerable<PIT_Image> pictureItemStructures)
        {
            Dictionary<string, PIT_Image> r = new Dictionary<string, PIT_Image>();

            lock (pictureItemStructures)
            {
                foreach (PIT_Image pictureItemStructure in pictureItemStructures)
                {
                    r.Add(pictureItemStructure.Name, pictureItemStructure);
                }
            }

            return r;
        }
    }
}
