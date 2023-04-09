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
using UnityEngine;
using Kingdox.UniFlux;
namespace Kingdox.UniFlux.Sample
{
    public sealed class Sample_3 : MonoFlux
    {
        [SerializeField] private int _life;
        public int Life
        {
            [Flux("Get_Life")] get => _life;
            [Flux("Set_Life")] set 
            {
                _life = value;
                "OnChange_Life".Dispatch(value);
            }
        }
        private void Start() 
        {
            "Set_Life".Dispatch(10);
        }
        private void Update()
        {
            (Time.frameCount % 60).Dispatch();
        }
        [Flux(0)] private void OnUpdate() 
        {
            if("Get_Life".Dispatch<int>() > 0)
            {
                "Set_Life".Dispatch("Get_Life".Dispatch<int>()-1);
            }
        }
        [Flux("OnChange_Life")] private void OnChange_Life(int life) 
        {
            if(life == 0)
            {
                "OnDeath".Dispatch();
            }   
        }
        [Flux("OnDeath")] private void OnDeath()
        {
            Debug.Log("You're Dead !");
        }
    }
}