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
namespace Kingdox.UniFlux.Core.Internal
{
    /// <summary>
    ///  TKey
    /// </summary>
    internal interface IFlux<in TKey, in TStorage>:  IStore<TKey, TStorage>
    {
        /// <summary>
        ///  Dispatch the TKey
        /// </summary>
        void Dispatch(TKey key);
    }
    /// <summary>
    ///  TKey TParam
    /// </summary>
    internal interface IFluxParam<in TKey, in TParam, in TStorage> : IStore<TKey, TStorage>
    {
        /// <summary>
        ///  Dispatch the TKey with TParam
        /// </summary>
        void Dispatch(TKey key, TParam param);
    }
    /// <summary>
    ///  TKey TReturn
    /// </summary>
    internal interface IFluxReturn<in TKey, out TReturn, in TStorage> : IStore<TKey, TStorage>
    {
        /// <summary>
        ///  Dispatch the TKey and return TReturn
        /// </summary>
        TReturn Dispatch(TKey key);   
    }
    /// <summary>
    ///  TKey TParam TReturn
    /// </summary>
    internal interface IFluxParamReturn<in TKey, in TParam, out TReturn, in TStorage> : IStore<TKey, TStorage>
    {
        /// <summary>
        ///  Dispatch the TKey with TParam and return TReturn
        /// </summary>
        TReturn Dispatch(TKey key, TParam param);
    }
}