using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("GoogleAnalyticsTracker.WindowsPhone")]
[assembly: AssemblyDescription("GoogleAnalyticsTracker - A C# library for tracking Google Analytics.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Maarten Balliauw and Henrik Sozzi")]
[assembly: AssemblyProduct("GoogleAnalyticsTracker.WindowsPhone")]
[assembly: AssemblyCopyright("Copyright © Maarten Balliauw and Henrik Sozzi 2012")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("108221af-d9cb-4bf3-8420-036e869c1e56")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.1.1.0")]
[assembly: AssemblyFileVersion("1.1.1.0")]

/*
 * CHANGELOG
 * 
 * 1.1.0.0 - Henrik Sozzi (henrik.sozzi@gmail.com) - 2012-07-28
 *  - Added WindowsPhoneAnalyticSession and used by default on WindowsPhone project
 *  - Added screen resolution to the gif url parameters.
 *  - Refactored UserAgent String with a one comparable to the one that the Android SDK is sending to google analytics
 * 1.1.1.0 - Henrik Sozzi (henrik.sozzi@gmail.com) - 2012-07-29
 *  - Refactored UserString to be the same that IE9 is sending when viewing websites. In that way now Google Analytics
 *    is now recognizing phone manufacturer and device model name!
*/