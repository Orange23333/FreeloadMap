using System;
using System.Collections.Generic;
using System.Text;

namespace FreeloadMap.Lib.Data.PictureItemTypes
{
    public interface IPictureItemTypeResolver
    {
        string Name { get; }

        Type ObjectType { get; }

        object Convert(PictureItemStructure pictureItemStructure);

        PictureItemStructure Convert(object obj);
    }
}
