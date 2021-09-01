using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

using FreeloadMap.Lib.Utility;

namespace FreeloadMap.Lib.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PictureSetProjectFile
    {
        private string path = null;
        public string Path { get { return path; } set { path = value; } }

        public static readonly Version SupportiveNewestProjectVersion = new Version(2, 0);
        public static readonly Version SupportiveOldestProjectVersion = new Version(2, 0);
        private Version thisProjectVersion = SupportiveNewestProjectVersion;
        [JsonProperty(nameof(ProjectVersion))]
        public Version ProjectVersion { get { return thisProjectVersion; } set { thisProjectVersion = value; } }

        private List<IPictureItemData> pictureItemData = new List<IPictureItemData>();
        [JsonProperty(nameof(PictureItemData))]
        public List<IPictureItemData> PictureItemData { get { return pictureItemData; } set { pictureItemData = value; } }

        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            Converters =new List<JsonConverter>() {
                new JSONConverter_Version()
            }
        };

        //// 只检查是否操作，不与原文件对比是否有变动。
        //private bool hasChanged = false;
        //public bool HasChanged { get { return hasChanged; } }
        //public event EventHandler ProjectChanged;
        //public event EventHandler ProjectChangeOnce;
        //public void MakeChange()
        //{
        //    if (!hasChanged)
        //    {
        //        hasChanged = true;
        //        ProjectChanged.Invoke(this,EventArgs.Empty);
        //    }
        //    ProjectChangeOnce.Invoke(this, EventArgs.Empty);
        //}
        //public void ResetHasChanged()
        //{
        //    hasChanged = false;
        //}

        public void Load(string path)
        {
            this.path = path;

            PictureSetProjectFile pictureSetProjectFile = (PictureSetProjectFile)JsonConvert.DeserializeObject<PictureSetProjectFile>(System.IO.File.ReadAllText(path), jsonSerializerSettings);
            this.ProjectVersion = pictureSetProjectFile.ProjectVersion;
            this.PictureItemData = pictureSetProjectFile.PictureItemData;
            //ResetHasChanged();
        }

        /// <remarks>
        /// 不检查文件存在。
        /// </remarks>
        public void Save(string path)
        {
            this.path = path;

            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(this, jsonSerializerSettings));
            //ResetHasChanged();
        }
    }
}
