// Ignore Spelling: Eth Btc Fi Stablecoin Cryptocurrencies Crypto

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.Utils.Net.Http;
using Skyline.DataMiner.Utils.SecureCoding.SecureSerialization.Json.Newtonsoft;
using static QAction_1.CmcUtils;

/// <summary>
/// DataMiner QAction Class: ParseGlobalMetricsLatest.
/// </summary>
public static class QAction
{
    /// <summary>
    /// The QAction entry point?.
    /// </summary>
    /// <param name="protocol">Link with SLProtocol process.</param>
    public static void Run(SLProtocolExt protocol)
    {
        try
        {
            var httpStatusLine = SLHttpStatusLine.Parse((string)protocol.Httpglobalmetricslateststatuscode_400);
            if (httpStatusLine.StatusCode != SLHttpStatusCode.OK)
            {
                throw new CmcException($"Received invalid status code {httpStatusLine.StatusCode}");
            }

            var notAvailableValue = (double)protocol.Datanotavailablefixed__fixed;

            var responseJson = (string)protocol.Httpglobalmetricslatestcontent_401;
            var response = SecureNewtonsoftDeserialization.DeserializeObject<Response>(responseJson);

            var parameters = new Dictionary<int, object>
            {
                [Parameter.globalmetricslatestactivecryptocurrencies_450] = response.Data.ActiveCryptocurrencies ?? notAvailableValue,
                [Parameter.globalmetricslatesttotalcryptocurrencies_451] = response.Data.TotalCryptocurrencies ?? notAvailableValue,
                [Parameter.globalmetricslatestactivemarketpairs_452] = response.Data.ActiveMarketPairs ?? notAvailableValue,
                [Parameter.globalmetricslatestactiveexchanges_453] = response.Data.ActiveExchanges ?? notAvailableValue,
                [Parameter.globalmetricslatesttotalexchanges_454] = response.Data.TotalExchanges ?? notAvailableValue,
                [Parameter.globalmetricslatestethdominance_455] = response.Data.EthDominance ?? notAvailableValue,
                [Parameter.globalmetricslatestbtcdominance_456] = response.Data.BtcDominance ?? notAvailableValue,
                [Parameter.globalmetricslatestethdominanceyesterday_457] = response.Data.EthDominanceYesterday ?? notAvailableValue,
                [Parameter.globalmetricslatestbtcdominanceyesterday_458] = response.Data.BtcDominanceYesterday ?? notAvailableValue,
                [Parameter.globalmetricslatestethdominance24hpercentagechange_459] = response.Data.EthDominance24HChange ?? notAvailableValue,
                [Parameter.globalmetricslatestbtcdominance24hpercentagechange_460] = response.Data.BtcDominance24HChange ?? notAvailableValue,
                [Parameter.globalmetricslatestdefivolume24h_461] = response.Data.DeFiVolume24H ?? notAvailableValue,
                [Parameter.globalmetricslatestdefivolume24hreported_462] = response.Data.DeFiVolume24HReported ?? notAvailableValue,
                [Parameter.globalmetricslatestdefimarketcap_463] = response.Data.DeFiMarketCap ?? notAvailableValue,
                [Parameter.globalmetricslatestdefi24hpercentagechange_464] = response.Data.DeFi24HChange ?? notAvailableValue,
                [Parameter.globalmetricslateststablecoinvolume24h_465] = response.Data.StablecoinVolume24H ?? notAvailableValue,
                [Parameter.globalmetricslateststablecoinvolume24hreported_466] = response.Data.StablecoinVolume24HReported ?? notAvailableValue,
                [Parameter.globalmetricslateststablecoinmarketcap_467] = response.Data.StablecoinMarketCap ?? notAvailableValue,
                [Parameter.globalmetricslateststablecoin24hpercentagechange_468] = response.Data.Stablecoin24HChange ?? notAvailableValue,
                [Parameter.globalmetricslatestderivativesvolume24h_469] = response.Data.DerivativesVolume24H ?? notAvailableValue,
                [Parameter.globalmetricslatestderivativesvolume24hreported_470] = response.Data.DerivativesVolume24HReported ?? notAvailableValue,
                [Parameter.globalmetricslatestderivatives24hpercentagechange_471] = response.Data.Derivatives24HChange ?? notAvailableValue,
                [Parameter.globalmetricslatesttotalcryptodexcurrencies_472] = response.Data.TotalCryptoDexCurrencies ?? notAvailableValue,
                [Parameter.globalmetricslatesttodayincrementalcryptonumber_473] = response.Data.TodayIncrementalCryptoNumber ?? notAvailableValue,
                [Parameter.globalmetricslatestpast24hincrementalcryptonumber_474] = response.Data.Past24HIncrementalCryptoNumber ?? notAvailableValue,
                [Parameter.globalmetricslatestpast7dincrementalcryptonumber_475] = response.Data.Past7DIncrementalCryptoNumber ?? notAvailableValue,
                [Parameter.globalmetricslatestpast30dincrementalcryptonumber_476] = response.Data.Past30DIncrementalCryptoNumber ?? notAvailableValue,
                [Parameter.globalmetricslatesttodaychangepercent_477] = response.Data.TodayChangePercent ?? notAvailableValue,
                [Parameter.globalmetricslatesttrackedyearlynumbermaxincrementalnumber_478] = response.Data.TrackedYearlyNumber.MaxIncrementalNumber ?? notAvailableValue,
                [Parameter.globalmetricslatesttrackedyearlynumberminincrementalnumber_479] = response.Data.TrackedYearlyNumber.MinIncrementalNumber ?? notAvailableValue,
                [Parameter.globalmetricslatesttrackedyearlynumbermaxincrementaldate_480] = response.Data.TrackedYearlyNumber.MaxIncrementalDate?.ToOADate() ?? notAvailableValue,
                [Parameter.globalmetricslatesttrackedyearlynumberminincrementaldate_481] = response.Data.TrackedYearlyNumber.MinIncrementalDate?.ToOADate() ?? notAvailableValue,
                [Parameter.globalmetricslatestlastupdated_482] = response.Data.LastUpdated?.ToOADate() ?? notAvailableValue,
            };

            SetParameters(protocol, parameters);
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }

    private static void SetParameters(SLProtocolExt protocol, IReadOnlyDictionary<int, object> parameters)
    {
        var keyValuePairs = parameters.ToList();
        var parameterIds = keyValuePairs.Select(pair => pair.Key).ToArray();
        var parametervalues = keyValuePairs.Select(pair => pair.Value).ToArray();
        protocol.SetParameters(parameterIds, parametervalues);
    }

    public class Response
    {
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("data")]
        public GlobalMetricsLatestData Data { get; set; }

        public class GlobalMetricsLatestData
        {
            [JsonProperty("active_cryptocurrencies")]
            public int? ActiveCryptocurrencies { get; set; }

            [JsonProperty("total_cryptocurrencies")]
            public int? TotalCryptocurrencies { get; set; }

            [JsonProperty("active_market_pairs")]
            public int? ActiveMarketPairs { get; set; }

            [JsonProperty("active_exchanges")]
            public int? ActiveExchanges { get; set; }

            [JsonProperty("total_exchanges")]
            public int? TotalExchanges { get; set; }

            [JsonProperty("eth_dominance")]
            public double? EthDominance { get; set; }

            [JsonProperty("btc_dominance")]
            public double? BtcDominance { get; set; }

            [JsonProperty("eth_dominance_yesterday")]
            public double? EthDominanceYesterday { get; set; }

            [JsonProperty("btc_dominance_yesterday")]
            public double? BtcDominanceYesterday { get; set; }

            [JsonProperty("eth_dominance_24h_percentage_change")]
            public double? EthDominance24HChange { get; set; }

            [JsonProperty("btc_dominance_24h_percentage_change")]
            public double? BtcDominance24HChange { get; set; }

            [JsonProperty("defi_volume_24h")]
            public double? DeFiVolume24H { get; set; }

            [JsonProperty("defi_volume_24h_reported")]
            public double? DeFiVolume24HReported { get; set; }

            [JsonProperty("defi_market_cap")]
            public double? DeFiMarketCap { get; set; }

            [JsonProperty("defi_24h_percentage_change")]
            public double? DeFi24HChange { get; set; }

            [JsonProperty("stablecoin_volume_24h")]
            public double? StablecoinVolume24H { get; set; }

            [JsonProperty("stablecoin_volume_24h_reported")]
            public double? StablecoinVolume24HReported { get; set; }

            [JsonProperty("stablecoin_market_cap")]
            public double? StablecoinMarketCap { get; set; }

            [JsonProperty("stablecoin_24h_percentage_change")]
            public double? Stablecoin24HChange { get; set; }

            [JsonProperty("derivatives_volume_24h")]
            public double? DerivativesVolume24H { get; set; }

            [JsonProperty("derivatives_volume_24h_reported")]
            public double? DerivativesVolume24HReported { get; set; }

            [JsonProperty("derivatives_24h_percentage_change")]
            public double? Derivatives24HChange { get; set; }

            [JsonProperty("total_crypto_dex_currencies")]
            public int? TotalCryptoDexCurrencies { get; set; }

            [JsonProperty("today_incremental_crypto_number")]
            public int? TodayIncrementalCryptoNumber { get; set; }

            [JsonProperty("past_24h_incremental_crypto_number")]
            public int? Past24HIncrementalCryptoNumber { get; set; }

            [JsonProperty("past_7d_incremental_crypto_number")]
            public int? Past7DIncrementalCryptoNumber { get; set; }

            [JsonProperty("past_30d_incremental_crypto_number")]
            public int? Past30DIncrementalCryptoNumber { get; set; }

            [JsonProperty("today_change_percent")]
            public double? TodayChangePercent { get; set; }

            [JsonProperty("tracked_yearly_number")]
            public TrackedYearlyNumberData TrackedYearlyNumber { get; set; }

            [JsonProperty("last_updated")]
            public DateTime? LastUpdated { get; set; }

            public class TrackedYearlyNumberData
            {
                [JsonProperty("maxIncrementalNumber")]
                public int? MaxIncrementalNumber { get; set; }

                [JsonProperty("minIncrementalNumber")]
                public int? MinIncrementalNumber { get; set; }

                [JsonProperty("maxIncrementalDate")]
                public DateTime? MaxIncrementalDate { get; set; }

                [JsonProperty("minIncrementalDate")]
                public DateTime? MinIncrementalDate { get; set; }
            }
        }
    }
}