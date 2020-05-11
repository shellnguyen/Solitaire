using System;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class Builder
{
    public static string[] scenes = { "Assets/Scenes/LoadingScene.unity", "Assets/Scenes/StartScene.unity", "Assets/Scenes/MainScene.unity", "Assets/Scenes/UIScene.unity" };
    public const string RELEASE_FLAG = "RELEASE_BUILD";
    public const string DEV_FLAG = "DEV_BUILD";

    private static void AddFlag(string flag)
    {
        // Get defines.
        BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);

        // Append only if not defined already.
        if (defines.Contains(flag))
        {
            Debug.LogWarning("Selected build target (" + EditorUserBuildSettings.activeBuildTarget.ToString() + ") already contains <b>" + flag + "</b> <i>Scripting Define Symbol</i>.");
            return;
        }

        // Append.
        PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, (defines + ";" + flag));
        Debug.LogWarning("<b>" + flag + "</b> added to <i>Scripting Define Symbols</i> for selected build target (" + EditorUserBuildSettings.activeBuildTarget.ToString() + ").");
    }

    public static void AndroidRelease()
    {
        AddFlag(RELEASE_FLAG);

        BuildPlayerOptions options = new BuildPlayerOptions();
        options.scenes = scenes;
        options.target = BuildTarget.Android;
        options.locationPathName = "../build/AndroidRelease_" + PlayerSettings.bundleVersion + "/"  + PlayerSettings.productName;
        options.options = BuildOptions.CompressWithLz4HC;
        EditorUserBuildSettings.buildAppBundle = true;

        BuildReport report = BuildPipeline.BuildPlayer(options);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build AndroidRelease succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build AndroidRelease failed");
        }
    }

    public static void AndroidDev()
    {
        AddFlag(DEV_FLAG);

        BuildPlayerOptions options = new BuildPlayerOptions();
        options.scenes = scenes;
        options.target = BuildTarget.Android;
        options.locationPathName = "../build/AndroidDev_" + PlayerSettings.bundleVersion + "/" + PlayerSettings.productName;
        options.options = BuildOptions.CompressWithLz4HC | BuildOptions.Development | BuildOptions.AllowDebugging;

        BuildReport report = BuildPipeline.BuildPlayer(options);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build AndroidDev succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build AndroidDev failed");
        }
    }

    public static void WebGLDev()
    {
        AddFlag(DEV_FLAG);

        BuildPlayerOptions options = new BuildPlayerOptions();
        options.scenes = scenes;
        options.target = BuildTarget.WebGL;
        options.locationPathName = "../build/WebGLDev/" + PlayerSettings.productName;
        options.options = BuildOptions.Development;

        BuildReport report = BuildPipeline.BuildPlayer(options);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build WebGLDev succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build WebGLDev failed");
        }
    }

    public static void WebGLRelease()
    {
        AddFlag(RELEASE_FLAG);

        BuildPlayerOptions options = new BuildPlayerOptions();
        options.scenes = scenes;
        options.target = BuildTarget.WebGL;
        options.locationPathName = "../build/WebGLRelease/" + PlayerSettings.productName;
        options.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(options);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build WebGLRelease succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build WebGLRelease failed");
        }
    }

    public static void IosRelease()
    {
        AddFlag(RELEASE_FLAG);

        BuildPlayerOptions options = new BuildPlayerOptions();
        options.scenes = scenes;
        options.target = BuildTarget.Android;
        options.locationPathName = "../build/iOSRelease_" + PlayerSettings.bundleVersion + "/" + PlayerSettings.productName;
        options.options = BuildOptions.CompressWithLz4HC;
        EditorUserBuildSettings.iOSBuildConfigType = iOSBuildType.Release;

        BuildReport report = BuildPipeline.BuildPlayer(options);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build IOSRelease succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build IOSRelease failed");
        }
    }

    public static void IosDev()
    {
        AddFlag(DEV_FLAG);

        BuildPlayerOptions options = new BuildPlayerOptions();
        options.scenes = scenes;
        options.target = BuildTarget.iOS;
        options.locationPathName = "../build/iOSDev_" + PlayerSettings.bundleVersion + "/" + PlayerSettings.productName;
        options.options = BuildOptions.Development | BuildOptions.AllowDebugging;
        EditorUserBuildSettings.iOSBuildConfigType = iOSBuildType.Debug;

        BuildReport report = BuildPipeline.BuildPlayer(options);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build IOSDev succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build IOSDev failed");
        }
    }

    public static void WindowsRelease()
    {
        AddFlag(RELEASE_FLAG);

        BuildPlayerOptions options = new BuildPlayerOptions();
        options.scenes = scenes;
        options.target = BuildTarget.StandaloneWindows64;
        options.locationPathName = "../build/WindowsRelease/" + PlayerSettings.productName;
        options.options = BuildOptions.CompressWithLz4HC;

        BuildReport report = BuildPipeline.BuildPlayer(options);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build WindowsRelease succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build WindowsRelease failed");
        }
    }

    public static void WindowsDev()
    {
        AddFlag(DEV_FLAG);

        BuildPlayerOptions options = new BuildPlayerOptions();
        options.scenes = scenes;
        options.target = BuildTarget.StandaloneWindows64;
        options.locationPathName = "../build/WindowsDev/" + PlayerSettings.productName;
        options.options = BuildOptions.Development | BuildOptions.AllowDebugging;

        BuildReport report = BuildPipeline.BuildPlayer(options);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build WindowsDev succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build WindowsDev failed");
        }
    }
}
