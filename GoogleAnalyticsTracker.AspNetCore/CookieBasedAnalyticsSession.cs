using System;
using GoogleAnalyticsTracker.Core;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace GoogleAnalyticsTracker.AspNet
{
    [PublicAPI]
    public class CookieBasedAnalyticsSession : AnalyticsSession
    {
        /// <summary>Unique identifier for the storage key.</summary>
        private const string StorageKeyUniqueId = "_GAT_uqid";

        /// <summary>The storage key for first visit time.</summary>
        private const string StorageKeyFirstVisitTime = "_GAT_fvt";

        /// <summary>The storage key for previous visit time.</summary>
        private const string StorageKeyPreviousVisitTime = "_GAT_pvt";

        /// <summary>The storage key for last session id.</summary>
        private const string StorageKeySessionId = "_GAT_si";

        /// <summary>The storage key for number of sessions.</summary>
        private const string StorageKeySessionCount = "_GAT_sc";

        private readonly IHttpContextAccessor _contextAccessor;

        private readonly bool _sessionFeatureAvailable;

        /// <summary>
        /// Initializes a new instance of the CookieBasedAnalyticsSession class.
        /// </summary>
        /// <param name="contextAccessor">The HTTP context accessor.</param>
        public CookieBasedAnalyticsSession([NotNull] IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
            _sessionFeatureAvailable = _contextAccessor.HttpContext?.Features?.Get<ISessionFeature>() != null;

            // Create properties at once.
            GetOrCreateUniqueVisitorId(true);
            GetOrCreateFirstVisitTime(true);
            GetOrCreatePreviousVisitTime(true);
            GetOrCreateSessionCount(true);
        }

        /// <summary>Gets the unique visitor identifier.</summary>
        /// <returns>The unique visitor identifier.</returns>
        protected override string GetUniqueVisitorId() => GetOrCreateUniqueVisitorId(false);

        /// <summary>Gets the first visit time.</summary>
        /// <returns>The first visit time.</returns>
        protected override int GetFirstVisitTime() => GetOrCreateFirstVisitTime(false);

        /// <summary>Gets the previous visit time.</summary>
        /// <returns>The previous visit time.</returns>
        protected override int GetPreviousVisitTime() => GetOrCreatePreviousVisitTime(false);

        /// <summary>Gets the session count.</summary>
        /// <returns>The session count.</returns>
        protected override int GetSessionCount() => GetOrCreateSessionCount(false);

        /// <summary>Gets or creates the unique visitor identifier.</summary>
        /// <param name="createCookie">true to create cookie.</param>
        /// <returns>The unique visitor identifier.</returns>
        private string GetOrCreateUniqueVisitorId(bool createCookie)
        {
            var v = base.GetUniqueVisitorId();
            
            // If no HTTP context available, bail out...
            if (_contextAccessor.HttpContext == null) return v;

            // If visitor has cookie, return value from cookie. 
            // ReSharper disable once InvertIf
            if (string.IsNullOrEmpty(_contextAccessor.HttpContext.Request.Cookies[StorageKeyUniqueId]))
            {
                if (createCookie)
                {
                    _contextAccessor.HttpContext.Response.Cookies.Append(StorageKeyUniqueId, v);
                }

                return v;
            }

            return _contextAccessor.HttpContext.Request.Cookies[StorageKeyUniqueId];
        }

        /// <summary>Gets or creates the first visit time.</summary>
        /// <param name="createCookie">true to create cookie.</param>
        /// <returns>The first visit time.</returns>
        private int GetOrCreateFirstVisitTime(bool createCookie)
        {
            // If no HTTP context available, bail out...
            if (_contextAccessor.HttpContext == null) return base.GetFirstVisitTime();

            // ReSharper disable once InvertIf
            if (int.TryParse(_contextAccessor.HttpContext.Request.Cookies[StorageKeyFirstVisitTime], out var v) && v == 0)
            {
                v = base.GetFirstVisitTime();

                if (createCookie)
                {
                    _contextAccessor.HttpContext.Response.Cookies.Append(StorageKeyFirstVisitTime, v.ToString());
                }
            }

            return v;
        }

        /// <summary>Gets or creates the previous visit time.</summary>
        /// <param name="createCookie">true to create cookie.</param>
        /// <returns>The previous visit time.</returns>
        private int GetOrCreatePreviousVisitTime(bool createCookie)
        {
            // If no HTTP context available, bail out...
            if (_contextAccessor.HttpContext == null) return base.GetCurrentVisitTime();

            int.TryParse(_contextAccessor.HttpContext.Request.Cookies[StorageKeyPreviousVisitTime], out var v);

            if (createCookie)
            {
                _contextAccessor.HttpContext.Response.Cookies.Append(StorageKeyPreviousVisitTime, GetCurrentVisitTime().ToString());
            }

            if (v == 0)
            {
                v = GetCurrentVisitTime();
            }

            return v;
        }

        /// <summary>Gets or creates the session count.</summary>
        /// <param name="createCookie">true to create cookie.</param>
        /// <returns>The session count.</returns>
        private int GetOrCreateSessionCount(bool createCookie)
        {
            // If no HTTP context available/no session ID available, bail out...
            if (_contextAccessor.HttpContext == null || !_sessionFeatureAvailable) return 0;
            
            // Get current session count
            int.TryParse(_contextAccessor.HttpContext.Request.Cookies[StorageKeySessionCount], out var v);
            
            // Does the session match the previous session? If not, increment session count.
            if (!string.IsNullOrEmpty(_contextAccessor.HttpContext.Request.Cookies[StorageKeySessionId]) && 
                _contextAccessor.HttpContext.Request.Cookies[StorageKeySessionId] != _contextAccessor.HttpContext.Session?.Id)
            {
                v += 1;
            }
            
            // ReSharper disable once InvertIf
            if (createCookie)
            {
                _contextAccessor.HttpContext.Response.Cookies.Append(StorageKeySessionId, _contextAccessor.HttpContext.Session?.Id);
                _contextAccessor.HttpContext.Response.Cookies.Append(StorageKeySessionCount, v.ToString());
            }

            return v;
        }
    }
}