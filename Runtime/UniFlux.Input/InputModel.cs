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
using UnityEngine;

namespace Kingdox.UniFlux.Input
{
    [Serializable] public struct InputModel
    {
        [SerializeField] private string _id;
        [SerializeField] private KeyCode _code;
        private Func<KeyCode, bool> _function;
        private Action<string> _onInput;
        //
        public string Id => _id;
        //
        public InputModel((string id, KeyCode code, string method) data, Action<string> onInput)
        {
            this._id = data.id;
            this._code = data.code;
            this._function = InputData.FUNCTIONS[data.method];
            this._onInput = onInput;
        }
        //
        public void CheckInput() 
        {
            if(_function(_code))
            {
                _onInput?.Invoke(_id);
            }
        }
    }
}