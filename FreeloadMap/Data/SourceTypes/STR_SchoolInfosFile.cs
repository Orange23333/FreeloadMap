using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FreeloadMap.Lib.Data;

namespace FreeloadMap.Data.SourceTypes
{
    public class STR_SchoolInfosFile : ISourceTypeResolver
    {
        public string Name { get { return KnownSourceType.SchoolInfosFile; } }

        public Type ReturnType { get { return typeof(SchoolInfo[]); } }

        public object Resolve(string path)
        {
            int i;

            SchoolInfosFile schoolInfosFile = new SchoolInfosFile();
            schoolInfosFile.Load(path);
            SchoolInfo[] r = schoolInfosFile.CompleteIconSrc();
            for (i = 0; i < r.Length; i++)
            {
                string temp;
                temp = PictureItemStructure.GetAbsolutePath(schoolInfosFile.Path, r[i].IconPath);
                r[i].IconPath = PictureItemStructure.GetRelativePath(DefineValues.WwwrootEmptyFilePath, temp);
            }

            return r;
        }
    }
}
