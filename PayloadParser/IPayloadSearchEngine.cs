using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationCenter.PayloadParser
{
    public interface IPayloadSearchEngine
    {
        #nullable enable
        /// <summary>
        /// Search a JObject inside another JObject with a dot separated levels of search (ie for example user.name)
        /// </summary>
        /// <param name="parsedPayload">A JObject, created from an IPayloadParser</param>
        /// <param name="searched">An access path to the searched datas</param>
        /// <returns>The JToken found</returns>
        JToken? SearchForValue(JObject parsedPayload, string searched);

        /// <summary>
        /// Search a list of JObject inside another JObject with some dot separated levels of search (ie for example user.name)
        /// </summary>
        /// <param name="parsedPayload">A JObject, created from an IPayloadParser</param>
        /// <param name="searched">List of access pathes to the searched datas</param>
        /// <returns>A dictionary containing the access path as key, and the JObject as value</returns>
        Dictionary<string, JToken?> SearchForValues(JObject parsedPayload, IEnumerable<string> searched);
    }
}
