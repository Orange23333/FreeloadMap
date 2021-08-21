using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;

namespace FreeloadMap.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ProgramConfigItem
    {
        [JsonProperty(nameof(Key))]
        [Index(0)]
        [Name("key")]
        public string Key { get; set; }

        [JsonProperty(nameof(Value))]
        [Index(1)]
        [Name("value")]
        public string Value { get; set; }

        public static ProgramConfigItem[] ToArray(Dictionary<string,string> dictionary)
        {
            int i;
            ProgramConfigItem[] r;

            lock (dictionary)
            {
                r = new ProgramConfigItem[dictionary.Count];

                for (i = 0; i < dictionary.Count; i++)
                {
                    var kvp = dictionary.ElementAt(i);

                    r[i] = new ProgramConfigItem()
                    {
                        Key = kvp.Key,
                        Value = kvp.Value
                    };
                }
            }

            return r;
        }

        public static Dictionary<string,string> ToDictionary(IEnumerable<ProgramConfigItem> programConfigItems)
        {
            Dictionary<string, string> r = new Dictionary<string, string>();

            lock (programConfigItems)
            {
                foreach (ProgramConfigItem programConfigItem in programConfigItems)
                {
                    r.Add(programConfigItem.Key, programConfigItem.Value);
                }
            }

            return r;
        }
    }
}
