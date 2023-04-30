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
using System.Linq;
using System.Threading.Tasks;
using OWOGame;
namespace Kingdox.UniFlux.OWO
{
    public sealed class OWOModule : MonoFlux, IOWO
    {
        private ConnectionState _state = ConnectionState.Disconnected;
        ConnectionState State
        {
            get => _state;
            set
            {
                if(_state != value)
                {
                    OWOService.Key.OnConnectionState.Dispatch((int)value);
                    OWOService.Key.OnConnectionState.Dispatch(value.ToString());
                    OWOService.Key.OnConnectionState.Dispatch(value.Equals(ConnectionState.Connected));
                }
                _state = value;
            }
        }
        private void Awake() => State = OWOGame.OWO.ConnectionState; // ~Loads State
        private void Update() => State = OWOGame.OWO.ConnectionState; // ~Updates State
        [Flux(OWOService.Key.AutoConnect)] Task IOWO.AutoConnect()  => OWOGame.OWO.AutoConnect();
        [Flux(OWOService.Key.Connect)] Task IOWO.Connect(string ip) => OWOGame.OWO.Connect(ip);
        [Flux(OWOService.Key.Disconnect)] Task IOWO.Disconnect()  => new Task(OWOGame.OWO.Disconnect);
        [Flux(OWOService.Key.Send)] Task IOWO.Send(int frequency,  float durationSeconds,  int intensityPercentage,  float rampUpMillis,  float rampDownMillis,  float exitDelaySeconds, params (int id, int intensity)[] muscles) => new Task(()=>OWOGame.OWO.Send(SensationsFactory.Create(frequency, durationSeconds, intensityPercentage, rampUpMillis, rampDownMillis, exitDelaySeconds),muscles?.Select(m => OWOService.Data.MUSCLES[m.id].WithIntensity(m.intensity)).ToArray() ?? new Muscle[0]));
        [Flux(OWOService.Key.Stop)] Task IOWO.Stop()  => new Task(OWOGame.OWO.Stop);
        [Flux(OWOService.Key.Module)] private IOWO Module() => this;
    }
}