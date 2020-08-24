using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NotificationCenter.Data;
using NotificationCenter.PayloadParser;
using NotificationCenter.PayloadParser.PayloadParserImpl;
using NotificationCenter.PayloadParser.PayloadSearchEngineImpl;
using NotificationCenter.Provider.Processor;
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

        Input input;

        MqttClient client;

        List<Engine.Processing> Processors = new List<Engine.Processing>();

        public MqttInputProvider(Input i)
        {
            input = i;
        }

        event Engine.Processing IInputProvider.OnProcessingRequired
        {
            add
            {
                Processors.Add(value);
            }

            remove
            {
                Processors.Remove(value);
            }
        }

        public bool Connect()
        {
            try
            {
                client = new MqttClient(input.Provider.ProviderUrl);

                client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;

                string clientId = Guid.NewGuid().ToString();

                client.Connect(clientId);

                client.Subscribe(new string[] { input.Provider.InputParameters.First(i => i.ParameterKey == MQTTTopicTag).ParameterKey }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
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

            JObject body = payloadParser.ParseStringPayload(payload);

            foreach(Engine.Processing p in Processors)
            {
                p.Invoke(this, body);
            }

            return true;
        }
    }
}
