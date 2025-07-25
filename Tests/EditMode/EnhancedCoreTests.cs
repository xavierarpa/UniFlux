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
using NUnit.Framework;
using UniFlux;
using UniFlux.Core;

namespace UniFlux.Tests
{
    /// <summary>
    /// Enhanced core functionality tests for UniFlux
    /// </summary>
    public class EnhancedCoreTests
    {
        private bool _actionInvoked;
        private string _receivedStringParam;
        private int _receivedIntReturn;
        private float _receivedFloatParam;
        private float _functionReturnValue;

        [SetUp]
        public void Setup()
        {
            _actionInvoked = false;
            _receivedStringParam = null;
            _receivedIntReturn = 0;
            _receivedFloatParam = 0f;
            _functionReturnValue = 0f;
        }

        [Test]
        public void Flux_ActionWorkflow_SubscribeDispatchUnsubscribe()
        {
            // Arrange
            const string key = "testAction";

            // Act & Assert - Subscribe
            Assert.DoesNotThrow(() => Flux.Store(key, TestAction, true));

            // Act & Assert - Dispatch
            Assert.DoesNotThrow(() => Flux.Dispatch(key));
            Assert.IsTrue(_actionInvoked, "Action should have been invoked");

            // Act & Assert - Unsubscribe
            _actionInvoked = false;
            Assert.DoesNotThrow(() => Flux.Store(key, TestAction, false));
            Flux.Dispatch(key);
            Assert.IsFalse(_actionInvoked, "Action should not be invoked after unsubscribe");
        }

        [Test]
        public void Flux_ActionParamWorkflow_WithStringParameter()
        {
            // Arrange
            const string key = "testActionParam";
            const string testParam = "hello";

            // Act & Assert - Subscribe and Dispatch
            Flux.Store<string, string>(key, TestActionWithStringParam, true);
            Flux.Dispatch<string, string>(key, testParam);

            Assert.AreEqual(testParam, _receivedStringParam, 
                "Parameter should be passed correctly");

            // Cleanup
            Flux.Store<string, string>(key, TestActionWithStringParam, false);
        }

        [Test]
        public void Flux_FunctionReturnWorkflow_ReturnsValue()
        {
            // Arrange
            const string key = "testFunction";
            const int expectedReturn = 42;

            // Act & Assert
            Flux.Store<string, int>(key, TestFunctionReturningInt, true);
            var result = Flux.Dispatch<string, int>(key);

            Assert.AreEqual(expectedReturn, result, 
                "Function should return expected value");

            // Cleanup
            Flux.Store<string, int>(key, TestFunctionReturningInt, false);
        }

        [Test]
        public void Flux_FunctionParamReturnWorkflow_WithParameters()
        {
            // Arrange
            const string key = "testFunctionParam";
            const float inputValue = 3.14f;
            const float multiplier = 2f;

            // Act & Assert
            Flux.Store<string, float, float>(key, TestFunctionWithFloatParam, true);
            var result = Flux.Dispatch<string, float, float>(key, inputValue);

            Assert.AreEqual(inputValue * multiplier, result, 0.001f, 
                "Function should process parameter and return correct value");
            Assert.AreEqual(inputValue, _receivedFloatParam, 
                "Parameter should be received correctly");

            // Cleanup
            Flux.Store<string, float, float>(key, TestFunctionWithFloatParam, false);
        }

        [Test]
        public void Flux_StateWorkflow_StoreDispatchGet()
        {
            // Arrange
            const string key = "testState";
            const bool initialValue = true;
            const bool updatedValue = false;

            // Act & Assert - Store callback and dispatch initial value
            Flux.StoreState<string, bool>(key, TestStateCallback, true);
            Flux.DispatchState<string, bool>(key, initialValue);

            // Verify state can be retrieved
            var hasState1 = Flux.GetState<string, bool>(key, out var state1);
            Assert.IsTrue(hasState1, "State should exist");
            Assert.AreEqual(initialValue, state1, "State should match initial value");

            // Update state
            Flux.DispatchState<string, bool>(key, updatedValue);
            var hasState2 = Flux.GetState<string, bool>(key, out var state2);
            Assert.IsTrue(hasState2, "State should still exist");
            Assert.AreEqual(updatedValue, state2, "State should match updated value");

            // Cleanup
            Flux.StoreState<string, bool>(key, TestStateCallback, false);
        }

        [Test]
        public void Flux_MultipleSubscribers_AllReceiveDispatches()
        {
            // Arrange
            const string key = "multiSubscriber";
            var callback1Invoked = false;
            var callback2Invoked = false;

            Action callback1 = () => callback1Invoked = true;
            Action callback2 = () => callback2Invoked = true;

            // Act
            Flux.Store(key, callback1, true);
            Flux.Store(key, callback2, true);
            Flux.Dispatch(key);

            // Assert
            Assert.IsTrue(callback1Invoked, "First callback should be invoked");
            Assert.IsTrue(callback2Invoked, "Second callback should be invoked");

            // Cleanup
            Flux.Store(key, callback1, false);
            Flux.Store(key, callback2, false);
        }

        [Test]
        public void Flux_DifferentKeyTypes_WorkIndependently()
        {
            // Arrange
            const string stringKey = "stringKey";
            const int intKey = 42;
            var stringKeyInvoked = false;
            var intKeyInvoked = false;

            // Act
            Flux.Store(stringKey, () => stringKeyInvoked = true, true);
            Flux.Store(intKey, () => intKeyInvoked = true, true);

            // Dispatch only string key
            Flux.Dispatch(stringKey);

            // Assert
            Assert.IsTrue(stringKeyInvoked, "String key callback should be invoked");
            Assert.IsFalse(intKeyInvoked, "Int key callback should not be invoked");

            // Cleanup
            Flux.Store(stringKey, () => stringKeyInvoked = true, false);
            Flux.Store(intKey, () => intKeyInvoked = true, false);
        }

        // Test helper methods
        private void TestAction()
        {
            _actionInvoked = true;
        }

        private void TestActionWithStringParam(string param)
        {
            _receivedStringParam = param;
        }

        private int TestFunctionReturningInt()
        {
            return 42;
        }

        private float TestFunctionWithFloatParam(float param)
        {
            _receivedFloatParam = param;
            return param * 2f;
        }

        private void TestStateCallback(bool value)
        {
            // State callback for testing
        }
    }
}