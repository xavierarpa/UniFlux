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
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Kingdox.UniFlux;
using OpenAI;
namespace Kingdox.UniFlux.OpenAI
{
    public sealed class OAIModule : MonoFlux, IOpenAI 
    {
        OpenAIApi api;
        OpenAIApi API 
        {
            get => api;
            set 
            {
                if(api == value)
                {
                    return;
                }
                api = value;
                OAIService.Key.OnConnection.Dispatch(value != null);
            }
        }
        [Flux(OAIService.Key.Module)] private IOpenAI Module() => this;
        // private async void SendRequest()
        // {
        //     var response = await API.CreateCompletion(new()
        //     {
        //         Model= OAIService.Data.Model.gpt_3_5_turbo,
        //         Prompt="Say this is a test",
        //     });
        // }
        // [ContextMenu("Test BORRAR LUEGO")] private async void test()
        // {
        //     UnityEngine.Debug.Log("1 Access");
        //     UnityEngine.Debug.Log("2 Prompt");
        //     UnityEngine.Debug.Log("3 Submit");
        //     // 1 Access
        //     API = new("API_KEY", null);
        //     // 2 Prompt
        //     var response = new CreateChatCompletionRequest()
        //     {
        //         Model= OAIService.Data.Model.gpt_3_5_turbo,
        //         Messages = new List<ChatMessage>
        //         {
        //             new ChatMessage()
        //             {
        //                 Role = "user",
        //                 Content = "Write a 100 word long short story in La Fontaine style."
        //             }
        //         },
        //         Temperature = 0.7f,
        //     };
        //     // 3 Submit
        //     var chat = await API.CreateChatCompletion(response);
        //     Debug.Log(chat);
        //     Debug.Log(chat.Id);
        //     Debug.Log(chat.Object);
        //     Debug.Log(chat.Choices?.Count);
        //     Debug.Log(chat.Created);
        //     Debug.Log(chat.Error);
        //     Debug.Log(chat.Usage);
        //     Debug.Log(chat.Model);
        //     Debug.Log(chat.ToString());
        //     // API.CreateChatCompletionAsync(response, 
        //     //     (responses) => {
        //     //         var result = string.Join("", responses.Select(response => response.Choices[0].Delta.Content));
        //     //         Debug.Log(responses.Count);
        //     //         Debug.Log(result);
        //     //     }, 
        //     //     () => Debug.Log("completed"),
        //     //     new CancellationTokenSource()
        //     // );
        // }
        // private void _1((string apiKey, string organization) data)
        // {
        //     // var req = new CreateChatCompletionRequest{
        //     //     Model = "gpt-3.5-turbo",
        //     //     Messages = new List<ChatMessage>
        //     //     {
        //     //         new ChatMessage()
        //     //         {
        //     //             Role = "user",
        //     //             Content = "Write a 100 word long short story in La Fontaine style."
        //     //         }
        //     //     },
        //     //     Temperature = 0.7f,
        //     // };
        //     // openai.CreateChatCompletionAsync(req, 
        //     //     (responses) => {
        //     //         var result = string.Join("", responses.Select(response => response.Choices[0].Delta.Content));
        //     //         Debug.Log(result);
        //     //     }, 
        //     //     () => {
        //     //         Debug.Log("completed");
        //     //     }, 
        //     //     new CancellationTokenSource()
        //     // );
        // }
    }
}