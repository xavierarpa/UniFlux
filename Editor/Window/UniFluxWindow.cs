using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace UniFlux.Editor
{
    [EditorWindowTitle(title = "UniFlux")] public class UniFluxWindow : EditorWindow
    {
        // [MenuItem("UniFlux/Open")] public static void OpenwWindow() => GetWindow<UniFluxWindow>("My Editor Window");
        // [MenuItem("UniFlux/Contact")] public static void OpenContact() => Debug.Log("UniFlux");
        private void OnGUI()
        {
            // Aquí puedes agregar los elementos de la interfaz de usuario de tu Editor Window
            GUILayout.Label("¡Hola desde mi Editor Window!", EditorStyles.boldLabel);

            if (GUILayout.Button("Haz clic"))
            {
                Debug.Log("¡Botón clickeado!");
            }
        }
    }
}
// [MenuItem("Window/Sequencing/Timeline", false, 1)]