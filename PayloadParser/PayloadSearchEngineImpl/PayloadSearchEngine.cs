using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationCenter.PayloadParser.PayloadSearchEngineImpl
{
    #nullable enable
    class PayloadSearchEngine : IPayloadSearchEngine
    {
        public JToken? SearchForValue(JObject parsedPayload, string searched)
        {
            return parsedPayload.SelectToken(searched);
        }

        public Dictionary<string, JToken?> SearchForValues(JObject parsedPayload, IEnumerable<string> searched)
        {
            Dictionary<string, JToken?> toReturn = new Dictionary<string, JToken?>();

            foreach(string search in searched)
            {
                toReturn.Add(search, SearchForValue(parsedPayload, search));
            }

            return toReturn;
        }
    }
}
