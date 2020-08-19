using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationCenter.Data
{
    public class History
    {
        public int HistoryId { get; set; }

        public string Content { get; set; }

        public Mapping Mapping { get; set; }

        public DateTime DateTime { get; set; }
    }
}
