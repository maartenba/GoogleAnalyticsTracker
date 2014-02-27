namespace GoogleAnalyticsTracker.Core
{
    public static class BeaconParameter
    {
        // values come from https://developers.google.com/analytics/resources/concepts/gaConceptsTrackingOverview#gifParameters

        public const string RequestType = "utmt";
        public const string ExtensibleParameter = "utme";

        public const string PageTitle = "utmdt";
        public const string PageUrl = "utmp";

        public const string HostName = "utmhn";

        public static class Browser
        {
            public const string Language = "utmtci";
            public const string LanguageEncoding = "utmcs";
            public const string FlashVersion = "utmfl";
            public const string ReferralUrl = "utmr";
            public const string JavaEnabled = "utmje";
            public const string ScreenColorDepth = "utmsc";
            public const string ScreenResolution = "utmsr";
        }

        public static class Transaction
        {
            public const string Affiliation = "utmtst";
            public const string OrderId = "utmtid";
            public const string ProductCode = "utmipc";
            public const string ProductName = "utmipn";
            public const string ProductUnitPrice = "utmipr";
            public const string ProductQuantity = "utmiqt";
            public const string ProductVariation = "utmiva";

            public const string BillingCity = "utmtci";
            public const string BillingCountry = "utmtco";
            public const string BillingRegion = "utmtrg";

            public const string ShippingCost = "utmtsp";
            public const string OrderTotal = "utmtto";
            public const string OrderTax = "utmttx";
        }
    }
}