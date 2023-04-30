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
namespace Kingdox.UniFlux.Netcode.Rollback
{
    public static partial class RollbackService // Data
    {
        private static partial class Data
        {

        }
    }
    public static partial class RollbackService // Key
    {
        public static partial class Key
        {
            private const string _K =  nameof(RollbackService) + ".";
            public const string RollbackFlux = _K + nameof(RollbackFlux);
            public const string OnSave = _K + nameof(OnSave);
            public const string OnReverse = _K + nameof(OnReverse);
            public const string OnReverseError = _K + nameof(OnReverseError);
            public const string OnUndoError = _K + nameof(OnUndoError);
            public const string OnUndo = _K + nameof(OnUndo);
        }
    }
    public static partial class RollbackService // Methods
    {
        public static IRollbackFlux RollbackFlux => Key.RollbackFlux.Dispatch<IRollbackFlux>();
        public static void OnSave((int fps, object state) data) => Key.OnSave.Dispatch(data);
        public static void OnReverse((int fps, object state) data) => Key.OnReverse.Dispatch(data);
        public static void OnReverseError((int fps, int fpsToReverse) data) => Key.OnReverseError.Dispatch(data);
        public static void OnUndoError() => Key.OnUndoError.Dispatch();
        public static void OnUndo((int fps, object state) data) => Key.OnUndo.Dispatch(data);
    }
}