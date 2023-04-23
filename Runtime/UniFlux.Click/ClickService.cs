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
namespace Kingdox.UniFlux.Click
{
    // public static partial class Click // Data
    // {
    //     private static partial class Data
    //     {
    //     }
    // }
    public static partial class Click // Key
    {
        public static partial class Key
        {
            private const string KEY =  nameof(Click) + ".";
            public const string OnClickEnterNew = KEY + nameof(OnClickEnterNew);
            public const string OnClickEnterOld = KEY + nameof(OnClickEnterOld);
            public const string OnClickEnter = KEY + nameof(OnClickEnter);
            public const string OnClickExitNew = KEY + nameof(OnClickExitNew);
            public const string OnClickExitOld = KEY + nameof(OnClickExitOld);
            public const string OnClickExit = KEY + nameof(OnClickExit);
        }
    }
    public static partial class Click // Methods
    {
        public static void OnClickEnterNew() => Key.OnClickEnterNew.Dispatch();
        public static void OnClickEnterOld() => Key.OnClickEnterOld.Dispatch();
        public static void OnClickEnter() => Key.OnClickEnter.Dispatch();
        public static void OnClickExitNew() => Key.OnClickExitNew.Dispatch();
        public static void OnClickExitOld() => Key.OnClickExitOld.Dispatch();
        public static void OnClickExit() => Key.OnClickExit.Dispatch();
    }
}