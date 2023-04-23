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
namespace Kingdox.UniFlux.Click
{
    public sealed class ClickFlux : MonoFlux
    {
        [SerializeField] private ClickObject click = new ClickObject
        {
            OnClickEnterNew = Click.OnClickEnterNew,
            OnClickExitNew = Click.OnClickExitNew,
            OnClickEnterOld = Click.OnClickEnterOld,
            OnClickExitOld = Click.OnClickExitOld,
            OnClickEnter = Click.OnClickEnter,
            OnClickExit = Click.OnClickExit
        };
        private void Update() 
        {
    #if UNITY_EDITOR
            click.IsClicking = UnityEngine.Input.GetMouseButton(0);
    #elif UNITY_ANDROID
            click.IsClicking = UnityEngine.Input.touchCount.Equals(1);
    #endif
        }
    }
}