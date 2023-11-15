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
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Kingdox.UniFlux.Core.Internal;
namespace Kingdox.UniFlux
{
    ///<summary>
    /// static class that ensure to handle the FluxAttribute
    ///</summary>
    internal static class MonoFluxExtension
    {
        internal static readonly BindingFlags m_bindingflag_all = (BindingFlags)(-1);
        //
        internal static readonly Type m_type_monoflux = typeof(MonoFlux);
        //
        internal static readonly Type m_type_flux = typeof(Core.Internal.Flux<>);
        internal static readonly Type m_type_flux_delegate = typeof(Action);
        internal static readonly string m_type_flux_method = nameof(Core.Internal.Flux<object>.Store);
        //
        internal static readonly Type m_type_fluxparam = typeof(Core.Internal.FluxParam<,>);
        internal static readonly Type m_type_fluxparam_state = typeof(Core.Internal.FluxState<,>);
        internal static readonly Type m_type_fluxparam_delegate = typeof(Action<>);
        internal static readonly string m_type_fluxparam_method = nameof(Core.Internal.FluxParam<object,object>.Store);
        internal static readonly string m_type_fluxparam_method_state = nameof(Core.Internal.FluxState<object,object>.Store);
        //
        internal static readonly Type m_type_fluxreturn = typeof(Core.Internal.FluxReturn<,>);
        internal static readonly Type m_type_fluxreturn_delegate = typeof(Func<>);
        internal static readonly string m_type_fluxreturn_method = nameof(Core.Internal.FluxReturn<object,object>.Store);
        //
        internal static readonly Type m_type_fluxparamreturn = typeof(Core.Internal.FluxParamReturn<,,>);
        internal static readonly Type m_type_fluxparamreturn_delegate = typeof(Func<,>);
        internal static readonly string m_type_fluxparamreturn_method = nameof(Core.Internal.FluxParamReturn<object,object,object>.Store);
        //
        ///<summary>
        /// typeof(void)
        ///</summary>
        internal static readonly Type m_type_void = typeof(void);
        ///<summary>
        /// Dictionary to cache each MonoFlux instance's methods
        ///</summary>
        internal static readonly Dictionary<MonoFlux, List<MethodInfo>> m_monofluxes = new Dictionary<MonoFlux, List<MethodInfo>>();
        ///<summary>
        /// Dictionary to cache the FluxAttribute of each MethodInfo
        ///</summary>
        internal static readonly Dictionary<MethodInfo, FluxAttribute> m_methods = new Dictionary<MethodInfo, FluxAttribute>();
        ///<summary>
        /// Allows subscribe methods using `FluxAttribute` by reflection
        /// ~ where magic happens ~
        ///</summary>
        internal static void Subscribe(this MonoFlux monoflux, in bool condition)
        {
            if (!m_monofluxes.ContainsKey(monoflux))
            {
                m_monofluxes.Add(
                    monoflux, 
                    monoflux.gameObject.GetComponent(m_type_monoflux).GetType().GetMethods(m_bindingflag_all).Where(method => 
                    {
                        if(System.Attribute.GetCustomAttributes(method).FirstOrDefault((_att) => _att is FluxAttribute) is FluxAttribute _attribute)
                        {
                            if(!m_methods.ContainsKey(method)) m_methods.Add(method, _attribute); // ADD <Method, Attribute>!
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

                if(m_methods[methods[i]] is StateFluxAttribute _stateFlux ) // IsState ?
                {
                    switch ((_Parameters.Length.Equals(1), !methods[i].ReturnType.Equals(m_type_void)))
                    {
                        case (true, false): 
                            m_type_fluxparam_state
                                .MakeGenericType(m_methods[methods[i]].key.GetType(), _Parameters[0].ParameterType)
                                .GetMethod(m_type_fluxparam_method_state, m_bindingflag_all)
                                .Invoke( null, new object[]{ m_methods[methods[i]].key, methods[i].CreateDelegate(m_type_fluxparam_delegate.MakeGenericType(_Parameters[0].ParameterType), monoflux), condition})
                            ;
                        break;
                        case (false, false): throw new Exception($"Error '{methods[i].Name}' : not treated as StateFluxAttribute, Add Parameter");
                        case (true, true):  throw new Exception($"Error '{methods[i].Name}' : not treated as StateFluxAttribute, Remove Return value");
                        case (false, true): throw new Exception($"Error '{methods[i].Name}' : not treated as StateFluxAttribute, Add Parameter, Remove Return value");
                        default : throw new Exception($"Error '{methods[i].Name}' : not treated as StateFluxAttribute");
                    }
                }
                else // IsNormal ?
                {
                    switch ((_Parameters.Length.Equals(1), !methods[i].ReturnType.Equals(m_type_void)))
                    {
                        case (false, false): // Flux
                            m_type_flux
                                .MakeGenericType(m_methods[methods[i]].key.GetType())
                                .GetMethod(m_type_flux_method, m_bindingflag_all)
                                .Invoke( null, new object[]{ m_methods[methods[i]].key, methods[i].CreateDelegate(m_type_flux_delegate, monoflux), condition})
                            ;
                        break;
                        case (true, false): // FluxParam
                            m_type_fluxparam
                                .MakeGenericType(m_methods[methods[i]].key.GetType(), _Parameters[0].ParameterType)
                                .GetMethod(m_type_fluxparam_method, m_bindingflag_all)
                                .Invoke( null, new object[]{ m_methods[methods[i]].key, methods[i].CreateDelegate(m_type_fluxparam_delegate.MakeGenericType(_Parameters[0].ParameterType), monoflux), condition})
                            ;
                        break;
                        case (false, true): //FluxReturn
                            m_type_fluxreturn
                                .MakeGenericType(m_methods[methods[i]].key.GetType(), methods[i].ReturnType)
                                .GetMethod(m_type_fluxreturn_method, m_bindingflag_all)
                                .Invoke( null, new object[]{ m_methods[methods[i]].key, methods[i].CreateDelegate(m_type_fluxreturn_delegate.MakeGenericType(methods[i].ReturnType), monoflux), condition})
                            ;
                        break;
                        case (true, true): //FluxParamReturn
                            m_type_fluxparamreturn
                                .MakeGenericType(m_methods[methods[i]].key.GetType(), _Parameters[0].ParameterType, methods[i].ReturnType)
                                .GetMethod(m_type_fluxparamreturn_method, m_bindingflag_all)
                                .Invoke( null, new object[]{ m_methods[methods[i]].key, methods[i].CreateDelegate(m_type_fluxparamreturn_delegate.MakeGenericType(_Parameters[0].ParameterType, methods[i].ReturnType), monoflux), condition})
                            ;
                        break;
                    }
                }
            }
        }
        // internal static void Subscribe_v2(this MonoFlux monoflux, in bool condition)
        // {
        //     var methods = new List<(MethodInfo Method, FluxAttribute Attribute)>();
        //     var methods_raw  = monoflux.GetType().GetMethods(m_bindingflag_all);
        //     foreach (var method in methods_raw)
        //     {
        //         var attribute = method.GetCustomAttribute<FluxAttribute>();
        //         if (attribute != null)
        //         {
        //             #if UNITY_EDITOR
        //             if (method.GetParameters().Length > 1)
        //             {
        //                 throw new System.Exception($"Error '{method.Name}' : Theres more than one parameter, please set 1 or 0 parameter. (if you need to add more than 1 argument use Tuples or create a struct, record o class...)");
        //             }
        //             #endif
        //             methods.Add((method, attribute));
        //         }
        //     }

        //     foreach (var (method, attribute) in methods)
        //     {
        //         var parameters = method.GetParameters();
        //         var returnType = method.ReturnType;

        //         switch ((parameters.Length == 1, returnType != m_type_void))
        //         {
        //             case (false, false):
        //                 m_type_flux.MakeGenericType(attribute.key.GetType())
        //                     .GetMethod(m_type_flux_method, m_bindingflag_all)
        //                     .Invoke(null, new object[] { attribute.key, Delegate.CreateDelegate(m_type_flux_delegate, monoflux, method), condition });
        //                 break;
        //             case (true, false):
        //                 m_type_fluxparam.MakeGenericType(attribute.key.GetType(), parameters[0].ParameterType)
        //                     .GetMethod(m_type_fluxparam_method, m_bindingflag_all)
        //                     .Invoke(null, new object[] { attribute.key, Delegate.CreateDelegate(m_type_fluxparam_delegate.MakeGenericType(parameters[0].ParameterType), monoflux, method), condition });
        //                 break;
        //             case (false, true):
        //                 m_type_fluxreturn.MakeGenericType(attribute.key.GetType(), returnType)
        //                     .GetMethod(m_type_fluxreturn_method, m_bindingflag_all)
        //                     .Invoke(null, new object[] { attribute.key, Delegate.CreateDelegate(m_type_fluxreturn_delegate.MakeGenericType(returnType), monoflux, method), condition });
        //                 break;
        //             case (true, true):
        //                 m_type_fluxparamreturn.MakeGenericType(attribute.key.GetType(), parameters[0].ParameterType, returnType)
        //                     .GetMethod(m_type_fluxparamreturn_method, m_bindingflag_all)
        //                     .Invoke(null, new object[] { attribute.key, Delegate.CreateDelegate(m_type_fluxparamreturn_delegate.MakeGenericType(parameters[0].ParameterType, returnType), monoflux, method), condition });
        //                 break;
        //         }
        //     }
        // }
        // internal static void Subscribe_v3(this MonoFlux monoflux, in bool condition)
        // {
        //     var methods_raw = monoflux.GetType().GetMethods(m_bindingflag_all);
        //     var methods = new (MethodInfo Method, FluxAttribute Attribute)[methods_raw.Length];
        //     var method_count = 0;

        //     for (int i = 0; i < methods_raw.Length; i++)
        //     {
        //         var attribute = methods_raw[i].GetCustomAttribute<FluxAttribute>();
        //         if (attribute != null)
        //         {
        //             #if UNITY_EDITOR
        //             if (methods_raw[i].GetParameters().Length > 1) throw new System.Exception($"Error '{methods_raw[i].Name}' : Theres more than one parameter, please set 1 or 0 parameter. (if you need to add more than 1 argument use Tuples or create a struct, record o class...)");
        //             #endif
        //             methods[method_count++] = (methods_raw[i], attribute);
        //         }
        //     }

        //     for (int i = 0; i < method_count; i++)
        //     {
        //         var method = methods[i].Method;
        //         var attribute = methods[i].Attribute;
        //         var parameters = method.GetParameters();
        //         var returnType = method.ReturnType;

        //         switch ((parameters.Length == 1, returnType != m_type_void))
        //         {
        //             case (false, false):
        //                 m_type_flux.MakeGenericType(attribute.key.GetType())
        //                     .GetMethod(m_type_flux_method, m_bindingflag_all)
        //                     .Invoke(null, new object[] { attribute.key, Delegate.CreateDelegate(m_type_flux_delegate, monoflux, method), condition }.ToArray());
        //                 break;
        //             case (true, false):
        //                 m_type_fluxparam.MakeGenericType(attribute.key.GetType(), parameters[0].ParameterType)
        //                     .GetMethod(m_type_fluxparam_method, m_bindingflag_all)
        //                     .Invoke(null, new object[] { attribute.key, Delegate.CreateDelegate(m_type_fluxparam_delegate.MakeGenericType(parameters[0].ParameterType), monoflux, method), condition }.ToArray());
        //                 break;
        //             case (false, true):
        //                 m_type_fluxreturn.MakeGenericType(attribute.key.GetType(), returnType)
        //                     .GetMethod(m_type_fluxreturn_method, m_bindingflag_all)
        //                     .Invoke(null, new object[] { attribute.key, Delegate.CreateDelegate(m_type_fluxreturn_delegate.MakeGenericType(returnType), monoflux, method), condition }.ToArray());
        //                 break;
        //             case (true, true):
        //                 m_type_fluxparamreturn.MakeGenericType(attribute.key.GetType(), parameters[0].ParameterType, returnType)
        //                     .GetMethod(m_type_fluxparamreturn_method, m_bindingflag_all)
        //                     .Invoke(null, new object[] { attribute.key, Delegate.CreateDelegate(m_type_fluxparamreturn_delegate.MakeGenericType(parameters[0].ParameterType, returnType), monoflux, method), condition }.ToArray());
        //                 break;
        //         }
        //     }
        // }
        // internal static void Subscribe_v4(this MonoFlux monoflux, in bool condition)
        // {
        //     var methods_raw = monoflux.GetType().GetMethods(m_bindingflag_all);
        //     var methods = new (MethodInfo Method, FluxAttribute Attribute)[methods_raw.Length];
        //     var method_count = 0;
        //     for (int i = 0; i < methods_raw.Length; i++)
        //     {
        //         var attribute = methods_raw[i].GetCustomAttribute<FluxAttribute>();
        //         if (attribute != null)
        //         {
        //             methods[method_count++] = (methods_raw[i], attribute);
        //         }
        //     }
        //     for (int i = 0; i < method_count; i++)
        //     {
        //         var method = methods[i].Method;
        //         var attribute = methods[i].Attribute;
        //         var parameters = method.GetParameters();
        //         var returnType = method.ReturnType;

        //         switch ((parameters.Length == 1, returnType != m_type_void))
        //         {
        //             case (false, false):
        //                 var genericType = m_type_flux.MakeGenericType(attribute.key.GetType());
        //                 var methodInfo = genericType.GetMethod(m_type_flux_method, m_bindingflag_all);
        //                 var delegateType = m_type_flux_delegate;
        //                 var delegateMethod = Delegate.CreateDelegate(delegateType, monoflux, method);
        //                 var arguments = new object[] { attribute.key, delegateMethod, condition };
        //                 methodInfo.Invoke(null, arguments);
        //                 break;
        //             case (true, false):
        //                 genericType = m_type_fluxparam.MakeGenericType(attribute.key.GetType(), parameters[0].ParameterType);
        //                 methodInfo = genericType.GetMethod(m_type_fluxparam_method, m_bindingflag_all);
        //                 delegateType = m_type_fluxparam_delegate.MakeGenericType(parameters[0].ParameterType);
        //                 delegateMethod = Delegate.CreateDelegate(delegateType, monoflux, method);
        //                 arguments = new object[] { attribute.key, delegateMethod, condition };
        //                 methodInfo.Invoke(null, arguments);
        //                 break;
        //             case (false, true):
        //                 genericType = m_type_fluxreturn.MakeGenericType(attribute.key.GetType(), returnType);
        //                 methodInfo = genericType.GetMethod(m_type_fluxreturn_method, m_bindingflag_all);
        //                 delegateType = m_type_fluxreturn_delegate.MakeGenericType(returnType);
        //                 delegateMethod = Delegate.CreateDelegate(delegateType, monoflux, method);
        //                 arguments = new object[] { attribute.key, delegateMethod, condition };
        //                 methodInfo.Invoke(null, arguments);
        //                 break;
        //             case (true, true):
        //                 genericType = m_type_fluxparamreturn.MakeGenericType(attribute.key.GetType(), parameters[0].ParameterType, returnType);
        //                 methodInfo = genericType.GetMethod(m_type_fluxparamreturn_method, m_bindingflag_all);
        //                 delegateType = m_type_fluxparamreturn_delegate.MakeGenericType(parameters[0].ParameterType, returnType);
        //                 delegateMethod = Delegate.CreateDelegate(delegateType, monoflux, method);
        //                 arguments = new object[] { attribute.key, delegateMethod, condition };
        //                 methodInfo.Invoke(null, arguments);
        //                 break;
        //         }
        //     }
        // }
    }
}