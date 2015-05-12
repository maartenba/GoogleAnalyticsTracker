namespace GoogleAnalyticsTracker.Owin
{
    using GoogleAnalyticsTracker.Core;

    using Microsoft.Owin;

    public class CookieBasedAnalyticsSession : AnalyticsSession
    {
        /// <summary>Unique identifier for the storage key.</summary>
        private const string StorageKeyUniqueId = "_GAT_uqid";

        /// <summary>The storage key first visit time.</summary>
        private const string StorageKeyFirstVisitTime = "_GAT_fvt";

        /// <summary>The storage key previous visit time.</summary>
        private const string StorageKeyPreviousVisitTime = "_GAT_pvt";

        /// <summary>Number of storage key sessions.</summary>
        private const string StorageKeySessionCount = "_GAT_sc";

        /// <summary>The context.</summary>
        private readonly IOwinContext context;

        /// <summary>
        /// Initializes a new instance of the CookieBasedAnalyticsSession class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CookieBasedAnalyticsSession(IOwinContext context)
        {
            this.context = context;

            // Create properties at once as OWIN for .NET 4.5.x has a bug that makes SystemWeb throw an error when adding cookies
            // This is fixed in .NET 4.6
            this.GetOrCreateUniqueVisitorId(true);
            this.GetOrCreateFirstVisitTime(true);
            this.GetOrCreatePreviousVisitTime(true);
            this.GetOrCreateSessionCount(true);
        }

        /// <summary>Gets the unique visitor identifier.</summary>
        /// <returns>The unique visitor identifier.</returns>
        protected override string GetUniqueVisitorId()
        {
            return this.GetOrCreateUniqueVisitorId(false);
        }

        /// <summary>Gets the first visit time.</summary>
        /// <returns>The first visit time.</returns>
        protected override int GetFirstVisitTime()
        {
            return this.GetOrCreateFirstVisitTime(false);
        }

        /// <summary>Gets the previous visit time.</summary>
        /// <returns>The previous visit time.</returns>
        protected override int GetPreviousVisitTime()
        {
            return this.GetOrCreatePreviousVisitTime(false);
        }

        /// <summary>Gets the session count.</summary>
        /// <returns>The session count.</returns>
        protected override int GetSessionCount()
        {
            return this.GetOrCreateSessionCount(false);
        }

        /// <summary>Gets or creates the unique visitor identifier.</summary>
        /// <param name="createCookie">true to create cookie.</param>
        /// <returns>The unique visitor identifier.</returns>
        private string GetOrCreateUniqueVisitorId(bool createCookie)
        {
            var v = base.GetUniqueVisitorId();

            // If visitor has cookie, return value from cookie. 
            if (string.IsNullOrEmpty(this.context.Request.Cookies[StorageKeyUniqueId]))
            {
                if (createCookie)
                {
                    this.context.Response.Cookies.Append(StorageKeyUniqueId, v);
                }

                return v;
            }

            return this.context.Request.Cookies[StorageKeyUniqueId];
        }

        /// <summary>Gets or creates the first visit time.</summary>
        /// <param name="createCookie">true to create cookie.</param>
        /// <returns>The first visit time.</returns>
        private int GetOrCreateFirstVisitTime(bool createCookie)
        {
            int v;

            if (int.TryParse(this.context.Request.Cookies[StorageKeyFirstVisitTime], out v) && v == 0)
            {
                v = base.GetFirstVisitTime();

                if (createCookie)
                {
                    this.context.Response.Cookies.Append(StorageKeyFirstVisitTime, v.ToString());
                }
            }

            return v;
        }

        /// <summary>Gets or creates the previous visit time.</summary>
        /// <param name="createCookie">true to create cookie.</param>
        /// <returns>The previous visit time.</returns>
        private int GetOrCreatePreviousVisitTime(bool createCookie)
        {
            int v;
            int.TryParse(this.context.Request.Cookies[StorageKeyPreviousVisitTime], out v);

            if (createCookie)
            {
                this.context.Response.Cookies.Append(StorageKeyPreviousVisitTime, base.GetCurrentVisitTime().ToString());
            }

            if (v == 0)
            {
                v = base.GetCurrentVisitTime();
            }

            return v;
        }

        /// <summary>Gets or creates the session count.</summary>
        /// <param name="createCookie">true to create cookie.</param>
        /// <returns>The session count.</returns>
        private int GetOrCreateSessionCount(bool createCookie)
        {
            int v;
            int.TryParse(this.context.Request.Cookies[StorageKeySessionCount], out v);

            v = v + 1;

            if (createCookie)
            {
                this.context.Response.Cookies.Append(StorageKeySessionCount, v.ToString());
            }

            return v;
        }
    }
}