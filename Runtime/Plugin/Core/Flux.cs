using System;
namespace UniFlux.Core
{
    /// <summary>
    /// Central dispatcher class for the UniFlux architecture.
    /// Provides static methods for storing, dispatching, and managing flux actions and state.
    /// </summary>
    /// <remarks>
    /// UniFlux follows the Flux pattern with unidirectional data flow:
    /// - Store methods register callbacks for specific keys
    /// - Dispatch methods trigger callbacks associated with keys
    /// - State methods provide persistent state management
    /// </remarks>
    public static class Flux 
    {
#region // Flux
        /// <summary>
        /// Stores or removes an action callback for a specific key.
        /// </summary>
        /// <typeparam name="T">The type of the key</typeparam>
        /// <param name="key">The key to associate with the callback</param>
        /// <param name="callback">The action to execute when the key is dispatched</param>
        /// <param name="condition">True to store the callback, false to remove it</param>
        public static void Store<T>(in T key, in Action callback, in bool condition) => Internal.Flux<T>.Store(in key, in callback, in condition);
        
        /// <summary>
        /// Dispatches an action for the specified key, triggering all associated callbacks.
        /// </summary>
        /// <typeparam name="T">The type of the key</typeparam>
        /// <param name="key">The key to dispatch</param>
        public static void Dispatch<T>(in T key) => Internal.Flux<T>.Dispatch(in key);
        // public static void Store<T>(in T key, in Action callback, in bool condition) => Internal.Flux<T>.actionFlux.Store(in condition, key, in callback);
        // public static void Dispatch<T>(in T key) => Internal.Flux<T>.actionFlux.Dispatch(key);
#endregion
#region // FluxParam
        /// <summary>
        /// Stores or removes a parameterized action callback for a specific key.
        /// </summary>
        /// <typeparam name="T">The type of the key</typeparam>
        /// <typeparam name="T2">The type of the parameter</typeparam>
        /// <param name="key">The key to associate with the callback</param>
        /// <param name="callback">The action to execute when the key is dispatched</param>
        /// <param name="condition">True to store the callback, false to remove it</param>
        public static void Store<T,T2>(in T key, in Action<T2> callback, in bool condition) => Internal.FluxParam<T,T2>.Store(in key, in callback, in condition);
        
        /// <summary>
        /// Dispatches a parameterized action for the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the key</typeparam>
        /// <typeparam name="T2">The type of the parameter</typeparam>
        /// <param name="key">The key to dispatch</param>
        /// <param name="param">The parameter to pass to callbacks</param>
        public static void Dispatch<T, T2>(in T key, in T2 @param) => Internal.FluxParam<T,T2>.Dispatch(in key, in @param);
#endregion
#region // FluxReturn
        /// <summary>
        /// Stores or removes a function callback that returns a value for a specific key.
        /// </summary>
        /// <typeparam name="T">The type of the key</typeparam>
        /// <typeparam name="T2">The type of the return value</typeparam>
        /// <param name="key">The key to associate with the callback</param>
        /// <param name="callback">The function to execute when the key is dispatched</param>
        /// <param name="condition">True to store the callback, false to remove it</param>
        public static void Store<T,T2>(in T key, in Func<T2> callback, in bool condition) => Internal.FluxReturn<T,T2>.Store(in key, in callback, in condition);
        
        /// <summary>
        /// Dispatches a function for the specified key and returns the result.
        /// </summary>
        /// <typeparam name="T">The type of the key</typeparam>
        /// <typeparam name="T2">The type of the return value</typeparam>
        /// <param name="key">The key to dispatch</param>
        /// <returns>The result from the associated function</returns>
        public static T2 Dispatch<T, T2>(in T key) => Internal.FluxReturn<T,T2>.Dispatch(in key);
#endregion
#region // FluxParamReturn
        /// <summary>
        /// Stores or removes a parameterized function callback for a specific key.
        /// </summary>
        /// <typeparam name="T">The type of the key</typeparam>
        /// <typeparam name="T2">The type of the parameter</typeparam>
        /// <typeparam name="T3">The type of the return value</typeparam>
        /// <param name="key">The key to associate with the callback</param>
        /// <param name="callback">The function to execute when the key is dispatched</param>
        /// <param name="condition">True to store the callback, false to remove it</param>
        public static void Store<T, T2, T3>(in T key, in Func<T2, T3> callback, in bool condition) => Internal.FluxParamReturn<T,T2,T3>.Store(in key, in callback, in condition);
        
        /// <summary>
        /// Dispatches a parameterized function for the specified key and returns the result.
        /// </summary>
        /// <typeparam name="T">The type of the key</typeparam>
        /// <typeparam name="T2">The type of the parameter</typeparam>
        /// <typeparam name="T3">The type of the return value</typeparam>
        /// <param name="key">The key to dispatch</param>
        /// <param name="param">The parameter to pass to the function</param>
        /// <returns>The result from the associated function</returns>
        public static T3 Dispatch<T, T2, T3>(in T key, in T2 @param) => Internal.FluxParamReturn<T,T2,T3>.Dispatch(in key, in @param);
#endregion
#region // FluxState
        /// <summary>
        /// Stores or removes a state callback for a specific key.
        /// State callbacks are invoked immediately if the state already exists.
        /// </summary>
        /// <typeparam name="T">The type of the key</typeparam>
        /// <typeparam name="T2">The type of the state value</typeparam>
        /// <param name="key">The key to associate with the callback</param>
        /// <param name="callback">The action to execute when the state changes</param>
        /// <param name="condition">True to store the callback, false to remove it</param>
        public static void StoreState<T,T2>(in T key, in Action<T2> callback, in bool condition) => Internal.FluxState<T,T2>.Store(in key, in callback, in condition);
        
        /// <summary>
        /// Dispatches a state change for the specified key.
        /// Only triggers callbacks if the new value is different from the current state.
        /// </summary>
        /// <typeparam name="T">The type of the key</typeparam>
        /// <typeparam name="T2">The type of the state value</typeparam>
        /// <param name="key">The key to dispatch</param>
        /// <param name="param">The new state value</param>
        public static void DispatchState<T, T2>(in T key, in T2 @param) => Internal.FluxState<T,T2>.Dispatch(in key, in @param);
        
        /// <summary>
        /// Retrieves the current state value for the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the key</typeparam>
        /// <typeparam name="T2">The type of the state value</typeparam>
        /// <param name="key">The key to get the state for</param>
        /// <param name="state">When this method returns, contains the state value if found</param>
        /// <returns>True if the state exists, false otherwise</returns>
        public static bool GetState<T, T2>(in T key, out T2 @state) => Internal.FluxState<T,T2>.Get(in key, out @state);
#endregion
    }
}