using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UniFlux.Core;
using UnityEditor;

namespace UniFlux.Editor
{
    internal static class UniFluxDebuggerUtils
    {
        private static readonly Texture Tx_PreviewPackageInUse = GetIcon(PreviewPackageInUse);
        private static readonly Texture Tx_d_Package_Manager = GetIcon(d_Package_Manager);
        //
        public const string PreviewPackageInUse = "PreviewPackageInUse"; // Caja Amarilla
        public const string d_Package_Manager = "d_Package Manager"; // Caja Gris
        //
        public static int _id = -1;
        public static UniFluxTreeElement Root = CreateElement("Root", -1, Tx_d_Package_Manager);// new UniFluxTreeElement("Root", -1, ++_id, Tx_d_Package_Manager, () => string.Empty, Array.Empty<string>(), string.Empty, null, string.Empty);
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
        }
        public static IEnumerable<UniFluxTreeElement> GetTreeElements_NO_DEBUG()
        {
            UniFluxTreeElement Create_0_Attribute(FluxAttribute attribute)
            {
                return default;
            }
            UniFluxTreeElement Create_1_Methods()
            {
                return default;
            }
            var list = new List<UniFluxTreeElement>();
            return list;
        }
        public static IEnumerable<UniFluxTreeElement> GetTreeElements_DEBUG()
        {
            var list = new List<UniFluxTreeElement>();

            UniFluxTreeElement Create_0_Attribute()
            {
                return default;
            }
            UniFluxTreeElement Create_1_StaticFlux()
            {
                return default;
            }
            UniFluxTreeElement Create_2_KEY()
            {
                return default;
            }
            UniFluxTreeElement Create_3_Methods()
            {
                return default;
            }

            var list_fluxTypes = UniFlux.Core.Internal.Flux.List_FluxTypes;
            Debug.Log(list_fluxTypes.Count);

            for (int i = 0; i < list_fluxTypes.Count; i++)
            {
                list.Add(Create_As_StaticFluxType(list_fluxTypes[i]));
            }
            return list;
        }


        private static UniFluxTreeElement Create_As_StaticFluxType(Type fluxType)
        {
            var element = CreateElement(fluxType.ToString(), Root.Depth + 1, Tx_PreviewPackageInUse);
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


        // private static UniFluxTreeElement Create_As_StaticFluxType_KEY(Type fluxType)
        // {
        //     var element = new UniFluxTreeElement(
        //         fluxType.ToString(), 
        //         Root.Depth + 1, 
        //         ++_id, 
        //         GetIcon(PreviewPackageInUse),
        //         () => "RES string.Empty", 
        //         Array.Empty<string>(), 
        //         string.Empty, 
        //         default, 
        //         kind: "KIND string.Empty"
        //     );
        //     return element;
        // }

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
        private static Texture GetIcon(string icon) => EditorGUIUtility.IconContent(icon).image;
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



// public const string ParticleSystemForceField_Gizmo = "ParticleSystemForceField Gizmo";
// public const string PreMatQuad = "PreMatQuad"; // Cuadrado
// public const string PreMatSphere = "PreMatSphere"; // Esfera
// public const string PreMatCube = "PreMatCube"; // Cubo
// public const string PreMatCylinder = "PreMatCylinder"; // Cylinder