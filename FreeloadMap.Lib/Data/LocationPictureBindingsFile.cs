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
    public class LocationPictureBindingsFile
    {
        private string path = null;
        public string Path { get { return path; } set { path = value; } }

        private List<LocationPictureBinding> locationPictureBindings = new List<LocationPictureBinding>();
        [JsonProperty(nameof(LocationPictureBindings))]
        public List<LocationPictureBinding> LocationPictureBindings { get { return locationPictureBindings; } set { locationPictureBindings = value; } }

        private static readonly CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            AllowComments = true,
            Encoding = Encoding.UTF8,
            PrepareHeaderForMatch = args => args.Header.ToLower()
        };

        public LocationPictureBinding[] CompletePicturePath()
        {
            int i;
            LocationPictureBinding[] newLocationPictureBindings = new LocationPictureBinding[locationPictureBindings.Count];
            lock (newLocationPictureBindings)
            {
                for (i = 0; i < locationPictureBindings.Count; i++)
                {
                    newLocationPictureBindings[i] = locationPictureBindings[i].GetCopy();
                }
            }

            //为了快速解锁schoolInfos，这里分开拷贝和补全部分。

            for (i = 0; i < newLocationPictureBindings.Length; i++)
            {
                if (String.IsNullOrEmpty(newLocationPictureBindings[i].PictureName))
                {
                    newLocationPictureBindings[i].PictureName = newLocationPictureBindings[i].Location.ToShortString();
                }
            }

            return newLocationPictureBindings;
        }

        public void Load(string path)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, csvConfiguration))
            {
                this.LocationPictureBindings = csv.GetRecords<LocationPictureBinding>().ToList();
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
                csv.WriteRecords(LocationPictureBindings);
            }

            this.path = path;
        }
    }
}
