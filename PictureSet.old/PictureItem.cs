using System;
using System.Collections.Generic;
using System.Text;

using FreeloadMap.Lib.Data;

namespace PictureSet
{
    public class PictureItem
    {
        public Guid ID { get; set; }

        public PictureItemControl Control { get; set; }
        public PictureItemStructure Data { get; set; }

        public PictureItem(PictureItemStructure data)
        {
            ;
        }

        public class PictureItemComparer : IComparer<PictureItem>
        {
            public int Compare(PictureItem x, PictureItem y)
            {
                return x.ID.CompareTo(y.ID);
            }
        }
    }
}
