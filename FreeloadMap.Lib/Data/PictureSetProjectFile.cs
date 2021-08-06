using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace FreeloadMap.Lib.Data
{
    public class PictureSetProjectFile
    {
        private string path = null;
        public string Path { get { return path; } set { path = value; } }
        public string Directory
        {
            get
            {
                return System.IO.Path.GetDirectoryName(this.Path).Replace('\\', '/'); //如果c是目录，位置是a/b/c，返回的是a/b
            }
        }

        public static readonly Version SupportiveNewestProjectVersion = new Version(1, 0);
        public static readonly Version SupportiveOldestProjectVersion = new Version(1, 0);
        private Version thisProjectVersion = SupportiveNewestProjectVersion;
        [JsonProperty(nameof(ProjectVersion))]
        public Version ProjectVersion { get { return thisProjectVersion; } set { thisProjectVersion = value; } }

        private List<PictureItemStructure> pictureItems = new List<PictureItemStructure>();
        [JsonProperty(nameof(PictureItems))]
        public List<PictureItemStructure> PictureItems { get { return pictureItems; } set { pictureItems = value; } }

        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented
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
            PictureSetProjectFile pictureSetProjectFile = (PictureSetProjectFile)JsonConvert.DeserializeObject(System.IO.File.ReadAllText(path), jsonSerializerSettings);
            this.ProjectVersion = pictureSetProjectFile.ProjectVersion;
            this.PictureItems = pictureSetProjectFile.PictureItems;
            this.path = path;
            //ResetHasChanged();
        }

        /// <remarks>
        /// 不检查文件存在。
        /// </remarks>
        public void Save(string path)
        {
            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(this, jsonSerializerSettings));
            this.path = path;
            //ResetHasChanged();
        }
    }
}
