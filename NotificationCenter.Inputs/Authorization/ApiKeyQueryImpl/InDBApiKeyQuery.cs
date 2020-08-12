using NotificationCenter.Inputs.Authorization.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationCenter.Inputs.Authorization.ApiKeyQueryImpl
{
    class InDBApiKeyQuery : IGetApiKeyQuery
    {
        private readonly IDictionary<string, APIKey> _apiKeys;

        public InDBApiKeyQuery()
        {
            var existingApiKeys = new List<APIKey>
        {
            new APIKey(1, "Main", "C5BFF7F0-B4DF-475E-A331-F737424F013C", new DateTime(2019, 01, 01),
                new List<string>
                {
                    "MainAPIKey",
                })
        };

            _apiKeys = existingApiKeys.ToDictionary(x => x.Key, x => x);
        }

        public Task<APIKey> Execute(string providedApiKey)
        {
            _apiKeys.TryGetValue(providedApiKey, out var key);
            return Task.FromResult(key);
        }
    }
    {
    }
}
