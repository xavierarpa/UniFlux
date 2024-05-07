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
        internal static readonly BindingFlags m_bindingflag_all = (BindingFlags)(-1);

        public static MyTreeElement Test_GetFluxItem()
        {
            return default;
        }

        public static async void Test()
        {
            Debug.Log($"INIT Test......");
            foreach (var type in GetMonoFluxesMethodsFromFluxAttribute())
            {
                Debug.Log(type);
                await Task.Yield();
            }
            Debug.Log($"......END Test"); 
        }
        #pragma warning disable CS0618
        private static IEnumerable<FluxAttribute> GetMonoFluxesFluxAttributes() => GetMonoFluxesMethodsFromFluxAttribute().SelectMany(m => m.GetCustomAttributes<FluxAttribute>());
        private static IEnumerable<MethodInfo> GetMonoFluxesMethodsFromFluxAttribute() => GetMonoFluxesMethods().Where(m => m.GetCustomAttributes(typeof(FluxAttribute), true).Any());
        #pragma warning restore CS0618
        private static IEnumerable<MethodInfo> GetMonoFluxesMethods() => GetMonoFluxesTypes().SelectMany(mt => mt.GetMethods(m_bindingflag_all));
        private static IEnumerable<Type> GetMonoFluxesTypes() => GetClassTypes().Where(t => t.IsSubclassOf(typeof(MonoFlux)));
        private static IEnumerable<Type> GetClassTypes() => GetModules().SelectMany(m => m.GetTypes());
        private static IEnumerable<Module> GetModules() => GetAssemblies().SelectMany(a => a.Modules);
        private static Assembly[] GetAssemblies() => AppDomain.CurrentDomain.GetAssemblies();
    }
}
