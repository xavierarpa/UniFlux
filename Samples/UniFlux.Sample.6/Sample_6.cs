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
namespace UniFlux.Sample
{
    /// <summary>
    /// Sample_6 demonstrates the IMonoFluxLifecycle interface approach.
    /// 
    /// This sample shows how to use the IMonoFluxLifecycle interface to receive
    /// lifecycle callbacks (OnEnableCallback, OnDisableCallback) without needing 
    /// to override OnEnable/OnDisable or worry about calling base methods.
    /// 
    /// This approach ensures the subscription mechanism is always called correctly,
    /// even if users forget to call base.OnEnable() or base.OnDisable().
    /// </summary>
    public sealed class Sample_6 : MonoFlux, IMonoFluxLifecycle
    {
        [SerializeField] private int enableCount;
        [SerializeField] private int disableCount;
        
        /// <summary>
        /// Called automatically after subscription is completed.
        /// No need to call base.OnEnable() - subscriptions are already handled!
        /// </summary>
        public void OnEnableCallback()
        {
            enableCount++;
            Debug.Log($"Sample_6: OnEnableCallback called! (Enable count: {enableCount})");
        }
        
        /// <summary>
        /// Called automatically after unsubscription is completed.
        /// No need to call base.OnDisable() - unsubscriptions are already handled!
        /// </summary>
        public void OnDisableCallback()
        {
            disableCount++;
            Debug.Log($"Sample_6: OnDisableCallback called! (Disable count: {disableCount})");
        }
        
        private void Start()
        {
            "Sample_6_Event".Dispatch();
        }
        
        [MethodFlux("Sample_6_Event")] 
        private void OnSample6Event()
        {
            Debug.Log("Sample_6: Event received! Subscription is working correctly.");
        }
    }
}
