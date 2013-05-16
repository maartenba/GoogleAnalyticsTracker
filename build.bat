@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild GoogleAnalyticsTracker.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

mkdir Build
mkdir Build\net40
.nuget\nuget.exe pack "GoogleAnalyticsTracker\GoogleAnalyticsTracker.csproj" -symbols -o Build\net40 -p SolutionDir=%cd% -p Configuration=%config% %version%
copy GoogleAnalyticsTracker\bin\%config%\*.dll Build\net40
copy GoogleAnalyticsTracker\bin\%config%\*.pdb Build\net40

mkdir Build\sl4-wp71
.nuget\nuget.exe pack "GoogleAnalyticsTracker.WP7\GoogleAnalyticsTracker.WP7.csproj" -symbols -o Build\sl4-wp71 -p SolutionDir=%cd% -p Configuration=%config% %version%
copy GoogleAnalyticsTracker.WP7\bin\%config%\*.dll Build\sl4-wp71
copy GoogleAnalyticsTracker.WP7\bin\%config%\*.pdb Build\sl4-wp71

mkdir Build\wp8
.nuget\nuget.exe pack "GoogleAnalyticsTracker.WP8\GoogleAnalyticsTracker.WP8.csproj" -symbols -o Build\wp8 -p SolutionDir=%cd% -p Configuration=%config% %version%
copy GoogleAnalyticsTracker.WP8\bin\%config%\*.dll Build\wp8
copy GoogleAnalyticsTracker.WP8\bin\%config%\*.pdb Build\wp8
