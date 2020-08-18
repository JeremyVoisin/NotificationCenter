using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationCenter.PostProvider
{
    interface IPostProvider
    {
        void SetUp(Dictionary<string, object> parameters);

        bool Post(Dictionary<string, object> body);
        bool Post(IEnumerable<object> body);
    }
}
