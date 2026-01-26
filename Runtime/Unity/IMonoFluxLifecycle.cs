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
namespace UniFlux
{
    /// <summary>
    /// Optional interface that MonoFlux-derived classes can implement to receive lifecycle callbacks.
    /// This provides an alternative to overriding OnEnable/OnDisable, ensuring that the subscription
    /// mechanism is always called correctly without requiring manual base method calls.
    /// 
    /// <para>
    /// When a class implements this interface, <see cref="OnEnableCallback"/> is called after 
    /// subscription is completed in OnEnable, and <see cref="OnDisableCallback"/> is called after
    /// unsubscription in OnDisable.
    /// </para>
    /// </summary>
    /// <example>
    /// <code>
    /// public class MyComponent : MonoFlux, IMonoFluxLifecycle
    /// {
    ///     public void OnEnableCallback()
    ///     {
    ///         // Called after subscription - safe to use without calling base.OnEnable()
    ///         Debug.Log("Enabled!");
    ///     }
    ///     
    ///     public void OnDisableCallback()
    ///     {
    ///         // Called after unsubscription - safe to use without calling base.OnDisable()
    ///         Debug.Log("Disabled!");
    ///     }
    /// }
    /// </code>
    /// </example>
    public interface IMonoFluxLifecycle
    {
        /// <summary>
        /// Called after the MonoFlux subscription is completed in OnEnable.
        /// This method is called automatically if the class implements IMonoFluxLifecycle.
        /// </summary>
        void OnEnableCallback();
        
        /// <summary>
        /// Called after the MonoFlux unsubscription is completed in OnDisable.
        /// This method is called automatically if the class implements IMonoFluxLifecycle.
        /// </summary>
        void OnDisableCallback();
    }
}
