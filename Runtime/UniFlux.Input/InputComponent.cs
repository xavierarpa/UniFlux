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
    [Serializable] public class InputComponent
    {
        [SerializeField] private List<InputModel> Models = new List<InputModel>();
        private Action<string> OnInputId;
        //
        public void Subscribe(bool condition, Action<string> callback)
        {
            if(condition) OnInputId += callback;
            else OnInputId -= callback;
        }
        public void Update() 
        {
            for (int i = 0; i < Models.Count; i++) 
            {
                Models[i].CheckInput();
            }
        }
        public void Add((string id, KeyCode code, string method) data) 
        {
            Models.Add(new InputModel(data, OnInput));
        }
        public void Remove(string id)
        {
            Models.RemoveAll(k => string.Equals(id, k.Id));
        }
        public void Clear()
        {
            Models.Clear();
        }
        //
        private void OnInput(string id)
        {
            OnInputId?.Invoke(id);
        }
    }
}
