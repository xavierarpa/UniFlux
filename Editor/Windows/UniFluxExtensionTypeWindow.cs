using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace UniFlux.Editor
{
    public class UniFluxExtensionTypeWindow : EditorWindow
    {
        private string inputText = "";


        void OnGUI()
        {
            void ConfirmGeneration()
            {
                string folderPath = EditorUtility.OpenFolderPanel($"Select the Folder to Generate \"UniFlux.{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(inputText)}.cs\"", Application.dataPath, default);
                
                if(!string.IsNullOrEmpty(folderPath))
                {
                    string path_assets = Application.dataPath;
                    string path_packages = path_assets + "/../Library/PackageCache";

                    string search_packages_uniflux = "com.xavierarpa.uniflux"; // Carpeta Template de Packages
                    string search_template = "__UniFluxTypeExtension-UniFluxTypeExtension.cs"; // Archivo Template

                    var list_folders_packages = Directory.GetDirectories(path_packages, ".", SearchOption.TopDirectoryOnly);

                    Debug.Log("--------------------");
                    foreach (var item in list_folders_packages)
                    {
                        Debug.Log(item);
                    }
                    Debug.Log("--------------------");

                    var _path_packages_uniflux = "list_folders_packages.FirstOrDefault(p => p";
                    // {
                    //     await Task.Yield();
                    //     // => p.Contains(search_packages_uniflux)

                    //     return false;
                    // });
                    var isPathPackagesUniFluxExist = !string.IsNullOrEmpty(_path_packages_uniflux);

                    Debug.Log($"carpetaEncontrada, {_path_packages_uniflux}, isPathPackagesUniFluxExist: {isPathPackagesUniFluxExist}");

                    // Paquete UniFlux está en Packages ?
                    if(isPathPackagesUniFluxExist)
                    {

                    }

                    // string[] files = Directory.GetFiles(searchDirectory, searchPattern, SearchOption.AllDirectories);

                    // Debug.Log(files.Length > 0 ? "Archivos encontrados:\n" + string.Join("\n", files) : "No se encontraron archivos con el nombre especificado.");



                    // "Library/PackageCache/"
                    // Busca el archivo template dentro del path del paquete UniFlux

                    // Crea un archivo en "Assets/UniFlux/Runtime/Plugin/Extensions/"

                    // inputText
                    //  string templatePath = Application.dataPath + "/TemplateScript.cs";
                    // string templateContent = File.ReadAllText(templatePath);

                    // templateContent = templateContent.Replace("#SCRIPTNAME#", scriptName);

                    // string folderPath = Application.dataPath + "/Scripts/Generated/";
                    // Directory.CreateDirectory(folderPath);
                    // string scriptPath = folderPath + scriptName + ".cs";

                    // File.WriteAllText(scriptPath, templateContent);

                    // AssetDatabase.Refresh();

                    // Debug.Log("Generated script: " + scriptName);

                }
                else
                {
                    // Cancels... Nothing Happens!
                }
            }
            void Render()
            {
                bool hasText = inputText.Length>0;

                // TEXT DESCRIPTION
                GUILayout.Label("Write the Type to generate the custom extensions to use");
                GUILayout.Label("Ex.: <T>.Dispatch();");
                GUILayout.Space(5);

                // INPUT
                inputText = EditorGUILayout.TextField("Type <T>:", inputText).Replace(" ", "");
                GUILayout.Space(5);
                
                // BUTTON GENERATE
                GUI.enabled = hasText;
                if (GUILayout.Button("Generate"))
                {
                    ConfirmGeneration();
                }
                GUI.enabled = true;
                GUILayout.Space(5);
                
                // TEXT EXAMPLE
                GUILayout.Label("Ex.: \"string\" => \"UniFlux.String.cs\"");
            }
            // GUILayout.BeginHorizontal();
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Space(16);
                GUILayout.FlexibleSpace();


                using (new GUILayout.VerticalScope())
                {
                    GUILayout.FlexibleSpace();

                    Render();
                    
                    GUILayout.FlexibleSpace();
                }
                GUILayout.FlexibleSpace();

                GUILayout.Space(16);
            }

            GUILayout.Space(16);
        }
    }
}