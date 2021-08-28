using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FreeloadMap.Lib.Utility;

namespace FreeloadMap
{
    public static class DefineValues
    {
#if _SYS_PATH_URI
        public static readonly string WwwrootEmptyFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, "wwwroot/.empty").Replace('\\', '/');
#elif _LIB_PATH_URI
        public static readonly string WwwrootEmptyFilePath = FkPath.Combine(Environment.CurrentDirectory, "wwwroot/.empty", FkPath.DirectorySeparator.Backslash);
#endif
    }
}
