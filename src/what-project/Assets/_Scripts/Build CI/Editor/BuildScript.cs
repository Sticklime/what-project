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
        }
    }
}