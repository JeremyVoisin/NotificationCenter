using NotificationCenter.Data;
using NotificationCenter.PostProvider;
using NotificationCenter.PostProvider.PostProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationCenter.Provider.Factories
{
    class HttpPostProviderFactory : OutputProviderFactory
    {
        public IPostProvider Build(Output provider)
        {
            IPostProvider outputProvider = new HTTPPostProvider(provider);
            return outputProvider;
        }
    }
}
