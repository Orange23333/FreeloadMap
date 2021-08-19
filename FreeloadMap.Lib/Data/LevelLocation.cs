using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CsvHelper.TypeConversion;
using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using CsvHelper;
using CsvHelper.Configuration;

namespace FreeloadMap.Lib.Data
{
    // 只考虑地球
    [JsonObject(MemberSerialization.OptIn)]
    public class LevelLocation : IComparable<LevelLocation>
    {
        // 未写
        public static readonly string Empty = ""; // String.Empty;
        // 缺省
        public static readonly string Default = "_";

        public static readonly LevelLocation EmptyLocation = new LevelLocation()
        {
            Country = LevelLocation.Empty,
            Province = LevelLocation.Empty,
            City = LevelLocation.Empty,
            District = LevelLocation.Empty,
            DetailAddress = LevelLocation.Empty
        };

        public static readonly LevelLocation AnyLocation = new LevelLocation()
        {
            Country = LevelLocation.Default,
            Province = LevelLocation.Default,
            City = LevelLocation.Default,
            District = LevelLocation.Default,
            DetailAddress = LevelLocation.Default
        };

        [JsonProperty(nameof(Country))]
        public string Country { get; set; }

        [JsonProperty(nameof(Province))]
        public string Province { get; set; }

        [JsonProperty(nameof(City))]
        public string City { get; set; }

        [JsonProperty(nameof(District))]
        public string District { get; set; }

        //Without Country, Province, City and District.
        [JsonProperty(nameof(DetailAddress))]
        public string DetailAddress { get; set; }

        public static implicit operator LevelLocation(string value)
        {
            return Parse(value);
        }
        /// <param name="noneMeaningsDefault">
        /// 如"a.b.c"中只有三个部分，缺少区域和详细地址，如果设置noneMeaningsDefault，则方法将会默认把缺少的值视为缺省而不是空值。
        /// </param>
        /// <remarks>
        /// 当defaultValue为null时不存在未写，只有缺省。
        /// </remarks>
        //public static LevelLocation Parse(string value/*, bool noneMeaningsDefault = true*/)
        public static LevelLocation Parse(string value)
        {
            int i;
            //string defaultValue = noneMeaningsDefault ? LevelLocation.Default : LevelLocation.Empty;
            string[] parts = value.Split('.', 5);
            string[] values = new string[5];

            for (i = 0; i < parts.Length; i++)
            {
                values[i] = parts[i];
            }
            for (; i < values.Length; i++)
            {
                values[i] = LevelLocation.Default;
            }

            return new LevelLocation()
            {
                Country = values[0],
                Province = values[1],
                City = values[2],
                District = values[3],
                DetailAddress = values[4]
            };
        }

        public class TC_LevelLocation : TypeConverter
        {
            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                return LevelLocation.Parse(text);
            }

            public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
            {
                return ((LevelLocation)value).ToString();
            }
        }

        public override string ToString()
        {
            int i;
            StringBuilder r = new StringBuilder();

            string[] values = new string[]
            {
                this.Country == null ? LevelLocation.Empty : this.Country,
                this.Province == null ? LevelLocation.Empty : this.Province,
                this.City == null ? LevelLocation.Empty : this.City,
                this.District == null ? LevelLocation.Empty : this.District,
                this.DetailAddress == null ? LevelLocation.Empty : this.DetailAddress
            };

            for (i = 0; i < values.Length - 1; i++){
                r.Append(values[i]);
                r.Append(".");
            }
            r.Append(values[i]);
            return r.ToString();
        }

        public string ToShortString()
        {
            int i, consecutiveDefaultIndexStartFrom;
            StringBuilder r = new StringBuilder();
            string[] values = new string[]
            {
                this.Country,
                this.Province,
                this.City,
                this.District,
                this.DetailAddress
            };

            // 查找尾部连续空字段。
            for (consecutiveDefaultIndexStartFrom = values.Length; consecutiveDefaultIndexStartFrom > 0; consecutiveDefaultIndexStartFrom--)
            {
                if (values[consecutiveDefaultIndexStartFrom - 1] != LevelLocation.Default)
                {
                    break;
                }
            }

            if (consecutiveDefaultIndexStartFrom == 0)
            {
                return "";
            }
            for (i = 0; i < consecutiveDefaultIndexStartFrom - 1; i++)
            {
                r.Append(values[i]);
                r.Append(".");
            }
            r.Append(values[i]);
            return r.ToString();
        }

        public bool IsEqualsOrInclude(LevelLocation levelLocation)
        {
            if (this.Country == LevelLocation.Default)
            {
                return true;
            }
            else if(this.Country == levelLocation.Country)
            {
                if (this.Province == LevelLocation.Default)
                {
                    return true;
                }
                else if (this.Province == levelLocation.Country)
                {
                    if (this.City == LevelLocation.Default)
                    {
                        return true;
                    }
                    else if (this.City == levelLocation.Country)
                    {
                        if (this.District == LevelLocation.Default)
                        {
                            return true;
                        }
                        else if (this.District == levelLocation.Country)
                        {
                            if (this.DetailAddress == LevelLocation.Default)
                            {
                                return true;
                            }
                            else if (this.DetailAddress == levelLocation.Country)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public int CompareTo(LevelLocation other)
        {
            int i ,r;
            string[] valuesA = new string[]
            {
                this.Country,
                this.Province,
                this.City,
                this.District,
                this.DetailAddress
            };
            string[] valuesB = new string[]
            {
                other.Country,
                other.Province,
                other.City,
                other.District,
                other.DetailAddress
            };

            for (i = 0; i < 5; i++)
            {
                //// 没有必要的，但还是写了：防止Compare特殊比较
                //if (valuesA[i].Equals(valuesB[i]))
                //{
                //    continue;
                //}

                if(valuesA[i].Equals(LevelLocation.Empty)&& valuesB[i].Equals(LevelLocation.Empty))
                {
                    continue;
                }

                r = CompareWord(valuesA[i], valuesB[i]);
                if (r != 0)
                {
                    return r;
                }
            }

            return 0;
        }
        /// <summary>
        /// 比较两个LevelLocation地址字段。
        /// </summary>
        public static int CompareWord(string word0, string word1)
        {
            // 不自己检测Null

            if (word0.Equals(LevelLocation.Default))
            {
                if (word1.Equals(LevelLocation.Default))
                {
                    return 0;
                }
                else
                {
                    return int.MaxValue;
                }
            }
            else if(word1.Equals(LevelLocation.Default))
            {
                return int.MinValue;
            }

            return word0.CompareTo(word1);
        }

        /// <remarks>
        /// 最低界限时相等，最接近即相等。不是模糊搜索。
        /// </remarks>
        public static LevelLocation FindClosetLocation(LevelLocation[] source, LevelLocation obj)
        {
            // // source为null不返回null，而是.NET库中自生的异常。
            if (source == null || source.Length == 0)
            {
                return null;
            }

            var _src = FindEquealsOrParentLocation(source, obj);
            if (_src.Count() == 0)
            {
                return null;
            }
            LevelLocation r = _src.First();

            foreach(var one in _src)
            {
                // 因为先经过FindEquealsOrParentLocation()的检查，所以可以不用CompareTo()而是别的简化代码来优化。
                if (r.CompareTo(one) > 0)
                {
                    r = one;
                }
            }

            return r;
        }
        public static IEnumerable<LevelLocation> FindEquealsOrParentLocation(LevelLocation[] source, LevelLocation obj)
        {
            foreach(LevelLocation _src in source)
            {
                if (_src.IsEqualsOrInclude(obj))
                {
                    yield return _src; 
                }
            }
        }
    }
}
