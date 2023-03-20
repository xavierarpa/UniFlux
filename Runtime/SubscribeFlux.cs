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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using Kingdox.Flux.Core;

namespace Kingdox.Flux
{
    internal static partial class SubscribeFlux 
    {
        private static readonly Type m_type_void = typeof(void);
        private static readonly Dictionary<MonoFlux, List<MethodInfo>> m_monofluxes = new Dictionary<MonoFlux, List<MethodInfo>>();
        private static readonly Dictionary<MethodInfo, SubscribeAttribute> m_methods = new Dictionary<MethodInfo, SubscribeAttribute>();
        private static readonly Dictionary<(bool isParam, bool isReturn), (Type fluxType, Type delegateType, string methodName)> m_fluxActivity = new Dictionary<(bool isParam, bool isReturn), (Type fluxType, Type delegateType, string methodName)>()
        {
            [(false,  false)] = (typeof(Core.Flux),     typeof(Action),     nameof(Core.Flux.SubscribeAction)),
            [(true,   false)] = (typeof(Core.Flux<>),   typeof(Action<>),   nameof(Core.Flux<object>.SubscribeActionParam)),
            [(false,  true)]  = (typeof(Core.Flux<>),   typeof(Func<>),     nameof(Core.Flux<object>.SubscribeFunc)),
            [(true,   true)]  = (typeof(Core.Flux<,>),  typeof(Func<,>),    nameof(Core.Flux<object,object>.SubscribeFuncParam)),
        };
        internal static void Subscribe(this MonoFlux monoflux, bool condition)
        {
            CheckMonoFlux(monoflux); // ~First Check Monoflux Implementation
            List<MethodInfo> methods = m_monofluxes[monoflux];
            for (int i = 0; i < methods.Count; i++) SubscribeAttribute(monoflux, methods[i], condition);
        }
        private static void CheckMonoFlux(MonoFlux monoflux)
        {
            if (!m_monofluxes.ContainsKey(monoflux))
            {
                m_monofluxes.Add(
                    monoflux, 
                    monoflux.gameObject.GetComponent(typeof(MonoFlux)).GetType().GetMethods((BindingFlags)(-1)).Where(method => 
                    {
                        var attribute = System.Attribute.GetCustomAttributes(method).FirstOrDefault((_att) => _att is SubscribeAttribute) as SubscribeAttribute;
                        if (attribute != null)
                        {
                            if(!m_methods.ContainsKey(method)) m_methods.Add(method, attribute); // ADD <Method, Attribute>!
                            return true;
                        }
                        else return false;
                    }).ToList()
                );
            }
        }
        private static void SubscribeAttribute(MonoFlux monoflux, MethodInfo methodInfo, bool condition)
        {
            var _Parameters = methodInfo.GetParameters();
#if UNITY_EDITOR
            if(_Parameters.Length > 1) // Auth Params is 0 or 1
            {
                throw new System.Exception($"Error '{methodInfo.Name}' : Theres more than one parameter, please set 1 or 0 parameter. (if you need to add more than 1 argument use Tuples or create a struct, record o class...)");
            }
#endif
            // Activity
            var activity = m_fluxActivity[(_Parameters.Length.Equals(1), !methodInfo.ReturnType.Equals(m_type_void))];
            switch ((_Parameters.Length.Equals(1), !methodInfo.ReturnType.Equals(m_type_void)))
            {
                case (false, false): 
                break;
                case (true, false): 
                    activity.delegateType = activity.delegateType.MakeGenericType(_Parameters[0].ParameterType); 
                    activity.fluxType = activity.fluxType.MakeGenericType(_Parameters[0].ParameterType);
                break;
                case (false, true): 
                    activity.delegateType = activity.delegateType.MakeGenericType(methodInfo.ReturnType); 
                    activity.fluxType = activity.fluxType.MakeGenericType(methodInfo.ReturnType);
                break;
                case (true, true): 
                    activity.delegateType = activity.delegateType.MakeGenericType(_Parameters[0].ParameterType, methodInfo.ReturnType); 
                    activity.fluxType = activity.fluxType.MakeGenericType(_Parameters[0].ParameterType, methodInfo.ReturnType);
                break;
            }
            //Execute
            activity.fluxType.GetMethod(activity.methodName).Invoke(
                null, 
                new object[]
                {
                    m_methods[methodInfo].key,
                    methodInfo.CreateDelegate(activity.delegateType, monoflux),
                    condition
                }   
            );
        }
    }
}
