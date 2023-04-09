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
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Kingdox.UniFlux;
using Kingdox.UniFlux.Core;

namespace Kingdox.UniFlux.Tests
{
    public class EditMode_Test_1
    {
        [Test] public void EditMode_Test_2_UniFlux_Subscribe_Pass()
        {
            //Subscribe
            Flux.Store("OnTest", OnTest_Action, true); // Action
            Flux.Store<string,bool>("OnTest", OnTest_ActionParam, true); // ActionParam
            Flux.Store<string, int>("OnTest", OnTest_Func, true); // Func
            Flux.Store<string,float,float>("OnTest", OnTest_FuncPatam, true); // FuncParam
        }
        [Test] public void EditMode_Test_3_UniFlux_Action_Pass()
        {
            //Action
            Flux.Dispatch("OnTest"); // Action
            Flux.Dispatch("OnTest", true); // ActionParamal
            var val = Flux.Dispatch<string,int>("OnTest"); // Func
            var val_2 = Flux.Dispatch<string, float, float>("OnTest", 3f); // FuncParam
        }
        [Test] public void EditMode_Test_4_UniFlux_Unsubscribe_Pass()
        {
            //Unsubscribe
            Flux.Store("OnTest", OnTest_Action, false); // Action
            Flux.Store<string, bool>("OnTest", OnTest_ActionParam, false); // ActionParam
            Flux.Store<string, int>("OnTest", OnTest_Func, false); // Func
            Flux.Store<string, float, float>("OnTest", OnTest_FuncPatam, false); // FuncParam
        }
        public void OnTest_Action() 
        {
            Debug.Log("OnTest");
        }
        public void OnTest_ActionParam(bool condition) 
        {
            Debug.Log($"ActionParam: {condition}");
        }
        public int OnTest_Func() 
        {
            return 42;
        }
        public float OnTest_FuncPatam(float value) 
        {
            return value;
        }
    }
}
