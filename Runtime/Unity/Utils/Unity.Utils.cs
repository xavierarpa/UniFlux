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
using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UniFlux
{
    public static class UnityUtils
    {
        public static void Subscribe<T>(this bool condition, ref UnityAction<T> del, UnityAction<T> action)
        {
            if (condition) del += action;
            else del -= action;
        }
        public static void Subscribe<T,T2>(this bool condition, ref UnityAction<T,T2> del, UnityAction<T,T2> action)
        {
            if (condition) del += action;
            else del -= action;
        }

        public static void Subscribe<T>(this bool condition, ref Action<T> del, Action<T> action)
        {
            if (condition) del += action;
            else del -= action;
        }
        public static void Subscribe<T,T2>(this bool condition, ref Action<T,T2> del, Action<T,T2> action)
        {
            if (condition) del += action;
            else del -= action;
        }

        public static void Subscribe(this bool condition, ref Action del, ref Action action)
        {
            if (condition) del += action;
            else del -= action;
        }

        public static void Subscribe(this bool condition, ref Action del, Action action)
        {
            if (condition) del += action;
            else del -= action;
        }
        public static void SubscribeOnce(this bool condition, ref Action del, Action action)
        {
            if (condition) del = action;
            else del = null;
        }

        public static void Subscribe(this bool condition, Button btn, UnityAction action)
        {
            if (condition) btn.onClick.AddListener(action);
            else btn.onClick.RemoveListener(action);
        }

        public static void Subscribe(this bool condtion, Slider slider, UnityAction<float> action)
        {
            if (condtion) slider.onValueChanged.AddListener(action);
            else slider.onValueChanged.RemoveListener(action);
        }
    }
}