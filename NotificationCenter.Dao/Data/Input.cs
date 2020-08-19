using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationCenter.Data
{
    public class Input
    {
        public int InputId { get; set; }

        public string Schema { get; set; }

        public InputProvider Provider { get; set; }
    }
}
