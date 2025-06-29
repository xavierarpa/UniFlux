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
    /// </summary>
    [DisallowMultipleComponent] public abstract partial class MonoFlux : MonoBehaviour
    {
        /// <summary>
        /// Called when the script instance is being enabled.
        /// </summary>
        protected virtual void OnEnable()  => OnSubscription(true);
        /// <summary>
        /// Called when the script instance is being disabled.
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
        }
        /// <summary>
        /// Override this method to react to changes in subscription state.
        /// </summary>
        /// <param name="condition">Whether the object is being subscribed or unsubscribed.</param>
        protected virtual void OnFlux(in bool condition) { }
    }
}
