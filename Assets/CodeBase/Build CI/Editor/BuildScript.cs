using UnityEditor;
using System.Linq;

namespace CodeBase.Build_CI.Editor
{
    public static class BuildScript
    {
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

            UnityEngine.Debug.Log("Build completed: " + buildPath);
        }
    }
}