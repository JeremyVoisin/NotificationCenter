using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayloadParser
{
    public interface IPayloadParser
    {
        JObject ParseStringPayload(string payload);

        JObject ParseByteArrayPayload(byte[] payload, Encoding encoding);
    }
}
