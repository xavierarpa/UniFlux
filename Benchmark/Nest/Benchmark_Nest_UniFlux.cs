using System;
using UnityEngine;
namespace Kingdox.UniFlux.Benchmark
{
    public sealed class Benchmark_Nest_UniFlux : MonoFlux
    {
        [SerializeField] private Marker _mark_fluxAttribute = new Marker()
        {
            K = "NestedModel Flux Attribute"
        };
        [SerializeField] private Marker _mark_store = new Marker()
        {
            K = "NestedModel Store"
        };
        private readonly Lazy<GUIStyle> _style = new Lazy<GUIStyle>(() => new GUIStyle("label")
		{
			fontSize = 28,
			alignment = TextAnchor.MiddleLeft,
            padding = new RectOffset(10, 0, 0, 0)
		});
		private Rect rect_area;
        public int iteration;
        protected override void OnFlux(in bool condition)
        {
            "1".Store(Store_1, condition);
            "2".Store(Store_2, condition);
            "3".Store(Store_3, condition);
            "4".Store(Store_4, condition);
            "5".Store(Store_5, condition);
        }
        private void Update() 
        {
            Sample();
            Sample_2();
        }
        [Flux("A")] private void A() => "B".Dispatch();
        [Flux("B")] private void B() => "C".Dispatch();
        [Flux("C")] private void C() => "D".Dispatch();
        [Flux("D")] private void D() => "E".Dispatch();
        [Flux("E")] private void E() {}
        private void Store_1() => "2".Dispatch();
        private void Store_2() => "3".Dispatch();
        private void Store_3() => "4".Dispatch();
        private void Store_4() => "5".Dispatch();
        private void Store_5() {}
        private void Sample()
        {
            if (_mark_fluxAttribute.Execute)
            {
                _mark_fluxAttribute.iteration = iteration;
                _mark_fluxAttribute.Begin();
                for (int i = 0; i < iteration; i++) "A".Dispatch();
                _mark_fluxAttribute.End();
            }
        }
        private void Sample_2()
        {
            if (_mark_store.Execute)
            {
                _mark_store.iteration = iteration;
                _mark_store.Begin();
                for (int i = 0; i < iteration; i++) "1".Dispatch();
                _mark_store.End();
            }
        }
        private void OnGUI()
        {
            if (_mark_fluxAttribute.Execute)
            {
                // Flux
                rect_area = new Rect(0, _style.Value.lineHeight, Screen.width, Screen.height / 2);
                GUI.Label(rect_area, _mark_fluxAttribute.Visual, _style.Value);
            }
            if (_mark_store.Execute)
            {
                // Store
                rect_area = new Rect(0, _style.Value.lineHeight * 2, Screen.width, Screen.height / 2);
                GUI.Label(rect_area, _mark_store.Visual, _style.Value);
            }
        }
    }
}