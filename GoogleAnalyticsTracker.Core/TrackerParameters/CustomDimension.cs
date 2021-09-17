using System;
using GoogleAnalyticsTracker.Core.TrackerParameters.Interface;
using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters
{
    [PublicAPI]
    public class CustomDimension : ICustomDimension
    {
        public CustomDimension(int id, string value)
        {
            Id = id;
            Value = value;
        }

        private int _id = 1;
        public int Id
        {
            get => _id;
            set
            {
                if (value is < 1 or > 200)
                {
                    throw new ArgumentOutOfRangeException(nameof(Id), "Must be between 1 and 200 inclusive");
                }

                _id = value;
            }
        }

        public string Name => $"cd{_id}";

        private string _value = string.Empty;
        public string Value
        {
            get => _value;
            set => _value = value.Length > 149 ? value.Substring(0, 149) : value;
        }
    }
}
