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
using System.Collections.Generic;
namespace Kingdox.UniFlux.Netcode.Rollback
{
    public sealed class RollbackObject : IRollback
    {
        private List<object> states = new List<object>();
        private int fps_count = 0;
        public Action<(int fps, object state)> OnSave;
        public Action<(int fps, object state)> OnReverse;
        public Action<(int fps, int fpsToReverse)> OnReverseError;
        public Action OnUndoError;
        public Action<(int fps, object state)> OnUndo;

        void IRollback.Save(object _state)
        {
            states.Add(_state);
            fps_count++;
            OnSave?.Invoke((fps_count, _state));
        }
        void IRollback.Reverse(int fpsToReverse)
        {
            if (fpsToReverse > fps_count)
            {
                OnReverseError?.Invoke((fps_count, fpsToReverse));
                throw new ArgumentException("The number of frames to rewind is greater than the total number of frames.");
            }

            fps_count -= fpsToReverse;
            OnReverse?.Invoke((fps_count, states[fps_count]));
        }
        void IRollback.Undo()
        {
            if (states.Count == 0)
            {
                OnUndoError?.Invoke();
                throw new InvalidOperationException("There are no states to undo.");
            }
            object stateToUndo = states[states.Count - 1];
            states.RemoveAt(states.Count - 1);
            fps_count--;
            OnUndo?.Invoke((fps_count, stateToUndo));
        }
    }
}
