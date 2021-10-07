using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters.Interface;

[PublicAPI]
public interface IContentExperimentsParameters
{
    /// <summary>
    /// Specifies the experiment id.
    /// <remarks>Required for experiment tracking</remarks>
    /// <example>K7Q-9lpLSd21prp9vIhdoA</example>
    /// </summary>        
    string ExperimentId { get; set; }

    /// <summary>
    /// Specifies the experiment variant id.
    /// <remarks>Required for content experiment tracking</remarks>
    /// <example>1</example>
    /// </summary>        
    string ExperimentVariant { get; set; }
}