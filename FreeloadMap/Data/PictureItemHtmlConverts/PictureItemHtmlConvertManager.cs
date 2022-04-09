using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeloadMap.Data.PictureItemHtmlConverts
{
    public static class PictureItemHtmlConvertManager
    {
        private static Dictionary<string, IPictureItemHtmlConvert> pictureItemHtmlResolvers = new Dictionary<string, IPictureItemHtmlConvert>()
        {
            {, }
        };
        public static Dictionary<string, IPictureItemHtmlConvert> PictureItemHtmlResolvers { get { return pictureItemHtmlResolvers; } }

        public static void Add(IPictureItemHtmlConvert pictureItemHtmlResolver)
        {
            pictureItemHtmlResolvers.Add(pictureItemHtmlResolver.Name, pictureItemHtmlResolver);
        }

        public static void Remove(string name)
        {
            pictureItemHtmlResolvers.Remove(name);
        }
    }
}
