using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;

namespace FreeloadMap.Lib.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SchoolInfosFile
    {
        private string path = null;
        public string Path { get { return path; } set { path = value; } }

        private List<SchoolInfo> schoolInfos = new List<SchoolInfo>();
        [JsonProperty(nameof(SchoolInfos))]
        public List<SchoolInfo> SchoolInfos { get { return schoolInfos; } set { schoolInfos = value; } }

        private static readonly CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            AllowComments = true,
            Encoding = Encoding.UTF8,
            PrepareHeaderForMatch = args => args.Header.ToLower()
        };

        public SchoolInfo[] CompleteIconSrc()
        {
            int i;
            SchoolInfo[] newSchoolInfos = new SchoolInfo[schoolInfos.Count];
            for (i = 0; i < schoolInfos.Count; i++)
            {
                newSchoolInfos[i] = schoolInfos[i].GetCopy();
            }

            //为了快速解锁schoolInfos，这里分开拷贝和补全部分。
            DirectoryInfo directoryInfo = new DirectoryInfo(System.IO.Path.GetDirectoryName(this.Path).Replace('\\', '/'));
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            for (i = 0; i < schoolInfos.Count; i++)
            {
                if (String.IsNullOrEmpty(newSchoolInfos[i].IconPath))
                {
                    var sameNameFiles = from val in fileInfos
                                       where Regex.IsMatch(val.Name, String.Format("^{0}\\..*$", newSchoolInfos[i].Name))
                                       select val;
                    if(sameNameFiles.Count() > 0)
                    {
                        var maySupportFiles = from val in sameNameFiles
                                              where Regex.IsMatch(
                                                  System.IO.Path.GetExtension(val.Name),
                                                  String.Format("^\\.(bmp|dib|gif|jpg|jpeg|jpe|jfif|png|tif|tiff|heic|webp)$")
                                                  )
                                              select val;
                        if(maySupportFiles.Count() > 0)
                        {
                            var greatFormatFiles = from val in maySupportFiles
                                                   where Regex.IsMatch(
                                                    System.IO.Path.GetExtension(val.Name),
                                                    String.Format("^\\.(bmp|dib|gif|jpg|jpeg|jpe|jfif|png|tif|tiff)$")
                                                    )
                                                   select val;
                            if(greatFormatFiles.Count() > 0)
                            {
                                newSchoolInfos[i].IconPath = greatFormatFiles.First().Name;
                            }
                            else
                            {
                                newSchoolInfos[i].IconPath = maySupportFiles.First().Name;
                            }
                        }
                        else
                        {
                            newSchoolInfos[i].IconPath = sameNameFiles.First().Name;
                        }
                    }
                }
            }

            return newSchoolInfos;
        }

        public void Load(string path)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, csvConfiguration))
            {
                this.SchoolInfos = csv.GetRecords<SchoolInfo>().ToList();
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
                csv.WriteRecords(SchoolInfos);
            }

            this.path = path;
        }
    }
}
