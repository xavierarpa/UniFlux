/*
Copyright (c) 2023 Xavier Arpa López Thomas Peter ('xavierarpa')

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
using UnityEngine;
using System.Collections.Generic;
namespace UniFlux.Sample
{
    public sealed class Sample_5 : MonoFlux
    {
        public const string K_Primary = "primary";
        [SerializeField] private Color color_1;
        [SerializeField] private Color color_2;
        [Space]
        [SerializeField] private Color color_current;
        [Space]
        [SerializeField] private List<Color> history_colors;
        private void Awake() 
        {
            history_colors.Clear();
        }
        protected override void OnFlux(in bool condition) => K_Primary.StoreState<Color>(OnPrimaryChange, condition); // 1 - Subscribe OnPrimaryChange and invokes automatically
        private void Start() => K_Primary.DispatchState(color_2); // 2 - Change to secondary color state
        private void OnPrimaryChange(Color color) 
        {
            color_current = color;
            history_colors.Add(color);
        }
        [Flux(nameof(Sample_5) + ".ChangePrimary_Color1")] private void _ChangePrimary_Color1() => K_Primary.DispatchState(color_1);
        [Flux(nameof(Sample_5) + ".ChangePrimary_Color2")] private void _ChangePrimary_Color2() => K_Primary.DispatchState(color_2);
    }
}