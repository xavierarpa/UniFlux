using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace UniFlux.Editor
{
    [EditorWindowTitle(title = "UniFlux Packages")] 
    public class UniFluxPackagesWindow : EditorWindow
    {
        private void OnGUI()
        {
            GUILayout.FlexibleSpace();
            GUILayout.Label("Aqui colocaremos un acceso rapido para instalar los paquetes de usan UniFlux, como un Package Manager v2");
            GUILayout.FlexibleSpace();
        }
    }
}