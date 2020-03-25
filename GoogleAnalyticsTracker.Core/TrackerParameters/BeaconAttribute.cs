using System;
using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters
{
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum)]
    public class BeaconAttribute : Attribute
    {
        /// <summary>
        /// Beacon name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Indicates that beacon is a Enum and must use its value.
        /// </summary>
        public bool IsEnumByValueBased { get; }

        /// <summary>
        /// Indicates whether is required or not.
        /// </summary>
        public bool IsRequired { get; }        

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="name">Beacon name</param>
        /// <param name="isRequired">Beacon is required?</param>
        /// <param name="isEnumByValueBased">Beacon is a enum that must be get by value?</param>
        public BeaconAttribute(string name, bool isRequired = false, bool isEnumByValueBased = false)
        {
            Name = name;
            IsEnumByValueBased = isEnumByValueBased;
            IsRequired = isRequired;
        }
    }
}