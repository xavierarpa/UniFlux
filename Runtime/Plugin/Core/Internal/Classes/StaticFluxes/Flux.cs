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
using System;
using System.Collections.Generic;
namespace UniFlux.Core.Internal
{
    ///<summary>
    /// static Flux aspects
    ///</summary>
    public static class Flux
    {
        public static Type TYPE_Flux_T = typeof(Flux<>);
        public static Type TYPE_FluxParam_T_T2 = typeof(FluxParam<,>);
        public static Type TYPE_FluxReturn_T_T2 = typeof(FluxReturn<,>);
        public static Type TYPE_FluxParamReturn_T_T2_T3 = typeof(FluxParamReturn<,,>);
        public static Type TYPE_FluxState_T_T2 = typeof(FluxState<,>);
        
        public static Dictionary<Type, string> DICTIONARY_Flux_Databases = new Dictionary<Type, string>()
        {
            [TYPE_Flux_T] = "flux_action",
            [TYPE_FluxParam_T_T2] = "flux_action_param",
            [TYPE_FluxReturn_T_T2] = "flux_func",
            [TYPE_FluxParamReturn_T_T2_T3] = "flux_func_param",
            [TYPE_FluxState_T_T2] = "flux_action_param",
        };
        public static Dictionary<Type, string> DICTIONARY_Flux_Databases_Dic = new Dictionary<Type, string>()
        {
            [TYPE_Flux_T] = "dictionary",
            [TYPE_FluxParam_T_T2] = "dictionary",
            [TYPE_FluxReturn_T_T2] = "dictionary",
            [TYPE_FluxParamReturn_T_T2_T3] = "dictionary",
            [TYPE_FluxState_T_T2] = "dictionary",
        };

        public static Action OnAddFluxType;
        public static readonly List<Type> List_FluxTypes = new List<Type>();
        internal static void AddFluxType(Type fluxType)
        {
            List_FluxTypes.Add(fluxType);
            OnAddFluxType.Invoke();
        }
    }
}