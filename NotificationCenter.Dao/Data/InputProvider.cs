using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationCenter.Data
{
    public class InputProvider
    {
        public int InputProviderId { get; set; }

        public string ProviderUrl { get; set; }

        public List<InputParameter> InputParameters { get; set; }

        public InputType Type { get; set; }
    }
}
