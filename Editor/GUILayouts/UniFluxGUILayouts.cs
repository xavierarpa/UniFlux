using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace UniFlux.Editor
{
    public static class GUILayouts
    {
        private static readonly Dictionary<Type, Func<string, object, object>> editorFieldMap = new Dictionary<Type, Func<string, object, object>>
        {
            { typeof(float), (title, value) => EditorGUILayout.FloatField(title, (float)value) },
            { typeof(double), (title, value) => EditorGUILayout.DoubleField(title, (double)value) },
            { typeof(int), (title, value) => EditorGUILayout.IntField(title, (int)value) },
            { typeof(long), (title, value) => EditorGUILayout.LongField(title, (long)value) },
            { typeof(Vector2), (title, value) => EditorGUILayout.Vector2Field(title, (Vector2)value) },
            { typeof(Vector3), (title, value) => EditorGUILayout.Vector3Field(title, (Vector3)value) },
            { typeof(Vector4), (title, value) => EditorGUILayout.Vector4Field(title, (Vector4)value) },
            { typeof(Vector2Int), (title, value) => EditorGUILayout.Vector2IntField(title, (Vector2Int)value) },
            { typeof(Vector3Int), (title, value) => EditorGUILayout.Vector3IntField(title, (Vector3Int)value) },
            { typeof(Rect), (title, value) => EditorGUILayout.RectField(title, (Rect)value) },
            { typeof(RectInt), (title, value) => EditorGUILayout.RectIntField(title, (RectInt)value) },
            { typeof(Bounds), (title, value) => EditorGUILayout.BoundsField(title, (Bounds)value) },
            { typeof(BoundsInt), (title, value) => EditorGUILayout.BoundsIntField(title, (BoundsInt)value) },
            { typeof(Color), (title, value) => EditorGUILayout.ColorField(title, (Color)value) },
            { typeof(Gradient), (title, value) => EditorGUILayout.GradientField(title, (Gradient)value) },
            { typeof(bool), (title, value) => EditorGUILayout.Toggle(title, (bool)value) },
            { typeof(string), (title, value) => EditorGUILayout.TextField(title, (string)value) },
        };

        public static void RefreshStatusBar(Action callback)
        {
            using (new EditorGUILayout.HorizontalScope(Styles.AppToolbar))
            {
                GUILayout.FlexibleSpace();
                var refreshIcon = EditorGUIUtility.IconContent("d_TreeEditor.Refresh");
                refreshIcon.tooltip = "Forces Tree View to Refresh";
                
                if (GUILayout.Button(refreshIcon, Styles.StatusBarIcon, GUILayout.Width(25)))
                {
                    callback?.Invoke();
                }
            }
        }   

        public static object SuperField(string title, Type type, object input)
        {
            object defaultValue;

            if (type.IsValueType)
            {
                // If the parameter type is a value type or has a default constructor, create an instance
                defaultValue = Activator.CreateInstance(type);
            }
            else
            {
                // Handle the case where the parameter type doesn't have a default constructor
                defaultValue = null; // or provide a default value based on your requirements
            }
            // El parámetro es primitivo
            object value = input ?? defaultValue;

            // Verifica si el tipo está en el diccionario
            if (editorFieldMap.ContainsKey(type))
            {
                // Obtiene la función correspondiente al tipo y la ejecuta
                return editorFieldMap[type](title, value);
            }
            else if (type.IsInterface)
            {
                return EditorGUILayout.ObjectField(title, value as UnityEngine.Object, type, true);
            }
            else if (type.IsEnum)
            {
                return EditorGUILayout.EnumFlagsField(title, (Enum)value);
            }
            else if (type.IsClass)
            {
                // Si el tipo no está en el diccionario y no es un tipo de valor, usa ObjectField
                return EditorGUILayout.ObjectField(title, value as UnityEngine.Object, type, true);
            }
            else
            {
                // EditorGUILayout.ser 
                // Si no se reconoce el tipo, devuelve el valor sin modificar
                return value;
            }
        }
    }
}
