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
    public class SourceConfigFile
    {
        private string path = null;
        public string Path { get { return path; } set { path = value; } }

        private List<SourceConfigItem> sourceConfigItems = new List<SourceConfigItem>();
        [JsonProperty(nameof(SourceConfigItems))]
        public List<SourceConfigItem> SourceConfigItems { get { return sourceConfigItems; } set { sourceConfigItems = value; } }

        private BigInfos bigInfos = null;
        [JsonProperty(nameof(BigInfos))]
        public BigInfos BigInfos { get { return bigInfos; } }
        private Dictionary<string, PictureItemStructure> pictureItemStructureDictionary = null;
        [JsonProperty(nameof(PictureItemStructureDictionary))]
        public Dictionary<string, PictureItemStructure> PictureItemStructureDictionary { get { return pictureItemStructureDictionary; } }

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

            Make();
        }
        private void Make()
        {
            bool _AutoComplete_LocationPictureBindings_flag = false;
            bool _AutoComplete_LocationByLocationPictureBindings_flag = false;
            List<PictureItemStructure> pictureItemStructures = new List<PictureItemStructure>();
            List<LocationPictureBinding> locationPictureBindings = new List<LocationPictureBinding>();
            List<LevelLocation> levelLocations = new List<LevelLocation>();
            List<SchoolInfo> schoolInfos = new List<SchoolInfo>();
            List<StudentInfo> studentInfos = new List<StudentInfo>();

            foreach(SourceConfigItem sourceConfigItem in SourceConfigItems)
            {
#warning 规范化特殊字段处理器
                if (sourceConfigItem.SourceType == KnownSourceType._AutoComplete_LocationPictureBindings)
                {
                    _AutoComplete_LocationPictureBindings_flag = true;
                    continue;
                }
                else if(sourceConfigItem.SourceType == KnownSourceType._AutoComplete_LocationByLocationPictureBindings)
                {
                    _AutoComplete_LocationByLocationPictureBindings_flag = true;
                    continue;
                }

                ISourceTypeResolver sourceTypeResolver = SourceTypeResolverManager.SourceTypeResolvers[sourceConfigItem.SourceType];

#if _SYS_PATH_URI
                string absolutePath = GetAbsolutePath(sourceConfigItem.Path);
#elif _LIB_PATH_URI
                object sourceTypeResolverReturn = sourceTypeResolver.Resolve(absolutePath);

                if (sourceTypeResolver.ReturnType == typeof(PictureItemStructure[]))
                {
                    pictureItemStructures.AddRange((PictureItemStructure[])sourceTypeResolverReturn);
                }
                else if (sourceTypeResolver.ReturnType == typeof(LocationPictureBinding[]))
                {
                    locationPictureBindings.AddRange((LocationPictureBinding[])sourceTypeResolverReturn);
                }
                else if(sourceTypeResolver.ReturnType == typeof(LevelLocation[]))
                {
                    levelLocations.AddRange((LevelLocation[])sourceTypeResolverReturn);
                }
                else if (sourceTypeResolver.ReturnType == typeof(SchoolInfo[]))
                {
                    schoolInfos.AddRange((SchoolInfo[])sourceTypeResolverReturn);
                }
                else if (sourceTypeResolver.ReturnType == typeof(StudentInfo[]))
                {
                    studentInfos.AddRange((StudentInfo[])sourceTypeResolverReturn);
                }
                else
                {
                    throw new ArgumentException("Non-support type.", "ReturnType");
                }
            }

            PictureItemStructure[] pictureItemStructuresArray = pictureItemStructures.ToArray();
            if (_AutoComplete_LocationPictureBindings_flag)
            {
                locationPictureBindings.AddRange(AutoComplete_LocationPictureBinding(pictureItemStructuresArray));
            }
            if (_AutoComplete_LocationByLocationPictureBindings_flag)
            {
                var locationsFromLPBs = from val in locationPictureBindings
                                        select val.Location;
                levelLocations.AddRange(locationsFromLPBs);
            }

#warning 这里还可以加上别的处理器（如筛选器（因此，Make要带参数，每个SourceInto可以有一串属性值））

            this.pictureItemStructureDictionary = PictureItemStructure.ToDictionaryByName(pictureItemStructuresArray);
            this.bigInfos = BigInfos.MakeMinimumSet(
                locationPictureBindings.ToArray(),
                levelLocations.ToArray(),
                schoolInfos.ToArray(),
                studentInfos.ToArray());
        }
        private string GetAbsolutePath(string relativePath)
        {
            return new Uri(new Uri(System.IO.Path.GetFullPath(this.Path).Replace('\\', '/'), UriKind.Absolute), new Uri(relativePath, UriKind.Relative)).LocalPath;
        }
        public static IEnumerable<LocationPictureBinding> AutoComplete_LocationPictureBinding(PictureItemStructure[] pictureItems)
        {
            foreach(PictureItemStructure pictureItem in pictureItems)
            {
                yield return new LocationPictureBinding()
                {
                    Location = LevelLocation.Parse(pictureItem.Name),
                    PictureName = pictureItem.Name
                };
            }
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
