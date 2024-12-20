using UnityEditor;
using System.IO;
using System.Linq;

namespace CodeBase.Build_CI.Editor
{
    public static class BuildScript
    {
        private const string ManifestPath = "Packages/manifest.json";
        private const string ManifestLinuxServerPath = "Packages/manifestLinuxServer.json";

        public static void BuildLinuxServer()
        {
            // SwitchManifest(ManifestLinuxServerPath);

            string[] scenesToBuild = EditorBuildSettings.scenes
                .Where(scene => scene.enabled)
                .Select(scene => scene.path)
                .ToArray();

            string buildPath = "Builds/LinuxServer";

            BuildPipeline.BuildPlayer(
                scenesToBuild,
                buildPath,
                BuildTarget.StandaloneLinux64,
                BuildOptions.EnableHeadlessMode
            );

            UnityEngine.Debug.Log("Build completed: " + buildPath);
        }

        /*private static void SwitchManifest(string sourcePath)
        {
            if (!File.Exists(sourcePath))
            {
                UnityEngine.Debug.LogError($"Manifest file not found: {sourcePath}");
                return;
            }

            if (!File.Exists(ManifestPath + ".backup"))
                File.Copy(ManifestPath, ManifestPath + ".backup", overwrite: true);

            File.Copy(sourcePath, ManifestPath, overwrite: true);
            UnityEngine.Debug.Log($"Switched manifest to {sourcePath}");
        }*/

        private static void RestoreOriginalManifest()
        {
            string backupPath = ManifestPath + ".backup";
            if (File.Exists(backupPath))
            {
                File.Copy(backupPath, ManifestPath, overwrite: true);
                File.Delete(backupPath);
                UnityEngine.Debug.Log("Restored original manifest.");
            }
            else
            {
                UnityEngine.Debug.LogWarning("Backup manifest not found. Original manifest not restored.");
            }
        }
    }
}