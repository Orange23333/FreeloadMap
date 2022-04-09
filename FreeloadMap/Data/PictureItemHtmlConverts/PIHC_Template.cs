using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AngleSharp;

using FreeloadMap.Lib.Data.PictureItemTypes;

namespace FreeloadMap.Data.PictureItemHtmlConverts
{
#warning HERE
    public class PIHC_Template : IPictureItemHtmlConvert
    {
        #region ==IPictureItemHtmlConvert==
#warning HERE
        public string Name { get { return "Template"; } }

#warning HERE
        public Type InputType { get { return typeof(PIT_Template); } }

        public string GetStyle(object obj)
        {
#warning HERE
            return GetStyle((PIT_Template)obj);
        }

        public void Initialize(object obj, string style)
        {
#warning HERE
            this.pit_template = (PIT_Template)obj;
            this.style = style;

#warning HERE
            // ...
        }

        public void Work(IEnumerable<IPictureItemData> loadedData)
        {
#warning HERE
            // ...
        }

        public string GetHtml()
        {
#warning HERE
            return GetHtml(pit_template);
        }

#warning HERE
        private string GetHtml(PIT_Template template)
        {
#warning HERE
            string style = this.style != null ? this.style : GetStyle(template);

#warning HERE
            // ...

            return r;
        }

#warning HERE
        private string GetStyle(PIT_Template template)
        {
#warning HERE
            // ...

            return r;
        }
        #endregion

#warning HERE
        private PIT_Template pit_template;
        private string style;
    }
}
