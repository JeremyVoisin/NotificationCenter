using Newtonsoft.Json.Linq;
using PayloadParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotificationCenter.PayloadParser.PayloadParserImpl
{
    public class JsonPayloadParser : IPayloadParser
    {
        public JObject ParseByteArrayPayload(byte[] payload, Encoding encoding)
        {
            return JObject.Parse(encoding.GetString(payload));
        }

        public JObject ParseStringPayload(string payload)
        {
            return JObject.Parse(payload);
        }
    }
}
