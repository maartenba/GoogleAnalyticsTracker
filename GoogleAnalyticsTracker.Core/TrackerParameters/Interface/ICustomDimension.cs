using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters.Interface
{
    [PublicAPI]
    public interface ICustomDimension
    {
        /// <summary>
        /// Index of custom dimension (from 1 to 200).
        /// </summary>
        int Id { set; }

        /// <summary>
        /// Parameter name of the custom dimension.
        /// <example>cd4</example>
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Value of custom dimension (max 150 bytes).
        /// </summary>
        string Value { get; set; }
    }
}
