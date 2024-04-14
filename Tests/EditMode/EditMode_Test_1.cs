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
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UniFlux;
using UniFlux.Core;

namespace UniFlux.Tests
{
    public class EditMode_Test_1
    {
        [Test] public void _0_EntireWorkFlow()
        {
            //Subscribe
            SubscribeAction();
            SubscribeActionParam();
            SubscribeFunc();
            SubscribeFuncParam();
            //Dispatch
            DispatchAction();
            DispatchActionParam();
            DispatchFunc();
            DispatchFuncParam();
            //Unsubscribe
            UnsubscribeAction();
            UnsubscribeActionParam();
            UnsubscribeFunc();
            UnsubscribeFuncParam();
        }
        [Test] public void SubscribeAction()
        {
            Flux.Store("OnTest", OnTest_Action, true);
        }
        [Test] public void SubscribeActionParam()
        {
            Flux.Store<string,bool>("OnTest", OnTest_ActionParam, true);
        }
        [Test] public void SubscribeFunc()
        {
            Flux.Store<string, int>("OnTest", OnTest_Func, true);
        }
        [Test] public void SubscribeFuncParam()
        {
            Flux.Store<string,float,float>("OnTest", OnTest_FuncPatam, true);
        }
        [Test] public void DispatchAction()
        {
            Flux.Dispatch("OnTest");
        }
        [Test] public void DispatchActionParam()
        {
            Flux.Dispatch("OnTest", true);
        }
        [Test] public void DispatchFunc()
        {
            var val = Flux.Dispatch<string,int>("OnTest");
        }
        [Test] public void DispatchFuncParam()
        {
            var val_2 = Flux.Dispatch<string, float, float>("OnTest", 3f);
        }
        [Test] public void UnsubscribeAction()
        {
            Flux.Store("OnTest", OnTest_Action, false);
        }
        [Test] public void UnsubscribeActionParam()
        {
            Flux.Store<string, bool>("OnTest", OnTest_ActionParam, false);
        }
        [Test] public void UnsubscribeFunc()
        {
            Flux.Store<string, int>("OnTest", OnTest_Func, false);
        }
        [Test] public void UnsubscribeFuncParam()
        {
            Flux.Store<string, float, float>("OnTest", OnTest_FuncPatam, false);
        }
        public void OnTest_Action() {}
        public void OnTest_ActionParam(bool condition) {}
        public int OnTest_Func() => 1;
        public float OnTest_FuncPatam(float value) => value;
    }
}
//Assert.AreNotEqual(0, val);