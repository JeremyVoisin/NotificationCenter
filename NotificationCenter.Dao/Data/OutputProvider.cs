using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationCenter.Data
{
    public class OutputProvider
    {
        public int OutputProviderId { get; set; }

        public string ProviderUrl { get; set; }

        public List<OutputParameter> OutputParameters { get; set; }

        public OutputType Type { get; set; }
    }
}
