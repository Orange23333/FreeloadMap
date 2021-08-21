using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FreeloadMap.Lib.Data;

namespace FreeloadMap.Data.SourceTypes
{
    public class STR_PictureSetProjectFile : ISourceTypeResolver
    {
        public string Name { get { return KnownSourceType.PictureSetProjectFile; } }

        public Type ReturnType { get { return typeof(PictureItemStructure[]); } }

        public object Resolve(string path)
        {
            int i;

            PictureSetProjectFile pictureSetProjectFile = new PictureSetProjectFile();
            pictureSetProjectFile.Load(path);

            PictureItemStructure[] pictureItemStructures = pictureSetProjectFile.PictureItems.ToArray();
            for (i = 0; i < pictureItemStructures.Length; i++)
            {
                PictureItemStructure temp = pictureItemStructures[i];
                string absolutePath = PictureItemStructure.GetAbsolutePath(pictureSetProjectFile.Path, temp.Path);
                temp.Path = PictureItemStructure.GetRelativePath(DefineValues.WwwrootEmptyFilePath, absolutePath);
                pictureItemStructures[i] = temp;
            }

            return pictureItemStructures;
        }
    }
}
