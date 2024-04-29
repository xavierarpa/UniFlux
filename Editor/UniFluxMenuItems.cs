using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UniFlux.Editor
{
    public static class UniFluxMenuItems
    {
        [MenuItem("UniFlux/Open Debugger", priority = 0)] private static void Openw_Window_UniFluxDebuggerWindow()
        {
            EditorWindow.GetWindow<UniFluxDebuggerWindow>(false, "UniFlux Debugger", true);
        }
        [MenuItem("UniFlux/Open Packages", priority = 0)] private static void Openw_Window_UniFluxPackagesWindow()
        {
            EditorWindow.GetWindow<UniFluxPackagesWindow>(false, "UniFlux Packages", true);
        }
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
