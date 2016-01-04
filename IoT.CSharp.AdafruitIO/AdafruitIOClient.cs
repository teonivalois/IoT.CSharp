using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace IoT.CSharp.AdafruitIO
{
    public abstract class AdafruitIOClient : IDisposable
    {
        public FeedManager Feed { get; } = new FeedManager();

        private readonly MqttClient _client;
        private readonly string _username;
        private readonly string _key;

        public AdafruitIOClient(string username, string key)
        {
            _username = username;
            _key = key;

            Feed.FeedChanged += OnFeedChanged;

            _client = new MqttClient("io.adafruit.com", 1883, false, MqttSslProtocols.None);
            _client.MqttMsgPublishReceived += OnMessageReceived;

            _client.Connect(Guid.NewGuid().ToString(), _username, _key);
        }

        private void OnMessageReceived(object sender, MqttMsgPublishEventArgs e)
        {
            var topic = e.Topic.Split('/').LastOrDefault();
            if (string.IsNullOrEmpty(topic))
                return;

            if (!Feed.Actions.ContainsKey(topic.ToLower()))
                return;

            Feed.Actions[topic.ToLower()](Encoding.UTF8.GetString(e.Message));
        }

        private void OnFeedChanged(string feedName)
        {
            _client.Subscribe(new[] { string.Format("{0}/feeds/{1}", _username, feedName) }, new[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
        }

        public void Dispose()
        {
            if (_client.IsConnected)
            {
                _client.Unsubscribe(Feed.Actions.Keys.Select(feedName => string.Format("{0}/feeds/{1}", _username, feedName)).ToArray());
                _client.Disconnect();
            }
        }

        public class FeedManager
        {
            public Dictionary<string, Action<string>> Actions { get; } = new Dictionary<string, Action<string>>();

            public Action<dynamic> this[string feedName]
            {
                set
                {
                    if (Actions.ContainsKey(feedName.ToLower()))
                        Actions.Remove(feedName.ToLower());

                    Actions.Add(feedName.ToLower(), value);

                    if (FeedChanged != null)
                        FeedChanged(feedName);
                }
            }

            public event FeedChangedHandler FeedChanged;
            public delegate void FeedChangedHandler(string feedName);
        }
    }
}
