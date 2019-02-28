#tool nuget:?package=xunit.runner.console&version=2.4.1
#addin nuget:?package=Cake.Git&version=0.19.0

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var projectName = "TemplateName";
var solution = "./" + projectName + ".sln";

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var branch = Argument("branch", GitBranchCurrent("./").FriendlyName);

var testProjects = new List<Tuple<string, string[]>>
  {
    new Tuple<string, string[]>("TemplateName.Test", new [] { "netcoreapp2.2" }),
  };

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
  .Does(() =>
  {
    Information("Current Branch is:" + branch);
    CleanDirectories("./src/**/bin");
    CleanDirectories("./src/**/obj");
    CleanDirectories("./test/**/bin");
    CleanDirectories("./test/**/obj");
  });

Task("Restore-NuGet-Packages")
  .IsDependentOn("Clean")
  .Does(() =>
  {
    DotNetCoreRestore(solution);
  });

Task("Build")
  .IsDependentOn("Restore-NuGet-Packages")
  .Does(() =>
  {
    var dotNetCoreMSBuildSettings = new DotNetCoreMSBuildSettings() { MaxCpuCount = 0 };
    DotNetCoreBuild(solution, new DotNetCoreBuildSettings() { Configuration = configuration, MSBuildSettings = dotNetCoreMSBuildSettings, NoRestore = true });
  });

Task("Run-Unit-Tests")
  .IsDependentOn("Build")
  .DoesForEach(testProjects, testProject =>
  {
    foreach (string targetFramework in testProject.Item2)
    {
      Information($"Test execution started for target frameowork: {targetFramework}...");
      var testProj = GetFiles($"./test/**/*{testProject.Item1}.csproj").First();
      DotNetCoreTest(testProj.FullPath, new DotNetCoreTestSettings { Configuration = configuration, Framework = targetFramework });
    }
  })
  .DeferOnError();

Task("Publish")
  .IsDependentOn("Run-Unit-Tests")
  .Does(() =>
  {

  });


//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
  .IsDependentOn("Build")
  .IsDependentOn("Run-Unit-Tests")
  .IsDependentOn("Publish");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
