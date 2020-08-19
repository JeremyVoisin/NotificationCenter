using Newtonsoft.Json.Linq;
using NotificationCenter.Data;
using NotificationCenter.PayloadParser;
using NotificationCenter.PayloadParser.PayloadParserImpl;
using NotificationCenter.PayloadParser.PayloadSearchEngineImpl;
using PayloadParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Exceptions;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace NotificationCenter.PostProvider.InputProviders
{
    class MqttInputProvider : IInputProvider
    {
        private static readonly string MQTTTopicTag = "MQTTTopic";

        Mapping providerMapping;

        MqttClient client;

        public MqttInputProvider(Mapping mapping)
        {
            providerMapping = mapping;
        }

        public bool Connect()
        {
            try
            {
                client = new MqttClient(providerMapping.Input.Provider.ProviderUrl);

                client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;

                string clientId = Guid.NewGuid().ToString();

                client.Connect(clientId);

                client.Subscribe(new string[] { providerMapping.Input.Provider.InputParameters.First(i => i.ParameterKey == MQTTTopicTag).ParameterKey }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            }
            catch(MqttConnectionException exception)
            {
                Console.WriteLine(exception);
                return false;
            }
            return true;
        }

        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string payload = Encoding.UTF8.GetString(e.Message);
            Process(payload);
        }

        public bool Process(string payload)
        {
            IPayloadParser payloadParser = new JsonPayloadParser();
            IPayloadSearchEngine searchEngine = new PayloadSearchEngine();

            JObject body = payloadParser.ParseStringPayload(payload);

            

            return true;
        }
    }
}
