using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AngleSharp;
using Newtonsoft.Json;

using FreeloadMap.Lib.Utility;

namespace FreeloadMap.Lib.Data.PictureItemTypes
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PIT_SVGGroup : IPictureItemData
    {
        #region ==IPictureItemData==
        public string TypeName { get { return KnownPictureItemTypeNames.SVGGroup; } }

        public string Name { get; set; }

        public string Path { get; set; }

        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented
        };

        public string SerializeData()
        {
            string json = JsonConvert.SerializeObject(this, jsonSerializerSettings);
            return json;
        }

        public void DeserializeData(string data)
        {
            PIT_SVGGroup svgGroup = JsonConvert.DeserializeObject<PIT_SVGGroup>(data);

            this.SVGGroupName = svgGroup.SVGGroupName;
            this.SVGGroupHtmlText = svgGroup.SVGGroupHtmlText;
        }

        public Dictionary<string, string> ExValues { get; set; }
        #endregion

        [JsonProperty(nameof(SVGGroupName))]
        public string SVGGroupName { get; set; }

        [JsonProperty(nameof(SVGGroupHtmlText))]
        public string SVGGroupHtmlText { get; set; }

        //public List<ISVGGroupItem> SVGItems = new List<ISVGGroupItem>();

#warning 没有优化异步
        public static string AddSVGGroupItem(string svgGroup,string groupItems)
        {
            var document = BrowsingContext.New().OpenAsync(m => m.Content(svgGroup)).Result;
            var g = document.QuerySelector("svg > g");

            var items = document.CreateElement(groupItems);
            g.AppendChild(items);

            return document.DocumentElement.OuterHtml;
        }
        public void AddSVGGroupItem(string groupItems)
        {
            this.SVGGroupHtmlText = AddSVGGroupItem(SVGGroupHtmlText, groupItems);
        }
        public static void RemoveSVGGroupItem(string svgGroup, string groupItemsSelector, bool selectorAll)
        {
            var document = BrowsingContext.New().OpenAsync(m => m.Content(svgGroup)).Result;
            var g = document.QuerySelector("svg > g");

            if (selectorAll)
            {
                var items = document.QuerySelectorAll(groupItemsSelector);
                foreach(var item in items)
                {
                    item.Remove();
                }
            }
            else
            {
                var item = document.QuerySelector(groupItemsSelector);
                item.Remove();
            }
        }
        public void RemoveSVGGroupItem(string groupItemsSelector, bool selectorAll)
        {
            RemoveSVGGroupItem(SVGGroupHtmlText, groupItemsSelector, selectorAll);
        }
    }
}
