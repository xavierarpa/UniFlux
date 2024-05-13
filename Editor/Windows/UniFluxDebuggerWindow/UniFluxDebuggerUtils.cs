#pragma warning disable CS0618

using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UniFlux.Core;
using UnityEditor;
using UnityEditorInternal;

namespace UniFlux.Editor
{
    internal static class UniFluxDebuggerUtils
    {
        // d_orangeLight	
        // d_redLight
        // d_greenLight	


        // Linked


        // d_UnityEditor.SceneHierarchyWindow
        // d_Linked
        // d_Lighting	
        // private static readonly Texture Tx_d_SignalAsset = GetIcon("d_SignalAsset Icon");
        // private static readonly Texture Tx_PreviewPackageInUse = GetIcon("PreviewPackageInUse");
        // private static readonly Texture Tx_d_FilterByLabel = GetIcon("d_FilterByLabel");
        // private static readonly Texture Tx_DeclaringType = GetIcon("cs Script Icon");
        // private static readonly Texture Tx_MethodInfo = GetIcon("Tile Icon");
        // ParticleSystemForceField Icon	
        private static readonly Texture Tx_DeclaringType = GetIcon("d_Tilemap Icon"); 
        private static readonly Texture Tx_Attribute = GetIcon("d_SortingGroup Icon");
        private static readonly Texture Tx_KEY = GetIcon("d_Tile Icon");
        private static readonly Texture Tx_MethodInfo = GetIcon("d_NetworkAnimator Icon");
        private static readonly Texture Tx_d_Package_Manager = GetIcon("d_Package Manager");
        //
        private static readonly Texture Tx_ParticleSystemForceField_Gizmo = GetIcon("ParticleSystemForceField Gizmo");
        // private static readonly Texture Tx_PreMatQuad = GetIcon("PreMatQuad");
        // public const string PreMatQuad = "PreMatQuad"; // Cuadrado
        // public const string PreMatSphere = "PreMatSphere"; // Esfera
        // public const string PreMatCube = "PreMatCube"; // Cubo
        // public const string PreMatCylinder = "PreMatCylinder"; // Cylinder
        //
        public static int _id = -1;
        public static UniFluxTreeElement Root = CreateElement("Root", -1, Tx_d_Package_Manager);
        internal static readonly BindingFlags m_bindingflag_all = (BindingFlags)(-1);
        //
        public static IEnumerable<UniFluxTreeElement> TreeElements_NONE = new List<UniFluxTreeElement>();
        public static IEnumerable<UniFluxTreeElement> TreeElements_NO_DEBUG = GetTreeElements_NO_DEBUG();
        public static IEnumerable<UniFluxTreeElement> TreeElements_DEBUG => GetTreeElements_DEBUG();
        //
        public static void ClearRootChilds(UniFluxTreeElement root)
        {
            if(root is null) 
            {
                return;
            }

            for (int i = 0; i < root?.Children?.Count; i++)
            {
                root.Children[i].Parent = null;
            }
            root?.Children?.Clear();
        }
        public static void SetRootChild(UniFluxTreeElement root, UniFluxTreeElement child)
        {
            root.Children.Add(child);
            child.Parent = root;
            child.Depth = root.Depth+1; 
        }
        /// <summary>
        /// - class 1
        ///     - MethodFlux
        ///         - KEY
        ///             - void Method1(string value)
        ///             - void Method2(string value)
        ///         - KEY 2
        ///             - void Method3(string value)
        ///     - StateFlux
        ///         - KEY 2
        ///             - void Method4(string value)
        /// - class 2
        ///     - StateFlux
        ///         - KEY 2
        ///             - void Method5(string value)
        /// </summary>
        public static IEnumerable<UniFluxTreeElement> GetTreeElements_NO_DEBUG()
        {
            var list = new List<UniFluxTreeElement>();
            var methods = __NO_DEBUG_GetAllFluxAttributesMethods();
            var declaringTypes = methods
                .GroupBy(m => m.DeclaringType)
                .ToDictionary(group => group.Key, group => group.ToList())
            ;
            var attributes_type = methods
                .SelectMany(m => m.GetCustomAttributes(true))
                .OfType<FluxAttribute>()
                .GroupBy(attr => attr.GetType())
                .ToDictionary(group => group.Key, group => group.ToList())
            ;
            var attributes = methods
                .ToDictionary(m => m, m2 => m2.GetCustomAttribute<FluxAttribute>(true))
            ;
            var dt_atb_methods = declaringTypes.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.GroupBy(
                    m => m.GetCustomAttribute<FluxAttribute>(true).GetType(),
                    m => m)
                    .ToDictionary(group => group.Key, group => group.ToList())
                )
            ;
            var dt_atb_keys = declaringTypes.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.GroupBy(
                    m => m.GetCustomAttribute<FluxAttribute>(true).key,
                    m => m)
                    .ToDictionary(group => group.Key, group => group.Where(m => m.GetCustomAttribute<FluxAttribute>(true).key.Equals(group.Key)).ToList())
                )
            ;

            foreach (var declaringType in declaringTypes)
            {
                // Clase
                var _element_dt = Create_As_DeclaringTypeClass(declaringType.Key);

                foreach (var attribute_type in attributes_type)
                {
                    // Tipo de Atributo
                    var _element_atb = Create_As_FluxAttributeType(attribute_type.Key);
                    
                    if(dt_atb_methods[declaringType.Key].TryGetValue(attribute_type.Key, out var _dt_atb_methodsList))
                    {
                        // DT -> Attribute Type
                        SetRootChild(_element_dt, _element_atb);
                        
                        var key_methods = _dt_atb_methodsList
                            .GroupBy(m => m.GetCustomAttribute<FluxAttribute>(true).key)
                            .ToDictionary(
                                group => group.Key, // Usa la clave del atributo FluxAttribute como clave en el diccionario
                                group => group.ToList() // Convierte cada grupo de métodos en una lista y úsala como valor en el diccionario
                        );

                        foreach (var keymethod in key_methods)
                        {
                            // Clave
                            var _element_atb_key = Create_As_FluxAttribute_Key(keymethod.Key);
                            // Attribute -> KEY
                            SetRootChild(_element_atb, _element_atb_key);

                            foreach (var method in keymethod.Value)
                            {
                                var _element_method = Create_As_Method(method);

                                // KEY -> Metodo
                                SetRootChild(_element_atb_key, _element_method);
                            }
                        }
                    }
                }

                list.Add(_element_dt);
            }
            return list;
        }
        /// <summary>
        /// - ActionFlux<int, string>
        ///     - KEY
        ///         - void Method1(string value)
        ///         - void Method2(string value)
        ///     - KEY
        ///         - void Method2(string value)
        ///     - KEY
        ///         - void Method4(string value)
        /// - ActionFlux<string>
        ///     - KEY
        ///         - void Method1()
        ///         - void Method2()
        ///     - KEY
        ///         - void Method3()
        /// </summary>
        public static IEnumerable<UniFluxTreeElement> GetTreeElements_DEBUG()
        {
            var list_elements_FluxTypes = new List<UniFluxTreeElement>();
            var list_fluxTypes = UniFlux.Core.Internal.Flux.List_FluxTypes;

            // Add FluxTypes
            for (int i = 0; i < list_fluxTypes.Count; i++)
            {
                var fluxType = list_fluxTypes[i];
                var fluxType_genericTypeDefinition = fluxType.GetGenericTypeDefinition();

                // Element StaticFluxType
                var element_staticFluxType  = Create_As_StaticFluxType(fluxType);

                // Database Name
                var name_database = UniFlux.Core.Internal.Flux.DICTIONARY_Flux_Databases[fluxType_genericTypeDefinition];

                // Database Field
                var field_database  = fluxType.GetField(name_database, m_bindingflag_all);

                // Database
                var database = field_database.GetValue(null); // null == static
                var database_type = database.GetType();

                // var name_database_dic = UniFlux.Core.Internal.Flux.DICTIONARY_Flux_Databases_Dic[fluxType_genericTypeDefinition];

                // Database Field DIc
                // var field_database_dic  = database_type.GetField(name_database_dic, m_bindingflag_all);
                // var database_dic = field_database_dic.GetValue(database);

                // IDictionary
                // var dict = database_dic as IDictionary;

                List<MethodInfo> methods = new List<MethodInfo>(); 

                var dict = database_type
                    .GetMethod("__GetDictOfListMethods", m_bindingflag_all)
                    .Invoke(database, null) as IDictionary
                ;

                if (dict != null)
                {


                    foreach (var key in dict.Keys)
                    {
                        // Clave
                        var _element_key = Create_As_FluxAttribute_Key(key);
                        // FLUX TYPE -> KEY
                        SetRootChild(element_staticFluxType, _element_key);

                        var keyValue = dict[key] as List<MethodInfo>;


                        foreach (var method in keyValue)
                        {
                            var _element_method = Create_As_Method(method);
                            
                            // KEY -> VALUE
                            SetRootChild(_element_key, _element_method);
                        }
                    }
                }

                // Add StaticFluxType
                list_elements_FluxTypes.Add(element_staticFluxType);
            }

            return list_elements_FluxTypes;
        }


        private static UniFluxTreeElement Create_As_DeclaringTypeClass(Type type)
        {
            var element = CreateElement(type.Name.ToString(), Root.Depth + 1, Tx_DeclaringType);
            return element;
        }
        private static UniFluxTreeElement Create_As_Method(MethodInfo method)
        {
            var element = CreateElement(method.ToString(), Root.Depth + 1, Tx_MethodInfo);
            return element;
        }
        private static UniFluxTreeElement Create_As_FluxAttributeType(Type attribute_type)
        {
            var element = CreateElement(attribute_type.Name.ToString(), Root.Depth + 1, Tx_Attribute);
            return element;
        }
        private static UniFluxTreeElement Create_As_FluxAttribute_Key(object key)
        {
            string _name = $"({key.GetType().Name}): '{key}'";

            var element = CreateElement(_name, Root.Depth + 1, Tx_KEY);
            return element;
        }
        private static UniFluxTreeElement Create_As_StaticFluxType(Type fluxType)
        {
            var element = CreateElement(fluxType.Name.ToString(), Root.Depth + 1, Tx_ParticleSystemForceField_Gizmo);
            return element;
        }


        private static UniFluxTreeElement CreateElement(string name, int depth, Texture icon)
        {
            return new UniFluxTreeElement(
                name:  name,
                depth:  depth,
                id:  ++_id,
                icon:  icon,
                resolutions:  () => string.Empty,
                contracts:  Array.Empty<string>(),
                resolutionType:  string.Empty,
                callsite:  default,
                kind:  string.Empty
            );
        }
        private static Assembly[] GetAssemblies() => AppDomain.CurrentDomain.GetAssemblies();
        private static IEnumerable<Module> GetModules() => GetAssemblies().SelectMany(a => a.Modules);
        private static IEnumerable<Type> GetClassTypes() => GetModules().SelectMany(m => m.GetTypes());
        private static IEnumerable<MethodInfo> __NO_DEBUG_GetAllFluxAttributesMethods() => GetClassTypes().SelectMany(mt => mt.GetMethods(m_bindingflag_all)).Where(m => m.GetCustomAttributes(typeof(FluxAttribute), true).Any());
        private static Texture GetIcon(string icon) => EditorGUIUtility.IconContent(icon).image;
    }
}
// Kingdox.UniFlux