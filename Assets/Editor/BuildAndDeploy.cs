using UnityEditor;
using UnityEngine;
using System.Diagnostics;

public class BuildAndDeploy
{
    [MenuItem("Build/Build and Deploy WebGL")]
    public static void BuildAndPush()
    {
        string buildPath = "Builds/WebGL";

        // Build WebGL
        BuildPipeline.BuildPlayer(
            EditorBuildSettings.scenes,
            buildPath,
            BuildTarget.WebGL,
            BuildOptions.None
        );

        UnityEngine.Debug.Log("Build complete!");

        // Run deploy script
        RunDeployScript();
    }

    static void RunDeployScript()
    {
        try
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "cmd.exe";
            psi.Arguments = "deploy.sh";
            psi.WorkingDirectory = Application.dataPath + "/..";
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;

            Process process = Process.Start(psi);
            process.WaitForExit();

            UnityEngine.Debug.Log("Deploy complete!");
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError("Deploy failed: " + e.Message);
        }
    }
}