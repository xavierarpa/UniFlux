/*
Copyright (c) 2023 Xavier Arpa López Thomas Peter ('Kingdox')

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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace Kingdox.Flux
{
    [AttributeUsageAttribute(AttributeTargets.Method, AllowMultiple = false)]
    public class FluxAttribute : System.Attribute
    {
        public readonly object key;
        public FluxAttribute(object key)
        {
            this.key = key;
        }
    }
}
namespace Kingdox.Flux.Core.Internal
{
    internal static partial class SubscribeFluxExtension
    {
        private static readonly Type m_type_void = typeof(void);
        private static readonly Dictionary<MonoFlux, List<MethodInfo>> m_monofluxes = new Dictionary<MonoFlux, List<MethodInfo>>();
        private static readonly Dictionary<MethodInfo, FluxAttribute> m_methods = new Dictionary<MethodInfo, FluxAttribute>();
        internal static void Subscribe(this MonoFlux monoflux, bool condition)
        {
            if (!m_monofluxes.ContainsKey(monoflux))
            {
                m_monofluxes.Add(
                    monoflux, 
                    monoflux.gameObject.GetComponent(typeof(MonoFlux)).GetType().GetMethods((BindingFlags)(-1)).Where(method => 
                    {
                        var attribute = System.Attribute.GetCustomAttributes(method).FirstOrDefault((_att) => _att is FluxAttribute) as FluxAttribute;
                        if (attribute != null)
                        {
                            if(!m_methods.ContainsKey(method)) m_methods.Add(method, attribute); // ADD <Method, Attribute>!
                            return true;
                        }
                        else return false;
                    }).ToList()
                );
            }
            //
            List<MethodInfo> methods = m_monofluxes[monoflux];
            //
            for (int i = 0; i < methods.Count; i++) 
            {
                var _Parameters = methods[i].GetParameters();
                #if UNITY_EDITOR
                    if(_Parameters.Length > 1) // Auth Params is 0 or 1
                    {
                        throw new System.Exception($"Error '{methods[i].Name}' : Theres more than one parameter, please set 1 or 0 parameter. (if you need to add more than 1 argument use Tuples or create a struct, record o class...)");
                    }
                #endif
                // Activity
                Type keyType = m_methods[methods[i]].key.GetType();
                Type fluxType;
                Type delegateType;
                string methodName;
                //
                switch ((_Parameters.Length.Equals(1), !methods[i].ReturnType.Equals(m_type_void)))
                {
                    case (false, false): 
                        fluxType = typeof(Core.Internal.Flux<>).MakeGenericType(keyType);
                        delegateType = typeof(Action);
                        methodName = nameof(Core.Internal.Flux<object>.SubscribeAction);
                    break;
                    case (true, false): 
                        fluxType = typeof(Core.Internal.Flux<,>).MakeGenericType(keyType, _Parameters[0].ParameterType);
                        delegateType = typeof(Action<>).MakeGenericType(_Parameters[0].ParameterType); 
                        methodName = nameof(Core.Internal.Flux<object,object>.SubscribeActionParam);
                    break;
                    case (false, true): 
                        fluxType = typeof(Core.Internal.Flux<,>).MakeGenericType(keyType, methods[i].ReturnType);
                        delegateType = typeof(Func<>).MakeGenericType(methods[i].ReturnType); 
                        methodName = nameof(Core.Internal.Flux<object,object>.SubscribeFunc);
                    break;
                    case (true, true): 
                        fluxType = typeof(Core.Internal.Flux<,,>).MakeGenericType(keyType, _Parameters[0].ParameterType, methods[i].ReturnType);
                        delegateType = typeof(Func<,>).MakeGenericType(_Parameters[0].ParameterType, methods[i].ReturnType); 
                        methodName = nameof(Core.Internal.Flux<object,object,object>.SubscribeFuncParam);
                    break;
                }
                //Execute
                fluxType.GetMethod(methodName).Invoke(
                    null, 
                    new object[]
                    {
                        m_methods[methods[i]].key,
                        methods[i].CreateDelegate(delegateType, monoflux),
                        condition
                    }   
                );
            }
        }
    }
}
//TODO: C# 11 allow Attribute<T>, instead of object key