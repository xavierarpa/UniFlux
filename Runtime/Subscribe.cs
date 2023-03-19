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

namespace Kingdox.Flux
{
    [AttributeUsageAttribute(AttributeTargets.Method, AllowMultiple = false)]
    public class SubscribeAttribute : System.Attribute
    {
        private static readonly Type m_type = typeof(SubscribeAttribute);
        private static readonly Dictionary<MonoFlux, List<MethodInfo>> m_monofluxes = new Dictionary<MonoFlux, List<MethodInfo>>();
        private static readonly Dictionary<MethodInfo, SubscribeAttribute> m_methods = new Dictionary<MethodInfo, SubscribeAttribute>();
        public readonly string key;
        public SubscribeAttribute(string key)
        {
            this.key = key;
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
        public static void Subscribe(MonoFlux monoflux, bool condition)
        {
            CheckMonoFlux(monoflux); // ~First Check Monoflux Implementation
            List<MethodInfo> methods = m_monofluxes[monoflux];
            for (int i = 0; i < methods.Count; i++)
            {
                // var atr = m_methods[methods[i]];
                // Debug.Log(atr.key);
                // mInfo.Get_MethodName_Subscribe().Get_Method_Subscribe(mInfo).Invoke(
                //     null, 
                //     new object[]
                //     {
                //         condition,
                //         atrb.Key,
                //         mInfo.__Get_Delegate(instance)
                //     }   
                // );
            }
        }
    }
}
