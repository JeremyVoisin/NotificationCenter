using NotificationCenter.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationCenter.PostProvider
{
    public interface IPostProvider
    {
        Output Output { get; }
        bool Post(string jsonBody, Dictionary<string, string> headers);
        bool Post(string jsonBody);
    }
}
