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
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Profiling;
using Kingdox.UniFlux;
using Kingdox.UniFlux.Core;
namespace Kingdox.UniFlux
{
    [Serializable]
    public class Marker
    {
        [SerializeField] public bool Execute=true;
        [HideInInspector] public int iteration = 1;
		[HideInInspector] public readonly Stopwatch sw = new Stopwatch();
        [HideInInspector] public string K = "?";
        public string Visual => $"{K} --- {iteration} iteration --- {sw.ElapsedMilliseconds} ms";
    }
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
            _Results.Clear();
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
                Clock(_m_store_string_add);
                for (int i = 0; i < _iterations; i++) 
                {
                    "Store".Store(Example_OnFlux, true);
                }
                Clock(_m_store_string_add);
            }
            // Store Int
            if(_m_store_int_add.Execute)
            {
                _m_store_int_add.iteration=_iterations;
                Clock(_m_store_int_add);
                for (int i = 0; i < _iterations; i++) 
                {
                    42.Store(Example_OnFlux, true);
                }
                Clock(_m_store_int_add);
            }
            // Store Byte
            if(_m_store_byte_add.Execute)
            {
                _m_store_byte_add.iteration=_iterations;
                Clock(_m_store_byte_add);
                for (int i = 0; i < _iterations; i++) 
                {
                    Flux.Store(__m_store, Example_OnFlux, true);
                }
                Clock(_m_store_byte_add);
            }
            // Store Bool
            if(_m_store_bool_add.Execute)
            {
                _m_store_bool_add.iteration=_iterations;
                Clock(_m_store_bool_add);
                for (int i = 0; i < _iterations; i++) 
                {
                    Flux.Store(true, Example_OnFlux, true);
                }
                Clock(_m_store_bool_add);
            }
        }
        private void StoreTest_Remove()
        {
            // Store String
            if(_m_store_string_remove.Execute)
            {
                _m_store_string_remove.iteration=_iterations;
                Clock(_m_store_string_remove);
                for (int i = 0; i < _iterations; i++) 
                {
                    "Store".Store(Example_OnFlux, false);
                }
                Clock(_m_store_string_remove);
            }
            // Store Int
            if(_m_store_int_remove.Execute)
            {
                _m_store_int_remove.iteration=_iterations;
                Clock(_m_store_int_remove);
                for (int i = 0; i < _iterations; i++) 
                {
                    42.Store(Example_OnFlux, false);
                }
                Clock(_m_store_int_remove);
            }
            // Store Byte
            if(_m_store_byte_remove.Execute)
            {
                _m_store_byte_remove.iteration=_iterations;
                Clock(_m_store_byte_remove);
                for (int i = 0; i < _iterations; i++) 
                {
                    Flux.Store(__m_store, Example_OnFlux, false);
                }
                Clock(_m_store_byte_remove);
            }
            // Store Bool
            if(_m_store_bool_remove.Execute)
            {
                _m_store_bool_remove.iteration=_iterations;
                Clock(_m_store_bool_remove);
                for (int i = 0; i < _iterations; i++) 
                {
                    Flux.Store(true, Example_OnFlux, false);
                }
                Clock(_m_store_bool_remove);
            }
        }
        private void DispatchTest()
        {
            // Dispatch String
            if(_m_dispatch_string.Execute)
            {
                _m_dispatch_string.iteration=_iterations;
                Clock(_m_dispatch_string);
                for (int i = 0; i < _iterations; i++) "UniFlux.Dispatch".Dispatch();
                Clock(_m_dispatch_string);
            }
            // Dispatch Int
            if(_m_dispatch_int.Execute)
            {
                _m_dispatch_int.iteration=_iterations;
                Clock(_m_dispatch_int);
                for (int i = 0; i < _iterations; i++) 0.Dispatch();
                Clock(_m_dispatch_int);
            }
            // Dispatch Byte
            if(_m_dispatch_byte.Execute)
            {
                _m_dispatch_byte.iteration=_iterations;
                Clock(_m_dispatch_byte);
                for (int i = 0; i < _iterations; i++) Flux.Dispatch(__m_dispatch);
                Clock(_m_dispatch_byte);
            }
            // Dispatch Boolean
            if(_m_dispatch_bool.Execute)
            {
                _m_dispatch_bool.iteration=_iterations;
                Clock(_m_dispatch_bool);
                for (int i = 0; i < _iterations; i++) Flux.Dispatch(true);
                Clock(_m_dispatch_bool);
            }
        }
        [Flux("UniFlux.Dispatch")] private void Example_Dispatch_String()
        {
            // String Key Example
        }
        [Flux(0)] private void Example_Dispatch_Int()
        {
            // Int Key Example
        }
        [Flux(__m_dispatch)] private void Example_Dispatch_Byte()
        {
            // Byte Key Example
        }
        [Flux(true)] private void Example_Dispatch_Boolean()
        {
            // Boolean Key Example
        }
        private void Example_OnFlux()
        {
            // Example to Stores
        }
        private void Clock(Marker marker)
        {
            if(!marker.sw.IsRunning)
            {
                marker.sw.Restart();
                Profiler.BeginSample(marker.K);
            }
            else
            {
                Profiler.EndSample();
                marker.sw.Stop();
                _Results.Add(marker.Visual);
            }
        }
        private void OnGUI()
		{
            if(!draw)return;
            var height = (float) Screen.height / 2;
            for (int i = 0; i < _Results.Count; i++)
            {
                rect_area = new Rect(0, _style.Value.lineHeight * i, Screen.width, height);
			    GUI.Label(rect_area, _Results[i], _style.Value);
            }
		}
    }
}