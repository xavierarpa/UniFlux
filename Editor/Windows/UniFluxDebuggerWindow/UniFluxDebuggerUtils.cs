using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UniFlux.Core;

namespace UniFlux.Editor
{
    internal static class UniFluxDebuggerUtils
    {
        // public const string ParticleSystemForceField_Gizmo = "ParticleSystemForceField Gizmo";
        //
        // public const string PreMatQuad = "PreMatQuad"; // Cuadrado
        // public const string PreMatSphere = "PreMatSphere"; // Esfera
        // public const string PreMatCube = "PreMatCube"; // Cubo
        // public const string PreMatCylinder = "PreMatCylinder"; // Cylinder
        //
        public const string PreviewPackageInUse = "PreviewPackageInUse"; // Caja Amarilla
        public const string d_Package_Manager = "d_Package Manager"; // Caja Gris
        //
        public static int _id = -1;
        public static MyTreeElement Root = new MyTreeElement("Root", -1, ++_id, d_Package_Manager, () => string.Empty, Array.Empty<string>(), string.Empty, null, string.Empty);
        internal static readonly BindingFlags m_bindingflag_all = (BindingFlags)(-1);
        //
        public static IEnumerable<MyTreeElement> TreeElements_NONE = new List<MyTreeElement>();
        public static IEnumerable<MyTreeElement> TreeElements_NO_DEBUG = GetTreeElements_NO_DEBUG();
        public static IEnumerable<MyTreeElement> TreeElements_DEBUG => GetTreeElements_DEBUG();
        //
        public static void ClearRootChilds(MyTreeElement root)
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
        public static void SetRootChild(MyTreeElement root, MyTreeElement child)
        {
            root.Children.Add(child);
            child.Parent = root;
        }
        public static IEnumerable<MyTreeElement> GetTreeElements_NO_DEBUG()
        {
            MyTreeElement Create_0_Attribute(FluxAttribute attribute)
            {
                return default;
            }
            MyTreeElement Create_1_Methods()
            {
                return default;
            }
            var list = new List<MyTreeElement>();
            return list;
        }
        public static IEnumerable<MyTreeElement> GetTreeElements_DEBUG()
        {
            var list = new List<MyTreeElement>();

            MyTreeElement Create_0_Attribute()
            {
                return default;
            }
            MyTreeElement Create_1_StaticFlux()
            {
                return default;
            }
            MyTreeElement Create_2_KEY()
            {
                return default;
            }
            MyTreeElement Create_3_Methods()
            {
                return default;
            }
            MyTreeElement Create_Type(Type type)
            {
                var element = new MyTreeElement(
                    type.ToString(), 
                    Root.Depth + 1, 
                    ++_id, 
                    PreviewPackageInUse,
                    () => "RES string.Empty", 
                    Array.Empty<string>(), 
                    string.Empty, 
                    default, 
                    kind: "KIND string.Empty"
                );
                return element;
            }

            var list_fluxTypes = UniFlux.Core.Internal.Flux.List_FluxTypes.ToList();
            Debug.Log(list_fluxTypes.Count);

            for (int i = 0; i < list_fluxTypes.Count; i++)
            {
                list.Add(Create_Type(list_fluxTypes[i]));
            }
            return list;
        }
        #pragma warning disable CS0618
        private static IEnumerable<FluxAttribute> GetMonoFluxesFluxAttributes() => GetMonoFluxesMethodsFromFluxAttribute().SelectMany(m => m.GetCustomAttributes<FluxAttribute>());
        private static IEnumerable<MethodInfo> GetMonoFluxesMethodsFromFluxAttribute() => GetMonoFluxesMethods().Where(m => m.GetCustomAttributes(typeof(FluxAttribute), true).Any());
        #pragma warning restore CS0618
        private static IEnumerable<MethodInfo> GetMonoFluxesMethods() => GetMonoFluxesTypes().SelectMany(mt => mt.GetMethods(m_bindingflag_all));
        private static IEnumerable<Type> GetMonoFluxesTypes() => GetClassTypes().Where(t => t.IsSubclassOf(typeof(MonoFlux)));
        private static IEnumerable<MethodInfo> GetAllFluxAttributesMethods() => GetClassTypes().SelectMany(mt => mt.GetMethods(m_bindingflag_all)).Where(m => m.GetCustomAttributes(typeof(FluxAttribute), true).Any());
        private static IEnumerable<Type> GetClassTypes() => GetModules().SelectMany(m => m.GetTypes());
        private static IEnumerable<Module> GetModules() => GetAssemblies().SelectMany(a => a.Modules);
        private static Assembly[] GetAssemblies() => AppDomain.CurrentDomain.GetAssemblies();
    }
}
// new MyTreeElement(
//     m.ToString(), 
//     Root.Depth + 1, 
//     ++_id, 
//     PreviewPackageInUse,
//     () => "RES string.Empty", 
//     Array.Empty<string>(), 
//     string.Empty, 
//     default, 
//     kind: "KIND string.Empty"
// );




// return GetAllFluxAttributesMethods().Select(m => 
// {
// // UniFluxDebuggerUtils.SetRootChild(element, TreeElements_Child_Test);
// return element;
// });



// public static MyTreeElement TreeElements_Child_Test = new MyTreeElement(
//     UniFlux.Core.Internal.Flux.List_FluxTypes.Count.ToString(), 
//     Root.Depth + 2,
//     ++_id, 
//     ParticleSystemForceField_Gizmo,
//     () => "RES string.Empty", 
//     Array.Empty<string>(), 
//     string.Empty, 
//     default, 
//     kind: "KIND string.Empty"
// );