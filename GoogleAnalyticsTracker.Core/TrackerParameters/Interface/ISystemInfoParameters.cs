using JetBrains.Annotations;

namespace GoogleAnalyticsTracker.Core.TrackerParameters.Interface
{
    [PublicAPI]
    public interface ISystemInfoParameters
    {
        /// <summary>
        /// Specifies the screen resolution
        /// <remarks>Optional</remarks>
        /// <example>800x600</example>
        /// </summary>        
        string ScreenResolution { get; set; }

        /// <summary>
        /// Specifies the viewable area of the browser / device.
        /// <remarks>Optional</remarks>
        /// <example>123x456</example>
        /// </summary>        
        string ViewportSize { get; set; }

        /// <summary>
        /// Specifies the character set used to encode the page / document.
        /// <remarks>Optional</remarks>
        /// <example>UTF-8</example>
        /// </summary>        
        string DocumentEncoding { get; set; }

        /// <summary>
        /// Specifies the screen color depth.
        /// <remarks>Optional</remarks>
        /// <example>24-bits</example>
        /// </summary>        
        string ScreenColors { get; set; }

        /// <summary>
        /// Specifies the language.
        /// <remarks>Optional</remarks>
        /// <example>en-us</example>        
        /// </summary>        
        string UserLanguage { get; set; }

        /// <summary>
        /// Specifies whether Java was enabled.
        /// <remarks>Optional</remarks>
        /// <example>GoogleBoolean.True</example>
        /// </summary>        
        bool? JavaEnabled { get; set; }

        /// <summary>
        /// Specifies the flash version.
        /// <remarks>Optional</remarks>
        /// <example>10 1 r103</example>
        /// </summary>        
        string FlashVersion { get; set; }
    }
}