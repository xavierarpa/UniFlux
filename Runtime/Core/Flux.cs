using System;
namespace UniFlux.Core
{
    public static class Flux 
    {
#region // Flux
        public static void Store<T>(in T key, in Action callback, in bool condition) => Internal.Flux<T>.Store(in key, in callback, in condition);
        public static void Dispatch<T>(in T key) => Internal.Flux<T>.Dispatch(in key);
        // public static void Store<T>(in T key, in Action callback, in bool condition) => Internal.Flux<T>.actionFlux.Store(in condition, key, in callback);
        // public static void Dispatch<T>(in T key) => Internal.Flux<T>.actionFlux.Dispatch(key);
#endregion
#region // FluxParam
        public static void Store<T,T2>(in T key, in Action<T2> callback, in bool condition) => Internal.FluxParam<T,T2>.Store(in key, in callback, in condition);
        public static void Dispatch<T, T2>(in T key, in T2 @param) => Internal.FluxParam<T,T2>.Dispatch(in key, in @param);
#endregion
#region // FluxReturn
        public static void Store<T,T2>(in T key, in Func<T2> callback, in bool condition) => Internal.FluxReturn<T,T2>.Store(in key, in callback, in condition);
        public static T2 Dispatch<T, T2>(in T key) => Internal.FluxReturn<T,T2>.Dispatch(in key);
#endregion
#region // FluxParamReturn
        public static void Store<T, T2, T3>(in T key, in Func<T2, T3> callback, in bool condition) => Internal.FluxParamReturn<T,T2,T3>.Store(in key, in callback, in condition);
        public static T3 Dispatch<T, T2, T3>(in T key, in T2 @param) => Internal.FluxParamReturn<T,T2,T3>.Dispatch(in key, in @param);
#endregion
#region // FluxState
        public static void StoreState<T,T2>(in T key, in Action<T2> callback, in bool condition) => Internal.FluxState<T,T2>.Store(in key, in callback, in condition);
        public static void DispatchState<T, T2>(in T key, in T2 @param) => Internal.FluxState<T,T2>.Dispatch(in key, in @param);
        public static bool GetState<T, T2>(in T key, out T2 @state) => Internal.FluxState<T,T2>.Get(in key, out @state);
#endregion
    }
}