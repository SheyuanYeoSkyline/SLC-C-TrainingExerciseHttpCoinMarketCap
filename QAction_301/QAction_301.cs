using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.Utils.Net.Http;
using Skyline.DataMiner.Utils.SecureCoding.SecureSerialization.Json.Newtonsoft;

using static Skyline.Protocol.CMCUtils;

/// <summary>
/// DataMiner QAction Class: ParseCategories.
/// </summary>
public static class QAction
{
    /// <summary>
    /// The QAction entry point.
    /// </summary>
    /// <param name="protocol">Link with SLProtocol process.</param>
    public static void Run(SLProtocolExt protocol)
    {
        try
        {
            var httpStatusLine = SLHttpStatusLine.Parse((string)protocol.Httpcategoriesstatuscode_300);
            if (httpStatusLine.StatusCode != SLHttpStatusCode.OK)
            {
                return;
            }

            var responseJson = (string)protocol.Httpcategoriescontent_301;
            var response = SecureNewtonsoftDeserialization.DeserializeObject<Response>(responseJson);

            var dataRows = response.Data.Select(ToQActionRow).ToArray();
            var categoriesTable = protocol.categories;
            categoriesTable.FillArray(categoriesTable.QActionRowsToObjectFillArray(dataRows));
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }

    private sealed class Response
    {
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("data")]
        public List<Category> Data { get; set; }
    }
}