@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild GoogleAnalyticsTracker.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

mkdir Build
mkdir Build\net40
.nuget\nuget.exe pack "GoogleAnalyticsTracker\GoogleAnalyticsTracker.csproj" -symbols -o Build\net40 -p SolutionDir=%cd%
copy GoogleAnalyticsTracker\bin\%Config%\*.dll Build\net40
copy GoogleAnalyticsTracker\bin\%Config%\*.pdb Build\net40

mkdir Build\sl4-wp71
.nuget\nuget.exe pack "GoogleAnalyticsTracker.WP7\GoogleAnalyticsTracker.WP7.csproj" -symbols -o Build\sl4-wp71 -p SolutionDir=%cd%
copy GoogleAnalyticsTracker.WP7\bin\%Config%\*.dll Build\sl4-wp71
copy GoogleAnalyticsTracker.WP7\bin\%Config%\*.pdb Build\sl4-wp71

mkdir Build\wp8
.nuget\nuget.exe pack "GoogleAnalyticsTracker.WP8\GoogleAnalyticsTracker.WP8.csproj" -symbols -o Build\wp8 -p SolutionDir=%cd%
copy GoogleAnalyticsTracker.WP8\bin\%Config%\*.dll Build\wp8
copy GoogleAnalyticsTracker.WP8\bin\%Config%\*.pdb Build\wp8
