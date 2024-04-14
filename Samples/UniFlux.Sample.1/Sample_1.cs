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
    /// In Sample_1 we explain how to implement easily. 
    /// In this case we only need to inherit from MonoFlux and include MethodFlux Attribute in the selected method
    /// Then we can use Dispatch extension methods to call the methods subscribed with the specified key, in this case "Sample_1"
    /// </summary>
    public sealed class Sample_1 : MonoFlux
    {
        /// <summary>
        /// Used as an example for calling methods subscribed with key "Sample_1"
        /// </summary>
        private void Start() 
        {
            "Sample_1".Dispatch();
        }
        /// <summary>
        /// Used as an example method subscribed with key "Sample_1"
        /// </summary>
        [MethodFlux("Sample_1")] private void OnDispatchKeySample_1()
        {
            Debug.Log("Sample_1 called !");
        }
    }
}