using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FreeloadMap.Data.SourceTypes;

namespace FreeloadMap.Data
{
    public static class SourceTypeResolverManager
    {
        private static Dictionary<string, ISourceTypeResolver> sourceTypeResolvers = new Dictionary<string, ISourceTypeResolver>()
        {
            { KnownSourceType.PictureSetProjectFile, new STR_PictureSetProjectFile() },
            { KnownSourceType.LocationPictureBindingsFile, new STR_LocationPictureBindingsFile() },
            { KnownSourceType.SchoolInfosFile, new STR_SchoolInfosFile() },
            { KnownSourceType.StudentInfosFile, new STR_StudentInfosFile() }
        };
        public static Dictionary<string, ISourceTypeResolver> SourceTypeResolvers { get { return sourceTypeResolvers; } }

        public static void Add(ISourceTypeResolver sourceTypeResolver)
        {
            sourceTypeResolvers.Add(sourceTypeResolver.Name, sourceTypeResolver);
        }

        public static void Remove(string name)
        {
            sourceTypeResolvers.Remove(name);
        }
    }
}
