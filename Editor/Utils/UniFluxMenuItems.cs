using UnityEditor;
using UnityEngine;

namespace UniFlux.Editor
{
    internal static class UniFluxMenuItems
    {
        [MenuItem("UniFlux/Open Debugger", priority = 0)] private static void Openw_Window_UniFluxDebuggerWindow()
        {
            EditorWindow.GetWindow<UniFluxDebuggerWindow>(false, "UniFlux Debugger", true);
        }
        [MenuItem("UniFlux/Open Generator Key", priority = 1)] public static void GenerateExtensionType()
        {
            Rect centerRect = new Rect(
                Screen.width / 2 - 100, 
                Screen.height / 2 - 100, 
                400,
                150
            );
            UniFluxExtensionTypeWindow window = (UniFluxExtensionTypeWindow)EditorWindow.GetWindowWithRect(typeof(UniFluxExtensionTypeWindow), centerRect, true, "Uniflux Generator Key");
            window.ShowPopup();
        }
        // [MenuItem("UniFlux/Open Packages", priority = 0)] private static void Openw_Window_UniFluxPackagesWindow()
        // {
        //     EditorWindow.GetWindow<UniFluxPackagesWindow>(false, "UniFlux Packages", true);
        // }
        [MenuItem("UniFlux/📚 Documentation", priority = 100)] private static void OpenDocumentation()
        {
            Application.OpenURL("https://xavierarpa.gitbook.io/uniflux");
        }
        [MenuItem("UniFlux/📦 Github", priority = 100)] private static void OpenGithub()
        {
            Application.OpenURL("https://github.com/xavierarpa/UniFlux");
        }
        [MenuItem("UniFlux/👋 Contact", priority = 200)] private static void OpenMail()
        {
            Application.OpenURL("mailto:"+"arpaxavier@gmail.com");
        }
    }
}
