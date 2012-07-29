using System;
using System.Globalization;
using System.IO.IsolatedStorage;

namespace GoogleAnalyticsTracker
{
    public class WindowsPhoneAnalyticsSession : GoogleAnalyticsTracker.IAnalyticsSession
    {

        private string sessionId = null;
        private string cookie = null;
        private IsolatedStorageSettings Settings = IsolatedStorageSettings.ApplicationSettings;

        private string GetUniqueVisitorID()
        {
            string Str = null;

            if (!Settings.Contains("GoogleAnalytics.UniqueID"))
            {
                Random random = new Random();
                Str = string.Format("{0}{1}", random.Next(100000000, 999999999), "00145214523");
                Settings.Add("GoogleAnalytics.UniqueID", Str);
            }
            else
            {
                Str = (string)Settings["GoogleAnalytics.UniqueID"];
            }
            return Str;
        }

        private int GetFirstVisitTime()
        {
            int Res = 0;
            if (!Settings.Contains("GoogleAnalytics.FirstVisitTime"))
            {
                Res = GetUnixTime();
                Settings.Add("GoogleAnalytics.FirstVisitTime", Res);
            }
            else
            {
                Res = (int)Settings["GoogleAnalytics.FirstVisitTime"];
            }
            return Res;
        }

        private int GetPreviousVisitTime()
        {
            int Res = 0;
            if (!Settings.Contains("GoogleAnalytics.PrevVisitTime"))
            {
                Res = GetUnixTime();
                Settings.Add("GoogleAnalytics.PrevVisitTime", Res);
            }
            else
            {
                Res = (int)Settings["GoogleAnalytics.PrevVisitTime"];
                Settings["GoogleAnalytics.PrevVisitTime"] = GetUnixTime();
            }
            return Res;
        }


        private int GetCurrentVisitTime()
        {
            return GetUnixTime();
        }

        private int GetSessionCount()
        {
            int Res = 0;
            if (!Settings.Contains("GoogleAnalytics.SessionCount"))
            {
                Res = 1;
                Settings.Add("GoogleAnalytics.SessionCount", Res);
            }
            else
            {
                Res = (int)Settings["GoogleAnalytics.SessionCount"];
                Res += 1;
                Settings["GoogleAnalytics.SessionCount"] = Res;
            }
            return Res;
        }

        private static int GetUnixTime()
        {
            return GetUnixTime(DateTime.UtcNow);
        }
        private static int GetUnixTime(System.DateTime UtcDateTime)
        {
            System.DateTime Epoch = new System.DateTime(1970, 1, 1);
            return (int)UtcDateTime.Subtract(Epoch).TotalSeconds;
        }

        public string GenerateCookieValue()
        {
            //__utma cookie syntax: domain-hash.unique-id.FirstVisitTime.PreviousVisitTime.CurrentVisitTime.session-counter
            if (cookie == null)
            {
                string UniqueVisitorID = GetUniqueVisitorID();
                cookie = string.Format("__utma=1.{0}.{1}.{2}.{3}.{4};+__utmz=1.{3}.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none);",
                                        UniqueVisitorID, GetFirstVisitTime(), GetPreviousVisitTime(), GetCurrentVisitTime(), GetSessionCount());
            }
            return cookie;
        }

        public string GenerateSessionId()
        {
            if (sessionId == null)
            {
                Random random = new Random();
                sessionId = random.Next(100000000, 999999999).ToString(CultureInfo.InvariantCulture);
            }
            return sessionId;
        }
    }
}