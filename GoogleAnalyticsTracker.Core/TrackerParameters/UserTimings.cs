using GoogleAnalyticsTracker.Core.TrackerParameters.Interface;
using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters;

[PublicAPI]
public class UserTimings : GeneralParameters, ITimingParameters
{
    public UserTimings()
    {
        NonInteractionHit = true;
    }

    #region Overrides of GeneralParameters

    /// <summary>
    /// The type of hit. Must be one of 'pageview', 'screenview', 'event', 'transaction', 'item', 'social', 'exception', 'timing'.
    /// <remarks>Required for all hit types</remarks>
    /// <example>HitType.Pageview</example>
    /// </summary>  
    public override HitType HitType => HitType.Timing;

    #endregion

    #region Implementation of ITimingParameters

    /// <summary>
    /// Specifies the user timing category.
    /// <remarks>Required for timing hit type</remarks>
    /// <example>category</example>
    /// </summary>
    [Beacon("utc", true)]
    public string? UserTimingCategory { get; set; }

    /// <summary>
    /// Specifies the user timing variable.
    /// <remarks>Required for timing hit type</remarks>
    /// <example>lookup</example>
    /// </summary>
    [Beacon("utv", true)]
    public string? UserTimingVariable { get; set; }

    /// <summary>
    /// Specifies the user timing value. The value is in milliseconds.
    /// <remarks>Required for timing hit type</remarks>
    /// <example>123</example>
    /// </summary>
    [Beacon("utt", true)]
    public long UserTimingTime { get; set; }

    /// <summary>
    /// Specifies the user timing label.
    /// <remarks>Optional for timing hit type</remarks>
    /// <example>label</example>
    /// </summary>
    [Beacon("utl")]
    public string? UserTimingLabel { get; set; }

    /// <summary>
    /// Specifies the time it took for a page to load. The value is in milliseconds.
    /// <remarks>Optional</remarks>
    /// <example>3554</example>
    /// </summary>
    [Beacon("plt")]
    public long? PageLoadTime { get; set; }

    /// <summary>
    /// Specifies the time it took to do a DNS lookup.The value is in milliseconds.
    /// <remarks>Optional</remarks>
    /// <example>43</example>
    /// </summary>
    [Beacon("dns")]
    public long? DnsTime { get; set; }

    /// <summary>
    /// Specifies the time it took for the page to be downloaded. The value is in milliseconds.
    /// <remarks>Optional</remarks>
    /// <example>500</example>
    /// </summary>
    [Beacon("pdt")]
    public long? PageDownloadTime { get; set; }

    /// <summary>
    /// Specifies the time it took for any redirects to happen. The value is in milliseconds.
    /// <remarks>Optional</remarks>
    /// <example>500</example>
    /// </summary>
    [Beacon("rrt")]
    public long? RedirectResponseTime { get; set; }

    /// <summary>
    /// Specifies the time it took for a TCP connection to be made. The value is in milliseconds.
    /// <remarks>Optional</remarks>
    /// <example>500</example>
    /// </summary>
    [Beacon("tcp")]
    public long? TcpConnectTime { get; set; }

    /// <summary>
    /// Specifies the time it took for the server to respond after the connect time. The value is in milliseconds.
    /// <remarks>Optional</remarks>
    /// <example>500</example>
    /// </summary>
    [Beacon("srt")]
    public long? ServerResponseTime { get; set; }

    /// <summary>
    /// Specifies the time it took for Document.readyState to be 'interactive'. The value is in milliseconds.
    /// <remarks>Optional</remarks>
    /// <example>500</example>
    /// </summary>
    [Beacon("dit")]
    public long? DomInteractiveTime { get; set; }

    /// <summary>
    /// Specifies the time it took for the DOMContentLoaded Event to fire. The value is in milliseconds.
    /// <remarks>Optional</remarks>
    /// <example>500</example>
    /// </summary>
    [Beacon("clt")]
    public long? ContentLoadTime { get; set; }

    #endregion
}