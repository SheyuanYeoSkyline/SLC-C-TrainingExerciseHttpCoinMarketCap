using System;

using Skyline.DataMiner.Scripting;

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
            protocol.CheckTrigger(350); // Hard-coded trigger ID.
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }
}