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
namespace Kingdox.UniFlux.Core
{
    public static partial class Flux // TKey (Action)
    {
        public static void SubscribeAction(in string key, in Action action, in bool condition) => Internal.Flux<string>.SubscribeAction(in key, in action, in condition);
        public static void TriggerAction(in string key) => Internal.Flux<string>.TriggerAction(in key);
    }
    public static partial class Flux<T> // TKey TParam (Action<T>)
    {
        public static void SubscribeActionParam(in string key, in Action<T> action, in bool condition) => Internal.Flux<string,T>.SubscribeActionParam(in key, in action, in condition);
        public static void TriggerActionParam(in string key,in T @param) => Internal.Flux<string,T>.TriggerActionParam(in key, in @param);
    }
    public static partial class Flux<T> // TKey TReturn (Func<out T>)
    {
        public static void SubscribeFunc(in string key, in Func<T> action, in bool condition) => Internal.Flux<string,T>.SubscribeFunc(in key, in action, in condition);
        public static T TriggerFunc(in string key) => Internal.Flux<string, T>.TriggerFunc(in key);
    }
    public static partial class Flux<T,T2> // TKey TReturn (Func<T, out T>)
    {
        public static void SubscribeFuncParam(in string key, in Func<T, T2> action, in bool condition) => Internal.Flux<string,T,T2>.SubscribeFuncParam(in key, in action, in condition);
        public static T2 TriggerFuncParam(in string key,in T @param) => Internal.Flux<string,T,T2>.TriggerFuncParam(in key, in @param);
    }    
}
