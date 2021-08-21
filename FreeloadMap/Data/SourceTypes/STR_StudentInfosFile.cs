using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FreeloadMap.Lib.Data;

namespace FreeloadMap.Data.SourceTypes
{
    public class STR_StudentInfosFile : ISourceTypeResolver
    {
        public string Name { get { return KnownSourceType.StudentInfosFile; } }

        public Type ReturnType { get { return typeof(StudentInfo[]); } }

        public object Resolve(string path)
        {
            StudentInfosFile studentInfosFile = new StudentInfosFile();
            studentInfosFile.Load(path);

            return studentInfosFile.StudentInfos;
        }
    }
}
