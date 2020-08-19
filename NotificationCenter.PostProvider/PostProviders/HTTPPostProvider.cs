using NotificationCenter.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationCenter.PostProvider.PostProviders
{
    class HTTPPostProvider : IPostProvider
    {
        public bool Post(OutputProvider provider, string jsonBody)
        {
            throw new NotImplementedException();
        }

        public bool Post(OutputProvider provider, string jsonBody, Dictionary<string, string> headers)
        {
            throw new NotImplementedException();
        }
    }
}
