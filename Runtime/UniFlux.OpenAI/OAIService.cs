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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kingdox.UniFlux;
using OpenAI;
namespace Kingdox.UniFlux.OpenAI
{
    public static partial class OAIService // Data
    {
        internal static partial class Data
        {
            internal static partial class Model
            {
                internal const string text_davinci_003 = "text-davinci-003";
                internal const string gpt_3_5_turbo = "gpt-3.5-turbo";
                internal const string gpt_3_5_turbo_0301 = "gpt-3.5-turbo-0301";
            }
        }
    }
    public static partial class OAIService // Key
    {
        public static partial class Key
        {
            private const string K =  nameof(OpenAI) + ".";
            public const string Module = K + nameof(Module);
            public const string OnConnection = K + nameof(OnConnection);
        }
    }
    public static partial class OAIService // Methods
    {
        public static IOpenAI Module() => Key.Module.Dispatch<IOpenAI>();
    }
}