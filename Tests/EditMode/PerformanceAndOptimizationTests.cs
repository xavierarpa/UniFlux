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
using UniFlux.Core.Internal;

namespace UniFlux.Tests
{
    /// <summary>
    /// Performance and optimization tests for UniFlux
    /// </summary>
    public class PerformanceAndOptimizationTests
    {
        private int _callbackInvocationCount;
        private string _lastReceivedValue;

        [SetUp]
        public void Setup()
        {
            _callbackInvocationCount = 0;
            _lastReceivedValue = null;
        }

        [Test]
        public void State_DispatchSameValue_ShouldNotTriggerCallback()
        {
            // Arrange
            var state = new State<string>(OnStateChanged);
            const string testValue = "test";

            // Act - dispatch same value twice
            state.Dispatch(testValue);
            var initialCount = _callbackInvocationCount;
            state.Dispatch(testValue); // Should not trigger callback

            // Assert
            Assert.AreEqual(initialCount, _callbackInvocationCount, 
                "Dispatching the same value should not trigger callbacks");
        }

        [Test]
        public void State_DispatchDifferentValues_ShouldTriggerCallbacks()
        {
            // Arrange
            var state = new State<string>(OnStateChanged);

            // Act
            state.Dispatch("value1");
            state.Dispatch("value2");

            // Assert
            Assert.AreEqual(2, _callbackInvocationCount, 
                "Dispatching different values should trigger callbacks");
            Assert.AreEqual("value2", _lastReceivedValue);
        }

        [Test]
        public void State_EqualityComparison_WorksWithValueTypes()
        {
            // Arrange
            var intCallbackCount = 0;
            var state = new State<int>(value => intCallbackCount++);

            // Act
            state.Dispatch(42);
            state.Dispatch(42); // Same value
            state.Dispatch(43); // Different value

            // Assert
            Assert.AreEqual(2, intCallbackCount, 
                "Value type equality should work correctly");
        }

        [Test]
        public void State_EqualityComparison_WorksWithReferenceTypes()
        {
            // Arrange
            var objCallbackCount = 0;
            var state = new State<object>(obj => objCallbackCount++);
            var testObj = new object();

            // Act
            state.Dispatch(testObj);
            state.Dispatch(testObj); // Same reference
            state.Dispatch(new object()); // Different reference

            // Assert
            Assert.AreEqual(2, objCallbackCount, 
                "Reference type equality should work correctly");
        }

        [Test]
        public void State_MultipleCallbacks_AllInvoked()
        {
            // Arrange
            var callback1Count = 0;
            var callback2Count = 0;
            var state = new State<string>(value => callback1Count++);
            state.Store(true, value => callback2Count++);

            // Act
            state.Dispatch("test");

            // Assert
            Assert.AreEqual(1, callback1Count, "First callback should be invoked");
            Assert.AreEqual(1, callback2Count, "Second callback should be invoked");
        }

        [Test]
        public void State_CallbackRemoval_WorksCorrectly()
        {
            // Arrange
            var removedCallbackCount = 0;
            var remainingCallbackCount = 0;
            
            Action<string> removedCallback = value => removedCallbackCount++;
            Action<string> remainingCallback = value => remainingCallbackCount++;
            
            var state = new State<string>(removedCallback);
            state.Store(true, remainingCallback);

            // Act - remove first callback
            state.Store(false, removedCallback);
            state.Dispatch("test");

            // Assert
            Assert.AreEqual(0, removedCallbackCount, "Removed callback should not be invoked");
            Assert.AreEqual(1, remainingCallbackCount, "Remaining callback should be invoked");
        }

        [Test]
        public void FluxState_GetState_ReturnsCorrectValues()
        {
            // Arrange
            const string key = "testKey";
            const int value = 42;

            // Act - store and dispatch state
            Flux.StoreState<string, int>(key, OnIntStateChanged, true);
            Flux.DispatchState<string, int>(key, value);

            // Get state
            var hasValue = Flux.GetState<string, int>(key, out var retrievedValue);

            // Assert
            Assert.IsTrue(hasValue, "Should have state value");
            Assert.AreEqual(value, retrievedValue, "Retrieved value should match dispatched value");

            // Cleanup
            Flux.StoreState<string, int>(key, OnIntStateChanged, false);
        }

        [Test]
        public void FluxState_GetNonExistentState_ReturnsFalse()
        {
            // Act
            var hasValue = Flux.GetState<string, int>("nonExistentKey", out var value);

            // Assert
            Assert.IsFalse(hasValue, "Should not have value for non-existent key");
            Assert.AreEqual(default(int), value, "Value should be default");
        }

        private void OnStateChanged(string value)
        {
            _callbackInvocationCount++;
            _lastReceivedValue = value;
        }

        private void OnIntStateChanged(int value)
        {
            // Helper for integer state tests
        }
    }
}