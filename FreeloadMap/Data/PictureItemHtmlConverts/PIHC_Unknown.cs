using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FreeloadMap.Lib.Data.PictureItemTypes;

namespace FreeloadMap.Data.PictureItemHtmlConverts
{
    public class PIHC_Unknown : IPictureItemHtmlConvert
    {
        public string Name { get { return "Unknown"; } }

        public Type InputType { get { return typeof(PIT_Unknown); } }

        public string GetStyle(object obj)
        {
            return String.Empty;
        }

        public void Initialize(object obj, string style) {; }

        public void Work(IEnumerable<IPictureItemData> loadedData) {; }

        public string GetHtml()
        {
            return String.Empty;
        }
    }
}
