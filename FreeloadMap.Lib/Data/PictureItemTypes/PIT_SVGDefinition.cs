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
    public class PIT_SVGDefinition : IPictureItemData
    {
        #region ==IPictureItemData==
        public string TypeName { get { return KnownPictureItemTypeNames.SVGDefinition; } }

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
            PIT_SVGDefinition svgDefinition = JsonConvert.DeserializeObject<PIT_SVGDefinition>(data);

            this.SVGGroupName = svgDefinition.SVGGroupName;
            this.SVGDefinitionHtmlText = svgDefinition.SVGDefinitionHtmlText;
        }

        public Dictionary<string, string> ExValues { get; set; }
        #endregion

        [JsonProperty(nameof(SVGGroupName))]
        public string SVGGroupName { get; set; }

        [JsonProperty(nameof(SVGDefinitionHtmlText))]
        public string SVGDefinitionHtmlText { get; set; }

#warning 没有优化异步
        public string SVGDefinitionContent
        {
            get
            {
                var document = BrowsingContext.New().OpenAsync(m => m.Content(SVGDefinitionHtmlText)).Result;
                var g = document.QuerySelector("svg > g");
                var targets = g.QuerySelectorAll("g > *");
                var targetTexts = from val in targets select val.OuterHtml;

                return StringEx.CatStrings(targetTexts);
            }
            set
            {
                var document = BrowsingContext.New().OpenAsync(m => m.Content(SVGDefinitionHtmlText)).Result;
                var g = document.QuerySelector("svg > g");
                var targets = g.QuerySelectorAll("g > *");
                foreach (var target in targets)
                {
                    target.Remove();
                }
                g.InnerHtml = value;
                SVGDefinitionHtmlText = g.QuerySelector("svg").OuterHtml;
            }
        }

    }
}
