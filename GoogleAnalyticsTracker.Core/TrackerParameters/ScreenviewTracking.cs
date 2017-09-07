namespace GoogleAnalyticsTracker.Core.TrackerParameters
{
    public class ScreenviewTracking : GeneralParameters
    {
        #region Overrides of GeneralParameters

        /// <summary>
        /// The type of hit. Must be one of 'pageview', 'screenview', 'event', 'transaction', 'item', 'social', 'exception', 'timing'.
        /// <remarks>Required for all hit types</remarks>
        /// <example>HitType.Pageview</example>
        /// </summary>
        public override HitType HitType
        {
            get { return HitType.Screenview; }
        }

        #endregion Overrides of GeneralParameters
    }
}