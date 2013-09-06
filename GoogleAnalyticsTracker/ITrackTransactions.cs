namespace GoogleAnalyticsTracker
{
    public interface ITrackTransactions
    {
        void TrackTransaction(string orderId, string storeName, string total, string tax, string shipping, string city,
						string region, string country, string hostname = null, string userAgent = null, string characterSet = null, string language = null);
    }
}