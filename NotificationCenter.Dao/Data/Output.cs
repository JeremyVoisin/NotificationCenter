using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationCenter.Data
{
    public class Output
    {
        public int OutputId { get; set; }

        public string Schema { get; set; }

        public OutputProvider Provider { get; set; }
    }
}
