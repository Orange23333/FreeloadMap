using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeloadMap.Data.PictureItemHtmlConverts
{
    public static class PictureItemHtmlConvertManager
    {
        private static Dictionary<string, IPictureItemHtmlConverts> pictureItemHtmlResolvers = new Dictionary<string, IPictureItemHtmlConverts>();
        public static Dictionary<string, IPictureItemHtmlConverts> PictureItemHtmlResolvers { get { return pictureItemHtmlResolvers; } }

        public static void Add(IPictureItemHtmlConverts pictureItemHtmlResolver)
        {
            pictureItemHtmlResolvers.Add(pictureItemHtmlResolver.Name, pictureItemHtmlResolver);
        }

        public static void Remove(string name)
        {
            pictureItemHtmlResolvers.Remove(name);
        }
    }
}
