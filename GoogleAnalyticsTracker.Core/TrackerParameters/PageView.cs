namespace GoogleAnalyticsTracker.Core.TrackerParameters
{
    public class PageView : GeneralParameters
    {        
        #region Overrides of GeneralParameters

        /// <summary>
        /// The type of hit. Must be one of 'pageview', 'screenview', 'event', 'transaction', 'item', 'social', 'exception', 'timing'.
        /// <remarks>Required for all hit types</remarks>
        /// <example>HitType.Pageview</example>
        /// </summary>  
        public override HitType HitType
        {
            get { return HitType.Pageview; }
        }

        #endregion
    }
}