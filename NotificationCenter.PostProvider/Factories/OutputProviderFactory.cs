using NotificationCenter.Data;
using NotificationCenter.PostProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationCenter.Provider.Factories
{
    public interface OutputProviderFactory
    {
        IPostProvider Build(Output provider);
    }
}
