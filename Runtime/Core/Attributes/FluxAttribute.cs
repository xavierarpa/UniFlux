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
namespace UniFlux
{
    ///<summary>
    /// Base class to detect the Target as an Item to being handled
    /// AllowMultiple is false to keep legibility
    ///</summary>
    [Obsolete("Naming has been changed, use instead [MethodFlux]")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class FluxAttribute : Attribute
    {
        ///<summary>
        /// Key provided to handle the subscription
        ///</summary>
        public readonly object key;
        ///<summary>
        /// Constructor of the FluxAttribute class that takes a key as a parameter.
        ///</summary>
        public FluxAttribute(object key)
        {
            this.key = key;
        }
    }
}
//TODO: C# 11 allow Attribute<T>, instead of object key