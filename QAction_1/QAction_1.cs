using System;
using Newtonsoft.Json;
using Skyline.DataMiner.Scripting;

namespace QAction_1
{
    public static class CMCUtils
    {
        public static CategoriesQActionRow ToQActionRow(Category category)
        {
            return new CategoriesQActionRow
            {
                Categoriesid_3001 = category.Id,
                Categoriesname_3002 = category.Name,
                Categoriestitle_3003 = category.Title,
                Categoriesdescription_3004 = category.Description,
                Categoriesnumtokens_3005 = category.NumTokens,
                Categoriesavgpricechange_3006 = category.AvgPriceChange,
                Categoriesmarketcap_3007 = category.MarketCap,
                Categoriesmarketcapchange_3008 = category.MarketCapChange,
                Categoriesvolume_3009 = category.Volume,
                Categoriesvolumechange_3010 = category.VolumeChange,
                Categorieslastupdated_3011 = category.LastUpdated?.ToOADate(),
            };
        }

        public sealed class Category
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("num_tokens")]
            public int? NumTokens { get; set; }

            [JsonProperty("avg_price_change")]
            public double? AvgPriceChange { get; set; }

            [JsonProperty("market_cap")]
            public double? MarketCap { get; set; }

            [JsonProperty("market_cap_change")]
            public double? MarketCapChange { get; set; }

            [JsonProperty("volume")]
            public double? Volume { get; set; }

            [JsonProperty("volume_change")]
            public double? VolumeChange { get; set; }

            [JsonProperty("last_updated")]
            public DateTime? LastUpdated { get; set; }
        }

        public sealed class Status
        {
            [JsonProperty("timestamp")]
            public DateTime? Timestamp { get; set; }

            [JsonProperty("error_code")]
            public int? ErrorCode { get; set; }

            [JsonProperty("error_message")]
            public string ErrorMessage { get; set; }

            [JsonProperty("elapsed")]
            public int? Elapsed { get; set; }

            [JsonProperty("credit_count")]
            public int? CreditCount { get; set; }

            [JsonProperty("notice")]
            public string Notice { get; set; }
        }

        public class CMCException : Exception
        {
            public CMCException()
            {
            }

            public CMCException(string message) : base(message)
            {
            }

            public CMCException(string message, Exception innerException) : base(message, innerException)
            {
            }
        }
    }
}