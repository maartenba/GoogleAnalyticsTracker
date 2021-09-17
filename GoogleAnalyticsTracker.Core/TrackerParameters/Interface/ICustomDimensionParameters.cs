using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters.Interface
{
    /// <summary>
    /// Custom dimension parameters. Currently, 20 indices for the standard GA account is supported only.
    /// </summary>
    [PublicAPI]
    public interface ICustomDimensionParameters
    {
        string? CustomDimension1 { get; set; }

        string? CustomDimension2 { get; set; }

        string? CustomDimension3 { get; set; }

        string? CustomDimension4 { get; set; }

        string? CustomDimension5 { get; set; }

        string? CustomDimension6 { get; set; }

        string? CustomDimension7 { get; set; }

        string? CustomDimension8 { get; set; }

        string? CustomDimension9 { get; set; }

        string? CustomDimension10 { get; set; }

        string? CustomDimension11 { get; set; }

        string? CustomDimension12 { get; set; }

        string? CustomDimension13 { get; set; }

        string? CustomDimension14 { get; set; }

        string? CustomDimension15 { get; set; }

        string? CustomDimension16 { get; set; }

        string? CustomDimension17 { get; set; }

        string? CustomDimension18 { get; set; }

        string? CustomDimension19 { get; set; }

        string? CustomDimension20 { get; set; }
    }
}