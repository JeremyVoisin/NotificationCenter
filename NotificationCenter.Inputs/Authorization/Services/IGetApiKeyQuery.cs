using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NotificationCenter.Inputs.Authorization.Services
{
    public interface IGetApiKeyQuery
    {
        Task<APIKey> Execute(string providedApiKey);
    }
}
