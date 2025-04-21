// Ignore Spelling: Eth Btc Fi Stablecoin Cryptocurrencies Crypto

using System;
using Newtonsoft.Json;
using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.Utils.Net.Http;
using Skyline.DataMiner.Utils.SecureCoding.SecureSerialization.Json.Newtonsoft;
using static QAction_1.CMCUtils;

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
                throw new CMCException($"Received invalid status code {httpStatusLine.StatusCode}");
            }

            var notAvailableValue = (double)protocol.Datanotavailablefixed__fixed;

            var responseJson = (string)protocol.Httpglobalmetricslatestcontent_401;
            var response = SecureNewtonsoftDeserialization.DeserializeObject<Response>(responseJson);

            protocol.Globalmetricslatestactivecryptocurrencies_450 = response.Data.ActiveCryptocurrencies ?? notAvailableValue;
            protocol.Globalmetricslatesttotalcryptocurrencies_451 = response.Data.TotalCryptocurrencies ?? notAvailableValue;
            protocol.Globalmetricslatestactivemarketpairs_452 = response.Data.ActiveMarketPairs ?? notAvailableValue;
            protocol.Globalmetricslatestactiveexchanges_453 = response.Data.ActiveExchanges ?? notAvailableValue;
            protocol.Globalmetricslatesttotalexchanges_454 = response.Data.TotalExchanges ?? notAvailableValue;
            protocol.Globalmetricslatestethdominance_455 = response.Data.EthDominance ?? notAvailableValue;
            protocol.Globalmetricslatestbtcdominance_456 = response.Data.BtcDominance ?? notAvailableValue;
            protocol.Globalmetricslatestethdominanceyesterday_457 = response.Data.EthDominanceYesterday ?? notAvailableValue;
            protocol.Globalmetricslatestbtcdominanceyesterday_458 = response.Data.BtcDominanceYesterday ?? notAvailableValue;
            protocol.Globalmetricslatestethdominance24hpercentagechange_459 = response.Data.EthDominance24HChange ?? notAvailableValue;
            protocol.Globalmetricslatestbtcdominance24hpercentagechange_460 = response.Data.BtcDominance24HChange ?? notAvailableValue;
            protocol.Globalmetricslatestdefivolume24h_461 = response.Data.DeFiVolume24H ?? notAvailableValue;
            protocol.Globalmetricslatestdefivolume24hreported_462 = response.Data.DeFiVolume24HReported ?? notAvailableValue;
            protocol.Globalmetricslatestdefimarketcap_463 = response.Data.DeFiMarketCap ?? notAvailableValue;
            protocol.Globalmetricslatestdefi24hpercentagechange_464 = response.Data.DeFi24HChange ?? notAvailableValue;
            protocol.Globalmetricslateststablecoinvolume24h_465 = response.Data.StablecoinVolume24H ?? notAvailableValue;
            protocol.Globalmetricslateststablecoinvolume24hreported_466 = response.Data.StablecoinVolume24HReported ?? notAvailableValue;
            protocol.Globalmetricslateststablecoinmarketcap_467 = response.Data.StablecoinMarketCap ?? notAvailableValue;
            protocol.Globalmetricslateststablecoin24hpercentagechange_468 = response.Data.Stablecoin24HChange ?? notAvailableValue;
            protocol.Globalmetricslatestderivativesvolume24h_469 = response.Data.DerivativesVolume24H ?? notAvailableValue;
            protocol.Globalmetricslatestderivativesvolume24hreported_470 = response.Data.DerivativesVolume24HReported ?? notAvailableValue;
            protocol.Globalmetricslatestderivatives24hpercentagechange_471 = response.Data.Derivatives24HChange ?? notAvailableValue;
            protocol.Globalmetricslatesttotalcryptodexcurrencies_472 = response.Data.TotalCryptoDexCurrencies ?? notAvailableValue;
            protocol.Globalmetricslatesttodayincrementalcryptonumber_473 = response.Data.TodayIncrementalCryptoNumber ?? notAvailableValue;
            protocol.Globalmetricslatestpast24hincrementalcryptonumber_474 = response.Data.Past24HIncrementalCryptoNumber ?? notAvailableValue;
            protocol.Globalmetricslatestpast7dincrementalcryptonumber_475 = response.Data.Past7DIncrementalCryptoNumber ?? notAvailableValue;
            protocol.Globalmetricslatestpast30dincrementalcryptonumber_476 = response.Data.Past30DIncrementalCryptoNumber ?? notAvailableValue;
            protocol.Globalmetricslatesttodaychangepercent_477 = response.Data.TodayChangePercent ?? notAvailableValue;
            protocol.Globalmetricslatesttrackedyearlynumbermaxincrementalnumber_478 = response.Data.TrackedYearlyNumber.MaxIncrementalNumber ?? notAvailableValue;
            protocol.Globalmetricslatesttrackedyearlynumberminincrementalnumber_479 = response.Data.TrackedYearlyNumber.MinIncrementalNumber ?? notAvailableValue;
            protocol.Globalmetricslatesttrackedyearlynumbermaxincrementaldate_480 = response.Data.TrackedYearlyNumber.MaxIncrementalDate?.ToOADate() ?? notAvailableValue;
            protocol.Globalmetricslatesttrackedyearlynumberminincrementaldate_481 = response.Data.TrackedYearlyNumber.MinIncrementalDate?.ToOADate() ?? notAvailableValue;
            protocol.Globalmetricslatestlastupdated_482 = response.Data.LastUpdated?.ToOADate() ?? notAvailableValue;
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
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