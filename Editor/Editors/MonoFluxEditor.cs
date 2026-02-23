/*
Copyright (c) 2023 Xavier Arpa López Thomas Peter ('xavierarpa')

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.Linq;
using System.Collections.Generic;
using UniFlux.Core;

namespace UniFlux.Editor
{
    [CustomEditor(typeof(MonoFlux), true)]
    public partial class MonoFluxEditor : UnityEditor.Editor
    {
        private MethodInfo[] methods_subscribeAttrb;
        private Dictionary<MethodInfo, object> dic_method_parameters;
        private Dictionary<MethodInfo, object> dic_method_outputs;
        #pragma warning disable CS0618
        private Dictionary<MethodInfo, FluxAttribute> dic_method_attributes;
        #pragma warning restore CS0618
        private static bool ShowMethods
        { 
            get => PlayerPrefs.GetInt("__UniFlux.MonoFluxEditor.ShowBox", default) == default;
            set => PlayerPrefs.SetInt("__UniFlux.MonoFluxEditor.ShowBox", value ? default : 1);
        }
        private void OnEnable()
        {
            Type type = target.GetType();
            var methods = type.GetMethods((BindingFlags)(-1));
            #pragma warning disable CS0618
            var discoveredMethods = new List<MethodInfo>();
            dic_method_attributes = new Dictionary<MethodInfo, FluxAttribute>();

            // Phase 1: methods with FluxAttribute on the concrete class
            foreach (var m in methods)
            {
                if (Attribute.GetCustomAttributes(m).FirstOrDefault(a => a is FluxAttribute) is FluxAttribute attr)
                {
                    discoveredMethods.Add(m);
                    dic_method_attributes[m] = attr;
                }
            }

            // Phase 2: FluxAttributes declared on interface method definitions
            var interfaces = type.GetInterfaces();
            for (int i = 0; i < interfaces.Length; i++)
            {
                var map = type.GetInterfaceMap(interfaces[i]);
                for (int j = 0; j < map.InterfaceMethods.Length; j++)
                {
                    var ifaceMethod = map.InterfaceMethods[j];
                    if (Attribute.GetCustomAttributes(ifaceMethod).FirstOrDefault(a => a is FluxAttribute) is FluxAttribute attr)
                    {
                        var implMethod = map.TargetMethods[j];
                        if (!dic_method_attributes.ContainsKey(implMethod))
                        {
                            dic_method_attributes[implMethod] = attr;
                        }
                        if (!discoveredMethods.Contains(implMethod))
                        {
                            discoveredMethods.Add(implMethod);
                        }
                    }
                }
            }

            methods_subscribeAttrb = discoveredMethods.ToArray();
            #pragma warning restore CS0618
            dic_method_parameters = methods_subscribeAttrb.Select(m => new { Method = m, Parameters = null as object  }).ToDictionary(mp => mp.Method, mp => mp.Parameters);
            dic_method_outputs = methods_subscribeAttrb.Select(m => new { Method = m, Return = null as object }).ToDictionary(mp => mp.Method, mp => mp.Return);
        }
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if(methods_subscribeAttrb.Length.Equals(0))
            {
                ShowMethods = false;
            }
            else
            {
                GUILayout.Space(10);
                if(GUILayout.Button( ShowMethods ? $"Close MonoFlux Tool ({methods_subscribeAttrb.Length})" : $"Open MonoFlux Tool ({methods_subscribeAttrb.Length})"))
                {
                    ShowMethods = !ShowMethods;
                }
            }
            if(ShowMethods)
            {
                _Draw();
            }
        }
        private void _Draw()
        {
            GUILayout.Space(20);
            
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
            {
                alignment = TextAnchor.MiddleLeft,
                richText = true,
                fontSize = 12
            };
            GUIStyle style_title = new GUIStyle(EditorStyles.boldLabel)
            {
                richText = true
            };
            foreach (var item in methods_subscribeAttrb)
            {
                #pragma warning disable CS0618
                var atribute = dic_method_attributes[item];
                #pragma warning restore CS0618
                var parameters = item.GetParameters();
                var isParameters = parameters.Length > 0;
                var isReturn = item.ReturnType != typeof(void);
                var isErr_static = item.IsStatic;
                var isError = isErr_static;
                string key_color =  isError ? "yellow" : "white";
                var atrbtype = atribute;

                
                int opt_status = (param: isParameters, rtrn: isReturn) switch
                {
                    (false, false) => 0, // Action
                    (true, false) => 1, // Action<T>
                    (false, true) => 2, // Func<T>
                    (true, true) => 3, // Func<T, T2>
                };

                //
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                // KEY
                GUILayout.Label( $"<color={key_color}>{atribute.key}</color>", style_title);
                // Method Name
                GUILayout.Label($"[{atrbtype.GetType().Name}] {item}", EditorStyles.whiteMiniLabel); 
                
                if(true)//(Application.isPlaying)
                {
                    EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                    if(isParameters)
                    {
                        // INPUT
                        GenerateInput(item, parameters);
                    }

                    // INVOKE BUTTON
                    GenerateButton(buttonStyle, item, opt_status);

                    if(isReturn)
                    {
                        // OUTPUT
                        GenerateOutput(item, parameters);
                    }

                    EditorGUILayout.EndVertical();
                }
                else
                {
                    // Nothing
                }


                EditorGUILayout.EndVertical();
                GUILayout.Space(5);
            }
        }
        private void GenerateInput(MethodInfo item, ParameterInfo[] parameters)
        {
            GUI.enabled = Application.isPlaying;
            // item.GetParameters()[0].Name
            dic_method_parameters[item] = GUILayouts.SuperField($"Input: {item.GetParameters()[0].ParameterType.Name}", parameters[0].ParameterType, dic_method_parameters[item]);
            GUI.enabled = true;
        }
        private void GenerateOutput(MethodInfo item, ParameterInfo[] parameters)
        {
            GUI.enabled = false;
            dic_method_outputs[item] = GUILayouts.SuperField($"Output: {item.ReturnType.Name}", item.ReturnType, dic_method_outputs[item]);
            GUI.enabled = true;
        }

        private void GenerateButton(GUIStyle buttonStyle, MethodInfo item, int opt)
        {
            GUI.enabled = Application.isPlaying;

            switch (opt)
            {
                case 0: // Action
                    if(GUILayout.Button($"Invoke!", buttonStyle))
                    {
                        item.Invoke(target, new object[0]);
                    }
                break;
                case 1: // Action<T>
                    if(GUILayout.Button($"Invoke!", buttonStyle))
                    {
                        item.Invoke(target, new object[]{dic_method_parameters[item]});
                    }
                break;
                case 2: // Func<T>
                    if(GUILayout.Button($"Invoke!", buttonStyle))
                    {
                        var resp_2 = item.Invoke(target, new object[0]);
                        dic_method_outputs[item] = resp_2;
                    }
                break;
                case 3: // Func<T, T2>
                    if(GUILayout.Button($"Invoke!", buttonStyle))
                    {
                        var resp_3 = item.Invoke(target, new object[]{dic_method_parameters[item]});
                        dic_method_outputs[item] = resp_3;
                    }
                break;
            }
            GUI.enabled = true;
        }
    }
}