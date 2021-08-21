using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeloadMap
{
    public static class DefineValues
    {
        public static readonly string WwwrootEmptyFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, "wwwroot/.empty").Replace('\\', '/');
    }
}
