@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)

REM Package restore
tools\nuget.exe restore GoogleAnalyticsTracker.sln -OutputDirectory %cd%\packages -NonInteractive

REM Build
"%programfiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe" GoogleAnalyticsTracker.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

REM Package
mkdir Build
mkdir Build\nuget
tools\nuget.exe pack "GoogleAnalyticsTracker.Core\GoogleAnalyticsTracker.Core.csproj" -symbols -o Build\nuget -p Configuration=%config% %version%
tools\nuget.exe pack "GoogleAnalyticsTracker.Simple\GoogleAnalyticsTracker.Simple.csproj" -symbols -o Build\nuget -p Configuration=%config% %version%
tools\nuget.exe pack "GoogleAnalyticsTracker.Nancy\GoogleAnalyticsTracker.Nancy.csproj" -symbols -o Build\nuget -p Configuration=%config% %version%
tools\nuget.exe pack "GoogleAnalyticsTracker.MVC4\GoogleAnalyticsTracker.MVC4.csproj" -symbols -o Build\nuget -p Configuration=%config% %version%
tools\nuget.exe pack "GoogleAnalyticsTracker.MVC5\GoogleAnalyticsTracker.MVC5.csproj" -symbols -o Build\nuget -p Configuration=%config% %version%
tools\nuget.exe pack "GoogleAnalyticsTracker.WebAPI\GoogleAnalyticsTracker.WebAPI.csproj" -symbols -o Build\nuget -p Configuration=%config% %version%
tools\nuget.exe pack "GoogleAnalyticsTracker.WebAPI2\GoogleAnalyticsTracker.WebAPI2.csproj" -symbols -o Build\nuget -p Configuration=%config% %version%
tools\nuget.exe pack "GoogleAnalyticsTracker.Owin\GoogleAnalyticsTracker.Owin.csproj" -symbols -o Build\nuget -p Configuration=%config% %version%
tools\nuget.exe pack "GoogleAnalyticsTracker.Nancy\GoogleAnalyticsTracker.Nancy.csproj" -symbols -o Build\nuget -p Configuration=%config% %version%
tools\nuget.exe pack "GoogleAnalyticsTracker.RT\GoogleAnalyticsTracker.RT.csproj" -symbols -o Build\nuget -p Configuration=%config% %version%
tools\nuget.exe pack "GoogleAnalyticsTracker.WP8\GoogleAnalyticsTracker.WP8.csproj" -symbols -o Build\nuget -p Configuration=%config% %version%
tools\nuget.exe pack "GoogleAnalyticsTracker.Owin\GoogleAnalyticsTracker.Owin.csproj" -symbols -o Build\nuget -p Configuration=%config% %version%

REM Plain assemblies
mkdir Build\assemblies
copy GoogleAnalyticsTracker.Core\bin\%config%\Google*.dll Build\assemblies
copy GoogleAnalyticsTracker.Core\bin\%config%\Google*.pdb Build\assemblies
copy GoogleAnalyticsTracker.Simple\bin\%config%\Google*.dll Build\assemblies
copy GoogleAnalyticsTracker.Simple\bin\%config%\Google*.pdb Build\assemblies
copy GoogleAnalyticsTracker.Nancy\bin\%config%\Google*.dll Build\assemblies
copy GoogleAnalyticsTracker.Nancy\bin\%config%\Google*.pdb Build\assemblies
copy GoogleAnalyticsTracker.MVC4\bin\%config%\Google*.dll Build\assemblies
copy GoogleAnalyticsTracker.MVC4\bin\%config%\Google*.pdb Build\assemblies
copy GoogleAnalyticsTracker.MVC5\bin\%config%\Google*.dll Build\assemblies
copy GoogleAnalyticsTracker.MVC5\bin\%config%\Google*.pdb Build\assemblies
copy GoogleAnalyticsTracker.WebAPI\bin\%config%\Google*.dll Build\assemblies
copy GoogleAnalyticsTracker.WebAPI\bin\%config%\Google*.pdb Build\assemblies
copy GoogleAnalyticsTracker.WebAPI2\bin\%config%\Google*.dll Build\assemblies
copy GoogleAnalyticsTracker.WebAPI2\bin\%config%\Google*.pdb Build\assemblies
copy GoogleAnalyticsTracker.Owin\bin\%config%\Google*.dll Build\assemblies
copy GoogleAnalyticsTracker.Owin\bin\%config%\Google*.pdb Build\assemblies
copy GoogleAnalyticsTracker.Nancy\bin\%config%\Google*.dll Build\assemblies
copy GoogleAnalyticsTracker.Nancy\bin\%config%\Google*.pdb Build\assemblies
copy GoogleAnalyticsTracker.RT\bin\%config%\Google*.dll Build\assemblies
copy GoogleAnalyticsTracker.RT\bin\%config%\Google*.pdb Build\assemblies
copy GoogleAnalyticsTracker.WP8\bin\%config%\Google*.dll Build\assemblies
copy GoogleAnalyticsTracker.WP8\bin\%config%\Google*.pdb Build\assemblies
copy GoogleAnalyticsTracker.Owin\bin\%config%\Google*.dll Build\assemblies
copy GoogleAnalyticsTracker.Owin\bin\%config%\Google*.pdb Build\assemblies

