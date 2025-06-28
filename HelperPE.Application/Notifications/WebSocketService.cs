using System.Text.Json;
using Fleck;

namespace HelperPE.Application.Notifications
{
    public class WebSocketService
    {
        private readonly WebSocketServer _server;
        private readonly List<IWebSocketConnection> _connections = new();
        private readonly object _lockObject = new();
        private bool _isStarted = false;

        public WebSocketService(string url = "ws://0.0.0.0:8181")
        {
            _server = new WebSocketServer(url);
        }

        public void Start()
        {
            if (_isStarted) return;

            _server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    lock (_lockObject)
                    {
                        _connections.Add(socket);
                    }
                };

                socket.OnClose = () =>
                {
                    lock (_lockObject)
                    {
                        _connections.Remove(socket);
                    }
                };
            });

            _isStarted = true;
        }

        public void BroadcastMessage<T>(T message)
        {
            if (!_isStarted)
                return;

            var jsonMessage = JsonSerializer.Serialize(message);

            lock (_lockObject)
            {
                var connectionsToRemove = new List<IWebSocketConnection>();

                foreach (var connection in _connections)
                {
                    try
                    {
                        if (connection.IsAvailable)
                        {
                            connection.Send(jsonMessage);
                        }
                        else
                        {
                            connectionsToRemove.Add(connection);
                        }
                    }
                    catch
                    {
                        connectionsToRemove.Add(connection);
                    }
                }

                foreach (var connection in connectionsToRemove)
                {
                    _connections.Remove(connection);
                }
            }
        }

        public void Dispose()
        {
            _server?.Dispose();
        }
    }
}
