using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using FreeloadMap.Lib.Data;

namespace PictureSet
{
    public partial class MainWindow : IPictureItemContainer
    {
        public Canvas Container_Canvas { get { return PictureCanvas; } }
        public PictureItemControlTools Container_PictureItemControlTools { get { throw new NotSupportedException(); } }

        public PictureItem FindItem(Guid id)
        {
            var sameItem = from val in pictureItems
                           where val.ID == id
                           select val;
            return sameItem.Count() == 0 ? null : sameItem.First();
        }
        public PictureItem FindItem(PictureItemControl pictureItemControl)
        {
            var sameItem = from val in pictureItems
                           where Object.ReferenceEquals(val.Control, pictureItemControl)
                           select val;
            return sameItem.Count() == 0 ? null : sameItem.First();
        }
        public PictureItem FindItem(PictureItemStructure pictureItemStructure)
        {
            var sameItem = from val in pictureItems
                           where PictureItemStructure.Equals(val.Data, pictureItemStructure)
                           select val;
            return sameItem.Count() == 0 ? null : sameItem.First();
        }

        /// <remarks>
        /// 将会自动设置ID。
        /// </remarks>
        public PictureItem AddItem(PictureItemStructure pictureItemStructure)
        {
            PictureItem pictureItem = new PictureItem(this, pictureItemStructure);
            _AddItem(pictureItem);

            return pictureItem;
        }
        public void AddItem(PictureItem pictureItem)
        {
            _AddItem(pictureItem);
        }
        private void _AddItem(PictureItem pictureItem)
        {
            Guid guid = GetFreeGuid();

            pictureItem.ID = guid;
            pictureItems.Add(pictureItem);
            pictureItem.Control.HorizontalAlignment = HorizontalAlignment.Left;
            pictureItem.Control.VerticalAlignment = VerticalAlignment.Top;
            PictureCanvas.Children.Add(pictureItem.Control);
        }
        private Guid GetFreeGuid()
        {
            Guid r;
            IEnumerable<Guid> guids;

            do
            {
                r = Guid.NewGuid();
                guids = from val in pictureItems
                        select val.ID;
            } while (guids.Contains(r));

            return r;
        }

        public void RemoveItem(PictureItem pictureItem)
        {
            if (!pictureItems.Contains(pictureItem))
            {
                throw new ArgumentException("Not found.", "pictureItem");
            }

            PictureCanvas.Children.Remove(pictureItem.Control);
            pictureItems.Remove(pictureItem);
        }
    }
}
