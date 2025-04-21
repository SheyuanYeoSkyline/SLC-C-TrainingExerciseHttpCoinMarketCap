using System;
using Newtonsoft.Json;
using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.Utils.Net.Http;
using Skyline.DataMiner.Utils.SecureCoding.SecureSerialization.Json.Newtonsoft;
using static QAction_1.CMCUtils;

/// <summary>
/// DataMiner QAction Class: ParseCategory.
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
            var httpStatusLine = SLHttpStatusLine.Parse((string)protocol.Httpcategorystatuscode_350);
            if (httpStatusLine.StatusCode != SLHttpStatusCode.OK)
            {
                throw new CMCException($"Received invalid status code {httpStatusLine.StatusCode}");
            }

            var responseJson = (string)protocol.Httpcategorycontent_351;
            var response = SecureNewtonsoftDeserialization.DeserializeObject<Response>(responseJson);

            var dataRow = ToQActionRow(response.Data);
            protocol.Log($"row id is {dataRow.Categoriesid_3001}, title {dataRow.Categoriestitle_3003}");
            protocol.categories.SetRow(dataRow);
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
        public Category Data { get; set; }
    }
}