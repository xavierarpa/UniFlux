using System.Globalization;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UniFlux.Editor
{
    public class UniFluxGeneratorKeyWindow : EditorWindow
    {
        const string KEY_SCRIPTNAME = "#SCRIPTNAME#";
        private string inputText = "";
        private string InputFile => $"UniFlux.{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(inputText)}.cs";
        void OnGUI()
        {
            if(inputText == default)
            {
                inputText = "";
            }

            void ConfirmGeneration()
            {
                string folderPath = EditorUtility.OpenFolderPanel($"Select the Folder to Generate \"{InputFile}\"", Application.dataPath, default);
                
                if(!string.IsNullOrEmpty(folderPath))
                {
                    string filePath = folderPath + "/" + InputFile;

                    string path_assets = Application.dataPath;
                    string path_general = Application.dataPath + "/..";
                    //
                    string search_template = "__UniFluxTypeExtension-UniFluxTypeExtension.cs.txt"; // Archivo Template
                    //
                    var list_folders_packages = Directory.GetFiles(path_general, "*"+search_template, SearchOption.AllDirectories);
                    //
                    var _path_template = list_folders_packages.FirstOrDefault(p => p.Contains(search_template) && !p.Contains(".meta"));
                    var isPathTemplateFinded = !string.IsNullOrEmpty(_path_template);
                    //
                    if(isPathTemplateFinded)
                    {
                        string txt_newScript = File.ReadAllText(_path_template).Replace(KEY_SCRIPTNAME, inputText);
                        //
                        using (StreamWriter writer = new StreamWriter(filePath))
                        {
                            writer.Write(txt_newScript);
                        }
                        //
                        AssetDatabase.Refresh();
                        //
                        inputText="";
                    }
                    else
                    {
                        // err no encontrado template
                        EditorUtility.DisplayDialog("Error", "Template not found :(", "Ok");
                    }
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
                GUILayout.Label($"Ex.: <T>.Dispatch();");
                GUILayout.Space(5);

                // INPUT
                inputText = EditorGUILayout.TextField(inputText).Replace(" ", ""); // "Type <T>:", 
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
                GUILayout.Space(5);
                GUILayout.Label("Note: Check namespaces, Ex.: \"UnityEngine.Vector2\" ");
                GUILayout.Space(5);
                GUILayout.Label("Note: \"int\" and \"string\" are included by default");
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