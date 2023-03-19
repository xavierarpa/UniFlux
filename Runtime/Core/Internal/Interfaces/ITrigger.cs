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
namespace Kingdox.Flux.Core.Internal
{
    // <summary>
    //  Trigger TKey
    // <summary>
    public interface ITrigger<in TKey> // TKey
    {
        void Trigger(TKey key);
    }
    // <summary>
    //  TKey TParam
    // <summary>
    public interface ITriggerParam<in TKey, in TParam> // TKey TParam
    {
        void Trigger(TKey key, TParam param);
    }
    // <summary>
    //  TKey TReturn
    // <summary>
    public interface ITriggerReturn<in TKey, out TReturn> // TKey TReturn
    {
        TReturn Trigger(TKey key);
    }
    // <summary>
    //  TKey TParam TReturn
    // <summary>
    public interface ITriggerParamReturn<in TKey, in TParam, out TReturn> // TKey TParam TReturn
    {
        TReturn Trigger(TKey key, TParam param);
    }
}
