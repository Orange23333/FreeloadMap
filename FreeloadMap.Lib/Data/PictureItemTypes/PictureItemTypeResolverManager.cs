using System;
using System.Collections.Generic;
using System.Text;

namespace FreeloadMap.Lib.Data.PictureItemTypes
{
    public static class PictureItemTypeResolverManager
    {
        private static Dictionary<string, IPictureItemTypeResolver> pictureItemResolvers = new Dictionary<string, IPictureItemTypeResolver>();
        public static Dictionary<string, IPictureItemTypeResolver> PictureItemResolvers { get { return pictureItemResolvers; } }

        public static void Add(IPictureItemTypeResolver pictureItemResolver)
        {
            pictureItemResolvers.Add(pictureItemResolver.Name, pictureItemResolver);
        }

        public static void Remove(string name)
        {
            pictureItemResolvers.Remove(name);
        }
    }
}
