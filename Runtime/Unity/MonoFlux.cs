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
using UnityEngine;
namespace UniFlux
{
    /// <summary>
    /// The `MonoFlux` class is a base class that should be used for all Unity scripts that need to respond to changes in a flux state. 
    /// It provides a helper method for subscribing and unsubscribing to flux state updates, and a virtual method that can be overriden to handle changes in subscription state. 
    /// 
    /// <para>
    /// <b>Lifecycle Options:</b>
    /// <list type="bullet">
    ///   <item><description>Override <see cref="OnFlux(in bool)"/> to react to subscription state changes.</description></item>
    ///   <item><description>Implement <see cref="IMonoFluxLifecycle"/> interface for separate OnEnableCallback/OnDisableCallback methods.</description></item>
    ///   <item><description>Override OnEnable/OnDisable directly (remember to call base.OnEnable()/base.OnDisable()).</description></item>
    /// </list>
    /// </para>
    /// </summary>
    [DisallowMultipleComponent] public abstract partial class MonoFlux : MonoBehaviour
    {
        /// <summary>
        /// Cached reference to IMonoFluxLifecycle interface if implemented by derived class.
        /// </summary>
        private IMonoFluxLifecycle _lifecycleCallbacks;
        
        /// <summary>
        /// Flag to track if we've checked for the lifecycle interface.
        /// </summary>
        private bool _lifecycleChecked;
        
        /// <summary>
        /// Called when the script instance is being enabled.
        /// If overriding this method, you MUST call base.OnEnable() to ensure proper subscription.
        /// Alternatively, implement <see cref="IMonoFluxLifecycle"/> to receive callbacks without needing to call base.
        /// </summary>
        protected virtual void OnEnable()  => OnSubscription(true);
        /// <summary>
        /// Called when the script instance is being disabled.
        /// If overriding this method, you MUST call base.OnDisable() to ensure proper unsubscription.
        /// Alternatively, implement <see cref="IMonoFluxLifecycle"/> to receive callbacks without needing to call base.
        /// </summary>
        protected virtual void OnDisable()  => OnSubscription(false);
        /// <summary>
        /// Helper method to subscribe or unsubscribe from the flux state updates.
        /// </summary>
        /// <param name="condition">Whether to subscribe or unsubscribe.</param>
        private void OnSubscription(bool condition)
        {
            // Subscribe or unsubscribe from flux state updates
            Utils.SubscribeAttributes(this, in condition);
            // Call OnFlux method with the new subscription state
            OnFlux(in condition);
            // Call lifecycle callbacks if implemented
            InvokeLifecycleCallback(condition);
        }
        
        /// <summary>
        /// Invokes lifecycle callbacks if the derived class implements IMonoFluxLifecycle.
        /// </summary>
        /// <param name="condition">Whether enabling (true) or disabling (false).</param>
        private void InvokeLifecycleCallback(bool condition)
        {
            // Cache the interface reference on first call
            if (!_lifecycleChecked)
            {
                _lifecycleCallbacks = this as IMonoFluxLifecycle;
                _lifecycleChecked = true;
            }
            
            // Invoke the appropriate callback if interface is implemented
            if (_lifecycleCallbacks != null)
            {
                if (condition)
                    _lifecycleCallbacks.OnEnableCallback();
                else
                    _lifecycleCallbacks.OnDisableCallback();
            }
        }
        
        /// <summary>
        /// Override this method to react to changes in subscription state.
        /// This is called after subscription/unsubscription is complete.
        /// </summary>
        /// <param name="condition">Whether the object is being subscribed (true) or unsubscribed (false).</param>
        protected virtual void OnFlux(in bool condition) { }
    }
}
