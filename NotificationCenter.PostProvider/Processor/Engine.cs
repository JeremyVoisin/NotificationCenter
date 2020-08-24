using Newtonsoft.Json.Linq;
using NotificationCenter.Data;
using NotificationCenter.PayloadParser;
using NotificationCenter.PayloadParser.PayloadSearchEngineImpl;
using NotificationCenter.PostProvider;
using NotificationCenter.Provider.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NotificationCenter.Provider.Processor
{
    public class Engine
    {
        public delegate void Processing(IInputProvider i, JObject body);


        Dictionary<IInputProvider, List<IPostProvider>> Providers = new Dictionary<IInputProvider, List<IPostProvider>>();
        Dictionary<IInputProvider, Input> Inputs = new Dictionary<IInputProvider, Input>();

        public Engine(List<Input> inputs)
        {
            IOFactory ioFactory = new IOFactory();
            foreach(Input i in inputs)
            {
                IInputProvider provider = ioFactory.BuildInputProvider(i);
                Inputs.Add(provider, i);
                Providers.Add(provider, ioFactory.BuildOutputProviders(i));

                provider.OnProcessingRequired += Provider_OnProcessingRequired;
            }
        }

        private void Provider_OnProcessingRequired(IInputProvider i, JObject body)
        {
            Input input = Inputs[i];
            List<IPostProvider> postProviders = Providers[i];

            IPayloadSearchEngine searchEngine = new PayloadSearchEngine();

            foreach(IPostProvider pProvider in postProviders)
            {
                Output output = pProvider.Output;
                string bodyOutput = output.Schema;

                IEnumerable<Group> toFind = Regex.Match(bodyOutput, @"\$\{([^}]*)\}").Groups.Where(t => !t.Value.Contains("$")).Distinct();

                foreach(Group c in toFind)
                {
                    bodyOutput = bodyOutput.Replace("${" + c.Value + "}", searchEngine.SearchForValue(body, c.Value)?.Value<string>());
                }

                pProvider.Post(bodyOutput);
            }
        }
    }
}
