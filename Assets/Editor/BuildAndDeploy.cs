using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.Diagnostics;
using System.IO;

public class BuildAndDeploy
{
    [MenuItem("Build/Build and Deploy WebGL")]
    public static void BuildAndPush()
    {
        string buildPath = "Builds/WebGL";

        BuildReport report = BuildPipeline.BuildPlayer(
            EditorBuildSettings.scenes,
            buildPath,
            BuildTarget.WebGL,
            BuildOptions.None
        );

        if (report.summary.result != BuildResult.Succeeded)
        {
            UnityEngine.Debug.LogError("Build failed. Deploy skipped.");
            return;
        }

        UnityEngine.Debug.Log("Build complete.");

        RunDeployScript();
    }

    static void RunDeployScript()
    {
        try
        {
            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            string deployScript = Path.Combine(projectRoot, "Assets", "Editor", "deploy.bat");

            if (!File.Exists(deployScript))
            {
                UnityEngine.Debug.LogError("Deploy script not found: " + deployScript);
                return;
            }

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = "/d /c \"\"" + deployScript + "\"\"",
                WorkingDirectory = projectRoot,
                CreateNoWindow = false,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = psi;
                process.Start();

                string stdout = process.StandardOutput.ReadToEnd();
                string stderr = process.StandardError.ReadToEnd();

                process.WaitForExit();

                UnityEngine.Debug.Log("Deploy stdout:\n" + stdout);

                if (!string.IsNullOrWhiteSpace(stderr))
                    UnityEngine.Debug.LogError("Deploy stderr:\n" + stderr);

                UnityEngine.Debug.Log("Deploy exit code: " + process.ExitCode);
            }
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError("Deploy failed: " + e);
        }
    }
}
