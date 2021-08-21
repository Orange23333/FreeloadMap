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
            SchoolInfosFile schoolInfosFile = new SchoolInfosFile();
            schoolInfosFile.Load(path);

            return schoolInfosFile.CompleteIconSrc();
        }
    }
}
