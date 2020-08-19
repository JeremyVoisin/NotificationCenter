using NotificationCenter.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationCenter.PostProvider
{
    public interface IInputProvider
    {
        bool Connect();

        bool Process(string payload);
    }
}
