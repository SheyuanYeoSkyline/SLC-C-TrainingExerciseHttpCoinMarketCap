using System;

using Skyline.DataMiner.Scripting;
using Skyline.DataMiner.Utils.Protocol.Extension;

/// <summary>
/// DataMiner QAction Class: RefreshCategory.
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
            var refreshedId = protocol.RowKey();
            var httpRequestUrl = $"v1/cryptocurrency/category?id={refreshedId}";
            protocol.Httpcategoryrequesturl_349 = httpRequestUrl;
            protocol.RunAction(350); // Hard-coded action ID to poll single category.
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }
}