using Stateless;
using Stateless.Graph;
using System;
using System.Threading;

namespace Exercies.Stateless
{
    public class Connection
    {
        private enum Trigger { ConnectionLost, ConnectionEstablished }

        private enum State { Connected, Disconnected, Reconnecting }

        private readonly StateMachine<State, Trigger> _connectionState;

        /// <summary>
        /// http://www.webgraphviz.com/
        /// </summary>
        public string Graph { get => UmlDotGraph.Format(_connectionState.GetInfo()); }
        public string CurrentState { get => _connectionState.State.ToString(); }

        public bool AutoReconnect { get; set; }

        public Connection()
        {
            _connectionState = new StateMachine<State, Trigger>(State.Disconnected);

            _connectionState.Configure(State.Connected)
                .OnEntry(a => OnConnected())
                .PermitIf(Trigger.ConnectionLost, State.Disconnected, () => !AutoReconnect, "Auto reconnect is disabled")
                .PermitIf(Trigger.ConnectionLost, State.Reconnecting, () => AutoReconnect, "Auto reconnect is enabled");

            _connectionState.Configure(State.Disconnected)
                .Permit(Trigger.ConnectionEstablished, State.Connected);

            _connectionState.Configure(State.Reconnecting)
                .OnEntry(() => Reconnect())
                .Permit(Trigger.ConnectionEstablished, State.Connected);
        }

        public void SimulateDisconnect()
        {
            _connectionState.Fire(Trigger.ConnectionLost);
        }

        public void OnConnected()
        {
            Console.WriteLine("Connected");
        }

        public void Reconnect()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(500);
                Console.WriteLine("Reconnecting ...");

            }
            Console.WriteLine("Reconnect complete!");

            _connectionState.Fire(Trigger.ConnectionEstablished);
        }
    }
}
