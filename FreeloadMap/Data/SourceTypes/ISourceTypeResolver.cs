using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeloadMap.Data.SourceTypes
{
    public interface ISourceTypeResolver
    {
        string Name { get; }

        Type ReturnType { get; }

        object Resolve(string path);
    }
}
