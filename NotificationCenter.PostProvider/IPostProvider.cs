using NotificationCenter.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationCenter.PostProvider
{
    interface IPostProvider
    {
        bool Post(OutputProvider provider, string jsonBody, Dictionary<string, string> headers);
        bool Post(OutputProvider provider, string jsonBody);
    }
}
