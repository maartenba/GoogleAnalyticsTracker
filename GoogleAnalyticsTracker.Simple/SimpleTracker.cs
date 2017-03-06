using System;
using System.Threading.Tasks;
using GoogleAnalyticsTracker.Core;
using GoogleAnalyticsTracker.Core.Interface;
using GoogleAnalyticsTracker.Core.TrackerParameters;

namespace GoogleAnalyticsTracker.Simple
{



    public class SimpleTracker : TrackerBase
    {
        static SimpleTracker instance;

        /// <summary>
        /// Specifies the application name.
        /// <remarks>Optional</remarks>
        /// <example>My App</example>
        /// </summary>
        public string appName;
        /// <summary>
        /// Application identifier.
        /// <remarks>Optional</remarks>
        /// <example>com.company.app</example>
        /// </summary>
        public string appId;
        /// <summary>
        /// Specifies the application version.
        /// <remarks>Optional</remarks>
        /// <example>1.2</example>
        /// </summary>
        public string appVersion;
        /// <summary>
        /// Specifies the screen resolution
        /// <remarks>Optional</remarks>
        /// <example>800x600</example>
        /// </summary> 
        public string ScreenResolution;
        /// <summary>
        /// Specifies the viewable area of the browser / device.
        /// <remarks>Optional</remarks>
        /// <example>123x456</example>
        /// </summary> 
        public string ViewportSize;
        /// <summary>
        /// This anonymously identifies a particular user, device, or browser instance. 
        /// For the web, this is generally stored as a first-party cookie with a two-year expiration. For mobile apps, this is randomly generated for each particular instance of an application install. 
        /// The value of this field should be a random UUID (version 4) as described in http://www.ietf.org/rfc/rfc4122.txt
        /// <remarks>Required for all hit types</remarks>
        /// <example>35009a79-1a05-49d7-b876-2b884d0f825b</example>
        /// </summary>
        public string ClientId;
        /// <summary>
        /// This is intended to be a known identifier for a user provided by the site owner/tracking library user. 
        /// It may not itself be PII (personally identifiable information). 
        /// The value should never be persisted in GA cookies or other Analytics provided storage.
        /// <remarks>Optional</remarks>
        /// <example>as8eknlll</example>
        /// </summary> 
        public string UserId;

        public static SimpleTracker Instance {
            get {
                if (instance == null) {
#if DEBUG
                    throw new Exception ("Tracker not configured");
#endif
                }
                return instance;
            }
        }

        public SimpleTracker (string trackingAccount,
                          string trackingDomain,
                          string Hostname,
                          string OsPlatform,
                          string OsVersion,
                          string OsVersionString)
        : base (trackingAccount, trackingDomain, new AnalyticsSession (), new SimpleTrackerEnvironment (Hostname, OsPlatform, OsVersion, OsVersionString))
        {
        }

        public SimpleTracker (string trackingAccount, string trackingDomain, ITrackerEnvironment trackerEnvironment)
            : base (trackingAccount, trackingDomain, new AnalyticsSession (), trackerEnvironment)
        {
        }

    static public void Configure (string trackingAccount,
                                  string trackingDomain,
                                  string Hostname,
                                  string OsPlatform,
                                  string OsVersion,
                                  string OsVersionString)
    {
      instance = new SimpleTracker (trackingAccount,
                                    trackingDomain,
                                    Hostname,
                                    OsPlatform,
                                    OsVersion,
                                    OsVersionString);
    }


        public async Task<TrackingResult> TrackScreenviewAsync (string screenName)
        {
            var screenviewParamenters = new ScreenviewTracking {
                ApplicationName = appName,
                ApplicationId = appId,
                ApplicationVersion = appVersion,
                ScreenName = screenName,
                CacheBuster = AnalyticsSession.GenerateCacheBuster (),
                ViewportSize = ViewportSize,
                ScreenResolution = ScreenResolution,
                ClientId = ClientId,
                UserId = UserId
            };
            return await TrackAsync (screenviewParamenters);
        }

        public async Task<TrackingResult> TrackPageViewAsync (string pageTitle, string pageUrl)
        {
            var pageViewParameters = new PageView {
                DocumentTitle = pageTitle,
                DocumentLocationUrl = pageUrl,
                CacheBuster = AnalyticsSession.GenerateCacheBuster (),
                ViewportSize = ViewportSize,
                ScreenResolution = ScreenResolution,
                ClientId = ClientId,
                UserId = UserId
            };

            return await TrackAsync (pageViewParameters);
        }

    }
}