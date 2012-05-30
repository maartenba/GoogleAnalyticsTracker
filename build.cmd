@echo Off
set config=%1
if "%config%" == "" (
   set config=Debug
)
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild GoogleAnalyticsTracker\GoogleAnalyticsTracker.csproj /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild GoogleAnalyticsTracker.WindowsPhone\GoogleAnalyticsTracker.WindowsPhone.csproj /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

mkdir Build
mkdir Build\net40
copy GoogleAnalyticsTracker\bin\%Config%\*.nupkg Build\net40
copy GoogleAnalyticsTracker\bin\%Config%\*.dll Build\net40
copy GoogleAnalyticsTracker\bin\%Config%\*.pdb Build\net40

mkdir Build\sl4-wp71
copy GoogleAnalyticsTracker.WindowsPhone\bin\%Config%\*.nupkg Build\sl4-wp71
copy GoogleAnalyticsTracker.WindowsPhone\bin\%Config%\*.dll Build\sl4-wp71
copy GoogleAnalyticsTracker.WindowsPhone\bin\%Config%\*.pdb Build\sl4-wp71
