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
using System.Collections.Generic;
using System.Linq;
using OWOGame;

namespace Kingdox.UniFlux.OWO
{
    public static partial class OWOService // Data
    {
        public static partial class Data
        {
            internal static readonly Dictionary<int, Muscle> MUSCLES = Muscle.All.Select(value => new { value.id, value }).ToDictionary(x => x.id, x => x.value);
        }
    }
    public static partial class OWOService // Key
    {
        public static partial class Key
        {
            private const string _K =  nameof(OWO) + ".";
            public const string Module = _K + nameof(Module);
            public const string OnConnectionState = _K + nameof(OnConnectionState);
            public const string AutoConnect = _K + nameof(AutoConnect);
            public const string Connect = _K + nameof(Connect);
            public const string Disconnect = _K + nameof(Disconnect);
            public const string Send = _K + nameof(Send);
            public const string Stop = _K + nameof(Stop);
        }
    }
    public static partial class OWOService // Methods
    {
        public static IOWO Module => Key.Module.Dispatch<IOWO>();
    }
}