using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

public static class Extensions
{
    public static string Until(this string source, string marker)
    {
        if (string.IsNullOrEmpty(source)) return source;

        var length = source.IndexOf(marker, StringComparison.OrdinalIgnoreCase);

        if (length < 0 || length > source.Length) return source;
        
        return source.Substring(0, length);
    }
}

[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
// ReSharper disable once ClassNeverInstantiated.Global
class Build : NukeBuild
{
    public static int Main () => Execute<Build>(x => x.Package);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;

    AbsolutePath SourceDirectory => RootDirectory;
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    static string AssemblyVersion = Environment.GetEnvironmentVariable("VersionFormat")?.Replace("{0}", "0")?.Until("-") ?? "0.0.1";
    static string PackageVersion = Environment.GetEnvironmentVariable("PackageVersion") ?? AssemblyVersion + "-dev";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(ArtifactsDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(_ => _
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(_ => _
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(AssemblyVersion)
                .SetFileVersion(AssemblyVersion)
                .SetInformationalVersion(PackageVersion)
                .EnableNoRestore());
        });

    Target Package => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            foreach (var project in Solution.AllProjects.Where(p => p.GetProperty<bool>("GeneratePackageOnBuild")).ToList())
            {
                DotNetPack(_ => _
                    .SetProject(project)
                    .EnableIncludeSource()
                    .EnableIncludeSymbols()
                    .EnableNoRestore()
                    .EnableNoBuild()
                    .SetVersion(PackageVersion)
                    .SetOutputDirectory(ArtifactsDirectory));
            }
        });
}