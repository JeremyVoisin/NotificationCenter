using NotificationCenter.Data;
using NotificationCenter.Provider.Processor;
using System;
using System.Collections.Generic;
using System.Text;
using static NotificationCenter.Provider.Processor.Engine;

namespace NotificationCenter.PostProvider
{
    public interface IInputProvider
    {
        event Processing OnProcessingRequired;

        bool Connect();

        bool Process(string payload);
    }
}
