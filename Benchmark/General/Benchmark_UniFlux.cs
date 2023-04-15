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
using Kingdox.UniFlux.Core;
namespace Kingdox.UniFlux.Benchmark
{
    public class Benchmark_UniFlux : MonoFlux
    {
        [SerializeField] private Marker _m_store_string_add = new Marker()
        {
            K = "store<string,Action> ADD"
        };
        [SerializeField] private Marker _m_store_int_add = new Marker()
        {
            K = "store<int,Action> ADD"
        };
        [SerializeField] private Marker _m_store_byte_add = new Marker()
        {
            K = "store<byte,Action> ADD"
        };
        [SerializeField] private Marker _m_store_bool_add = new Marker()
        {
            K = "store<bool,Action> ADD"
        };
        [SerializeField] private Marker _m_store_string_remove = new Marker()
        {
            K = "store<string,Action> REMOVE"
        };
        [SerializeField] private Marker _m_store_int_remove = new Marker()
        {
            K = "store<int,Action> REMOVE"
        };
        [SerializeField] private Marker _m_store_byte_remove = new Marker()
        {
            K = "store<byte,Action> REMOVE"
        };
        [SerializeField] private Marker _m_store_bool_remove = new Marker()
        {
            K = "store<bool,Action> REMOVE"
        };
        [SerializeField] private Marker _m_dispatch_string = new Marker()
        {
            K = $"dispatch<string>"
        };
        [SerializeField] private Marker _m_dispatch_int = new Marker()
        {
            K = $"dispatch<int>"
        };
        [SerializeField] private Marker _m_dispatch_byte = new Marker()
        {
            K = $"dispatch<byte>"
        };
        [SerializeField] private Marker _m_dispatch_bool = new Marker()
        {
            K = $"dispatch<bool>"
        };
        private const byte __m_store = 52;
        private const byte __m_dispatch = 250;
		private Rect rect_area;
        private readonly Lazy<GUIStyle> _style = new Lazy<GUIStyle>(() => new GUIStyle("label")
		{
			fontSize = 28,
			alignment = TextAnchor.MiddleLeft,
            padding = new RectOffset(10, 0, 0, 0)
		});
        [SerializeField] private int _iterations = default;
        [SerializeField] private List<string> _Results = default;
        public bool draw=true;
        public bool isUpdated = false;
        public bool isUpdated_store = false;
        public bool isUpdated_dispatch = false;
        protected override void OnFlux(in bool condition)
        {
            StoreTest_Add();
            StoreTest_Remove();
        }
        public void Start()
        {
            DispatchTest();
        }
        private void Update()
        {
            if(!isUpdated) return;
            if(isUpdated_store) StoreTest_Add();
            if(isUpdated_store) StoreTest_Remove();
            if(isUpdated_dispatch) DispatchTest();
        }
        private void StoreTest_Add()
        {
            // Store String
            if(_m_store_string_add.Execute)
            {
                _m_store_string_add.iteration=_iterations;
                _m_store_string_add.Begin();
                for (int i = 0; i < _iterations; i++) 
                {
                    "Store".Store(Example_OnFlux, true);
                }
                _m_store_string_add.End();
            }
            // Store Int
            if(_m_store_int_add.Execute)
            {
                _m_store_int_add.iteration=_iterations;
                _m_store_int_add.Begin();
                for (int i = 0; i < _iterations; i++) 
                {
                    42.Store(Example_OnFlux, true);
                }
                _m_store_int_add.End();
            }
            // Store Byte
            if(_m_store_byte_add.Execute)
            {
                _m_store_byte_add.iteration=_iterations;
                _m_store_byte_add.Begin();
                for (int i = 0; i < _iterations; i++) 
                {
                    Flux.Store(__m_store, Example_OnFlux, true);
                }
                _m_store_byte_add.End();
            }
            // Store Bool
            if(_m_store_bool_add.Execute)
            {
                _m_store_bool_add.iteration=_iterations;
                _m_store_bool_add.Begin();
                for (int i = 0; i < _iterations; i++) 
                {
                    Flux.Store(true, Example_OnFlux, true);
                }
                _m_store_bool_add.End();
            }
        }
        private void StoreTest_Remove()
        {
            // Store String
            if(_m_store_string_remove.Execute)
            {
                _m_store_string_remove.iteration=_iterations;
                _m_store_string_remove.Begin();
                for (int i = 0; i < _iterations; i++) 
                {
                    "Store".Store(Example_OnFlux, false);
                }
                _m_store_string_remove.End();
            }
            // Store Int
            if(_m_store_int_remove.Execute)
            {
                _m_store_int_remove.iteration=_iterations;
                _m_store_int_remove.Begin();
                for (int i = 0; i < _iterations; i++) 
                {
                    42.Store(Example_OnFlux, false);
                }
                _m_store_int_remove.End();
            }
            // Store Byte
            if(_m_store_byte_remove.Execute)
            {
                _m_store_byte_remove.iteration=_iterations;
                _m_store_byte_remove.Begin();
                for (int i = 0; i < _iterations; i++) 
                {
                    Flux.Store(__m_store, Example_OnFlux, false);
                }
                _m_store_byte_remove.End();
            }
            // Store Bool
            if(_m_store_bool_remove.Execute)
            {
                _m_store_bool_remove.iteration=_iterations;
                _m_store_bool_remove.Begin();
                for (int i = 0; i < _iterations; i++) 
                {
                    Flux.Store(true, Example_OnFlux, false);
                }
                _m_store_bool_remove.End();
            }
        }
        private void DispatchTest()
        {
            // Dispatch String
            if(_m_dispatch_string.Execute)
            {
                _m_dispatch_string.iteration=_iterations;
                _m_dispatch_string.Begin();
                for (int i = 0; i < _iterations; i++) "UniFlux.Dispatch".Dispatch();
                _m_dispatch_string.End();
            }
            // Dispatch Int
            if(_m_dispatch_int.Execute)
            {
                _m_dispatch_int.iteration=_iterations;
                _m_dispatch_int.Begin();
                for (int i = 0; i < _iterations; i++) 0.Dispatch();
                _m_dispatch_int.End();
            }
            // Dispatch Byte
            if(_m_dispatch_byte.Execute)
            {
                _m_dispatch_byte.iteration=_iterations;
                _m_dispatch_byte.Begin();
                for (int i = 0; i < _iterations; i++) Flux.Dispatch(__m_dispatch);
                _m_dispatch_byte.End();
            }
            // Dispatch Boolean
            if(_m_dispatch_bool.Execute)
            {
                _m_dispatch_bool.iteration=_iterations;
                _m_dispatch_bool.Begin();
                for (int i = 0; i < _iterations; i++) Flux.Dispatch(true);
                _m_dispatch_bool.End();
            }
        }
        [Flux("UniFlux.Dispatch")] private void Example_Dispatch_String(){}
        [Flux("UniFlux.Dispatch")] private void Example_Dispatch_String2(){}
        [Flux(0)] private void Example_Dispatch_Int(){}
        [Flux(__m_dispatch)] private void Example_Dispatch_Byte(){}
        [Flux(false)] private void Example_Dispatch_Boolean_2(){}
        [Flux(false)] private void Example_Dispatch_Boolean_3(){}
        [Flux(false)] private void Example_Dispatch_Boolean_4(){}
        [Flux(false)] private void Example_Dispatch_Boolean_5(){}
        [Flux(false)] private void Example_Dispatch_Boolean_6(){}
        [Flux(true)] private void Example_Dispatch_Boolean(){}
        private void Example_OnFlux(){}
        private void OnGUI()
		{

            if(!draw)return;

            _Results.Clear();
            _Results.Add(_m_store_string_add.Visual);
            _Results.Add(_m_store_int_add.Visual);
            _Results.Add(_m_store_byte_add.Visual);
            _Results.Add(_m_store_bool_add.Visual);
            _Results.Add(_m_store_string_remove.Visual);
            _Results.Add(_m_store_int_remove.Visual);
            _Results.Add(_m_store_byte_remove.Visual);
            _Results.Add(_m_store_bool_remove.Visual);
            _Results.Add(_m_dispatch_string.Visual);
            _Results.Add(_m_dispatch_int.Visual);
            _Results.Add(_m_dispatch_byte.Visual);
            _Results.Add(_m_dispatch_bool.Visual);
            var height = (float) Screen.height / 2;
            for (int i = 0; i < _Results.Count; i++)
            {
                rect_area = new Rect(0, _style.Value.lineHeight * i, Screen.width, height);
			    GUI.Label(rect_area, _Results[i], _style.Value);
            }
		}
    }
}