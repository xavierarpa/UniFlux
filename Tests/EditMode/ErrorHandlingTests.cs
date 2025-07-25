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
    /// Error handling and validation tests for UniFlux
    /// </summary>
    public class ErrorHandlingTests
    {
        [Test]
        public void ActionFlux_StoreNullAction_ThrowsArgumentNullException()
        {
            // Arrange
            var actionFlux = new ActionFlux<string>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                actionFlux.Store(true, "key", null));
        }

        [Test]
        public void StateFlux_StoreNullAction_ThrowsArgumentNullException()
        {
            // Arrange
            var stateFlux = new StateFlux<string, int>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                stateFlux.Store(true, "key", null));
        }

        [Test]
        public void Flux_StoreNullCallback_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                Flux.Store<string>("key", null, true));
        }

        [Test]
        public void Flux_StoreStateNullCallback_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                Flux.StoreState<string, int>("key", null, true));
        }

        [Test]
        public void Flux_DispatchNonExistentKey_DoesNotThrow()
        {
            // Act & Assert - should not throw exceptions
            Assert.DoesNotThrow(() => Flux.Dispatch("nonExistentKey"));
            Assert.DoesNotThrow(() => Flux.Dispatch<string, int>("nonExistentKey", 42));
        }

        [Test]
        public void Flux_GetStateNonExistentKey_ReturnsFalse()
        {
            // Act
            var hasState = Flux.GetState<string, int>("nonExistentKey", out var value);

            // Assert
            Assert.IsFalse(hasState);
            Assert.AreEqual(default(int), value);
        }

        [Test]
        public void ActionFlux_DispatchWithNullActionsInCollection_HandlesGracefully()
        {
            // This test ensures the null-conditional operator in ActionFlux.Dispatch works
            // Arrange
            var actionFlux = new ActionFlux<string>();
            var callbackInvoked = false;
            Action validCallback = () => callbackInvoked = true;

            // Add a valid callback
            actionFlux.Store(true, "key", validCallback);

            // Act - dispatch should handle any potential nulls gracefully
            Assert.DoesNotThrow(() => actionFlux.Dispatch("key"));

            // Assert
            Assert.IsTrue(callbackInvoked, "Valid callback should have been invoked");
        }
    }
}