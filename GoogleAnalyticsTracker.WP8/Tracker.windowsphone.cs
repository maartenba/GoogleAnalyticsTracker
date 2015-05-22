using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace GoogleAnalyticsTracker
{
    public partial class Tracker
    {
#if WINDOWS_PHONE
        public Tracker(string trackingAccount, string trackingDomain)
            : this(trackingAccount, trackingDomain, new WindowsPhoneAnalyticsSession())
        {
        }
#endif
    }
}
