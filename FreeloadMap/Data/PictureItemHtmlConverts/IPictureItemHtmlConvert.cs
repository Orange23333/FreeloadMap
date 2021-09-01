using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeloadMap.Data.PictureItemHtmlConverts
{
    public interface IPictureItemHtmlConvert
    {
        string Name { get; }

        Type InputType { get; }

        string GetHtml(object obj);
    }
}
