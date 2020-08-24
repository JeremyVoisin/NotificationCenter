using NotificationCenter.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationCenter.PostProvider.PostProviders
{
    class HTTPPostProvider : IPostProvider
    {

        public Output Output { get; }
        
        public HTTPPostProvider(Output o)
        {
            Output = o;
        }

        public bool Post(string jsonBody)
        {
            Console.WriteLine(jsonBody);
            return true;
        }

        public bool Post(string jsonBody, Dictionary<string, string> headers)
        {
            throw new NotImplementedException();
        }
    }
}
