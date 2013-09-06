namespace GoogleAnalyticsTracker
{
    public interface ITrackTransactionItems
    {
        void TrackTransactionItem(string orderId, string productId, string productName, string productVariation,
						string productPrice, string quantity, string hostname = null, string userAgent = null, string characterSet = null, string language = null);
    }
}