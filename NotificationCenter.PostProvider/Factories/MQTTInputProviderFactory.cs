using NotificationCenter.Data;
using NotificationCenter.PostProvider;
using NotificationCenter.PostProvider.InputProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationCenter.Provider.Factories
{
    class MQTTInputProviderFactory : InputProviderFactory
    {
        public IInputProvider Build(Input provider)
        {
            IInputProvider inputProvider = new MqttInputProvider(provider);
            inputProvider.Connect();
            return inputProvider;
        }
    }
}
