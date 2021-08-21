using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FreeloadMap.Lib.Data;

namespace FreeloadMap.Data.SourceTypes
{
    public class STR_LocationPictureBindingsFile : ISourceTypeResolver
    {
        public string Name { get { return KnownSourceType.LocationPictureBindingsFile; } }

        public Type ReturnType { get { return typeof(LocationPictureBinding[]); } }

        public object Resolve(string path)
        {
            LocationPictureBindingsFile locationPictureBindingsFile = new LocationPictureBindingsFile();
            locationPictureBindingsFile.Load(path);

            return locationPictureBindingsFile.CompletePicturePath();
        }
    }
}
