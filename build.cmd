@echo Off
set config=%1
if "%config%" == "" (
   set config=Debug
)
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild GoogleAnalyticsTracker\GoogleAnalyticsTracker.csproj /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

mkdir Build
copy GoogleAnalyticsTracker\bin\%Config%\*.nupkg Build
copy GoogleAnalyticsTracker\bin\%Config%\*.dll Build
copy GoogleAnalyticsTracker\bin\%Config%\*.pdb Build