using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using FreeloadMap.Lib.Data;

namespace PictureSet
{
    public interface IPictureItemContainer
    {
        Canvas Container_Canvas { get; }
        PictureItemControlTools Container_PictureItemControlTools { get; }

        PictureItem FindItem(Guid id);
        PictureItem FindItem(PictureItemControl pictureItemControl);
        PictureItem FindItem(PictureItemStructure pictureItemStructure);

        void AddItem(PictureItem pictureItem);

        void RemoveItem(PictureItem pictureItem);
    }
}
