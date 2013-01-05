@echo Off
set config=%1
if "%config%" == "" (
   set config=Debug
)
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild GoogleAnalyticsTracker\GoogleAnalyticsTracker.csproj /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild GoogleAnalyticsTracker.WP7\GoogleAnalyticsTracker.WP7.csproj /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild GoogleAnalyticsTracker.WP8\GoogleAnalyticsTracker.WP8.csproj /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

mkdir Build
mkdir Build\net40
copy GoogleAnalyticsTracker\bin\%Config%\*.nupkg Build\net40
copy GoogleAnalyticsTracker\bin\%Config%\*.dll Build\net40
copy GoogleAnalyticsTracker\bin\%Config%\*.pdb Build\net40

mkdir Build\sl4-wp71
copy GoogleAnalyticsTracker.WP7\bin\%Config%\*.nupkg Build\sl4-wp71
copy GoogleAnalyticsTracker.WP7\bin\%Config%\*.dll Build\sl4-wp71
copy GoogleAnalyticsTracker.WP7\bin\%Config%\*.pdb Build\sl4-wp71

mkdir Build\wp8
copy GoogleAnalyticsTracker.WP8\bin\%Config%\*.nupkg Build\wp8
copy GoogleAnalyticsTracker.WP8\bin\%Config%\*.dll Build\wp8
copy GoogleAnalyticsTracker.WP8\bin\%Config%\*.pdb Build\wp8
