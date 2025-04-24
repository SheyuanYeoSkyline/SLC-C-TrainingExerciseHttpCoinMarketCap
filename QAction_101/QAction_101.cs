// Ignore Spelling: Cmc

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.Utils.Net.Http;
using Skyline.DataMiner.Utils.SecureCoding.SecureSerialization.Json.Newtonsoft;
using static QAction_1.CmcUtils;

/// <summary>
/// DataMiner QAction Class: ParseResponse.
/// </summary>
public static class QAction
{
    private static double notAvailableValue;
    private static double infiniteValue;

    /// <summary>
    /// The QAction entry point.
    /// </summary>
    /// <param name="protocol">Link with SLProtocol process.</param>
    public static void Run(SLProtocolExt protocol)
    {
        try
        {
            var httpStatusLine = SLHttpStatusLine.Parse((string)protocol.Httplistingsstatuscode_100);
            if (httpStatusLine.StatusCode != SLHttpStatusCode.OK)
            {
                throw new CmcException($"Received invalid status code {httpStatusLine.StatusCode}");
            }

            notAvailableValue = (double)protocol.Datanotavailablefixed__fixed;
            infiniteValue = (double)protocol.Infinityfixed__fixed;

            var responseJson = (string)protocol.Httplistingscontent_101;
            var response = SecureNewtonsoftDeserialization.DeserializeObject<Response>(responseJson);

            PopulateListingTable(protocol, response);
            PopulateQuoteTable(protocol, response);
            PopulateListingUsdTable(protocol, response);
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }

    private static void PopulateListingTable(SLProtocolExt protocol, Response response)
    {
        var dataRows = response.Data
                    .Select(ToListingQActionRow)
                    .ToArray();
        var listingTable = protocol.listing;
        listingTable.FillArray(listingTable.QActionRowsToObjectFillArray(dataRows));
    }

    private static void PopulateQuoteTable(SLProtocolExt protocol, Response response)
    {
        var quoteRows = response.Data
                .SelectMany(cryptocurrency => cryptocurrency.Quote
                .Select(quote => ToQActionRow(quote.Value, cryptocurrency.Id.ToString(), quote.Key)))
                .ToArray();
        var quoteTable = protocol.quote;
        quoteTable.FillArray(quoteTable.QActionRowsToObjectFillArray(quoteRows));
    }

    private static void PopulateListingUsdTable(SLProtocolExt protocol, Response response)
    {
        // ASSUMPTION: Quote always contains USD.
        var listingUsdRows = response.Data
            .Select(ToListingusdQActionRow)
            .ToArray();
        var listingUsdTable = protocol.listingusd;

        listingUsdTable.FillArray(listingUsdTable.QActionRowsToObjectFillArray(listingUsdRows));
    }

    private static ListingusdQActionRow ToListingusdQActionRow(Response.Listing dataEntry)
    {
        var quote = dataEntry.Quote["USD"];
        return new ListingusdQActionRow
        {
            Listingusdid_8001 = dataEntry.Id.ToString(),
            Listingusdcmcrank_8002 = dataEntry.CmcRank ?? notAvailableValue,
            Listingusdname_8003 = dataEntry.Name,
            Listingusdprice_8004 = quote.Price.HasValue ? quote.Price : notAvailableValue,
            Listingusdpercentchange1h_8005 = quote.PercentChange1H ?? notAvailableValue,
            Listingusdpercentchange24h_8006 = quote.PercentChange24H ?? notAvailableValue,
            Listingusdpercentchange7d_8007 = quote.PercentChange7D ?? notAvailableValue,
            Listingusdmarketcap_8008 = quote.MarketCap.HasValue ? quote.MarketCap : notAvailableValue,
            Listingusdvolume_8009 = quote.Volume24H.HasValue ? quote.Volume24H : notAvailableValue,
            Listingusdcirculatingsupply_8010 = dataEntry.CirculatingSupply.HasValue ? $"{dataEntry.CirculatingSupply} {dataEntry.Symbol}" : notAvailableValue.ToString(),
        };
    }

    private static ListingQActionRow ToListingQActionRow(Response.Listing dataEntry)
    {
        return new ListingQActionRow
        {
            Listingid_1001 = dataEntry.Id.ToString(),
            Listingname_1002 = dataEntry.Name,
            Listingsymbol_1003 = dataEntry.Symbol,
            Listingcmcrank_1004 = dataEntry.CmcRank ?? notAvailableValue,
            Listingcirculatingsupply_1005 = dataEntry.CirculatingSupply ?? notAvailableValue,
            Listingtotalsupply_1006 = dataEntry.TotalSupply ?? notAvailableValue,
            Listingmaxsupply_1007 = dataEntry.InfiniteSupply ? infiniteValue : dataEntry.MaxSupply ?? notAvailableValue,
        };
    }

    private static QuoteQActionRow ToQActionRow(Response.Listing.ListingQuote quote, string cryptocurrencyId, string quoteCurrencySymbol)
    {
        return new QuoteQActionRow
        {
            Quoteid_2001 = $"{cryptocurrencyId}_{quoteCurrencySymbol}",
            Quoteprice_2002 = quote.Price ?? notAvailableValue,
            Quotepercentchange1h_2003 = quote.PercentChange1H ?? notAvailableValue,
            Quotepercentchange24h_2004 = quote.PercentChange24H ?? notAvailableValue,
            Quotepercentchange7d_2005 = quote.PercentChange7D ?? notAvailableValue,
            Quotemarketcap_2006 = quote.MarketCap ?? notAvailableValue,
            Quotelastupdated_2007 = quote.LastUpdated?.ToOADate(),
            Quotesymbol_2008 = quoteCurrencySymbol,
            Quotecryptocurrencyid_2500 = cryptocurrencyId,
        };
    }

    private sealed class Response
    {
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("data")]
        public List<Listing> Data { get; set; }

        public sealed class Listing
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("symbol")]
            public string Symbol { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }

            [JsonProperty("cmc_rank")]
            public int? CmcRank { get; set; }

            [JsonProperty("num_market_pairs")]
            public int? NumMarketPairs { get; set; }

            [JsonProperty("circulating_supply")]
            public double? CirculatingSupply { get; set; }

            [JsonProperty("total_supply")]
            public double? TotalSupply { get; set; }

            [JsonProperty("max_supply")]
            public double? MaxSupply { get; set; }

            [JsonProperty("infinite_supply")]
            public bool InfiniteSupply { get; set; }

            [JsonProperty("last_updated")]
            public DateTime? LastUpdated { get; set; }

            [JsonProperty("date_added")]
            public DateTime? DateAdded { get; set; }

            [JsonProperty("self_reported_circulating_supply")]
            public double? SelfReportedCirculatingSupply { get; set; }

            [JsonProperty("self_reported_market_cap")]
            public double? SelfReportedMarketCap { get; set; }

            [JsonProperty("quote")]
            public Dictionary<string, ListingQuote> Quote { get; set; }

            public sealed class ListingQuote
            {
                [JsonProperty("price")]
                public double? Price { get; set; }

                [JsonProperty("volume_24h")]
                public double? Volume24H { get; set; }

                [JsonProperty("volume_change_24h")]
                public double? VolumeChange24H { get; set; }

                [JsonProperty("volume_24h_reported")]
                public double? Volume24HReported { get; set; }

                [JsonProperty("volume_7d")]
                public double? Volume7D { get; set; }

                [JsonProperty("volume_7d_reported")]
                public double? Volume7DReported { get; set; }

                [JsonProperty("volume_30d")]
                public double? Volume30D { get; set; }

                [JsonProperty("volume_30d_reported")]
                public double? Volume30DReported { get; set; }

                [JsonProperty("market_cap")]
                public double? MarketCap { get; set; }

                [JsonProperty("market_cap_dominance")]
                public double? MarketCapDominance { get; set; }

                [JsonProperty("fully_diluted_market_cap")]
                public double? FullyDilutedMarketCap { get; set; }

                [JsonProperty("tvl")]
                public double? TotalValueLocked { get; set; }

                [JsonProperty("percent_change_1h")]
                public double? PercentChange1H { get; set; }

                [JsonProperty("percent_change_24h")]
                public double? PercentChange24H { get; set; }

                [JsonProperty("percent_change_7d")]
                public double? PercentChange7D { get; set; }

                [JsonProperty("last_updated")]
                public DateTime? LastUpdated { get; set; }
            }
        }
    }
}