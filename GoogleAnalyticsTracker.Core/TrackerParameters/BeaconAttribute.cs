using System;

namespace GoogleAnalyticsTracker.Core.TrackerParameters
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Enum, AllowMultiple = false, Inherited = true)]
    public class BeaconAttribute : Attribute
    {
        /// <summary>
        /// Beacon name
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Indicates that beacon is a Enum and must use its value.
        /// </summary>
        public bool IsEnumByValueBased { get; private set; }
        /// <summary>
        /// Indicates whether is required or not.
        /// </summary>
        public bool IsRequired { get; private set; }        

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="name">Beacon name</param>
        /// <param name="isRequried">Beacon is required?</param>
        /// <param name="isEnumByValueBased">Beacon is a enum that muust be get by value?</param>
        public BeaconAttribute(string name, bool isRequried = false, bool isEnumByValueBased = false)
        {
            Name = name;
            IsEnumByValueBased = isEnumByValueBased;
            IsRequired = isRequried;
        }
    }
}