namespace GoogleAnalyticsTracker
{
    public interface ITrackTransactionItems
    {
        void TrackTransactionItem(string orderId, string productId, string productName, string productVariation,
            string productPrice, string quantity);
    }
}