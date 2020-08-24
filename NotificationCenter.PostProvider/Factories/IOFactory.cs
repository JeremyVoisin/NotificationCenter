using Microsoft.EntityFrameworkCore;
using NotificationCenter.Data;
using NotificationCenter.PostProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationCenter.Provider.Factories
{
    public class IOFactory
    {
        public List<IInputProvider> BuildInputProviders()
        {
            List<IInputProvider> toReturn = new List<IInputProvider>();
            using(DataContext data = new DataContext())
            {
                foreach(Input i in data.Inputs.ToList())
                {
                    toReturn.Add(BuildInputProvider(i));
                }
            }
            return toReturn;
        }

        public IInputProvider BuildInputProvider(Input input)
        {
            InputProviderFactory factory;
            switch (input.Provider.Type)
            {
                case InputType.Mqtt:
                default:
                    factory = new MQTTInputProviderFactory();
                    break;
            }

            return factory.Build(input);
        }

        public List<IPostProvider> BuildOutputProviders(Input i)
        {
            List<IPostProvider> toReturn = new List<IPostProvider>();
            using (DataContext data = new DataContext())
            {
                foreach (Mapping m in data.Mappings.Include(m => m.Input)
                                                    .Include(m => m.Output)
                                                    .ThenInclude(o => o.Provider)
                                                    .Where(m => m.Input.InputId == i.InputId))
                {
                    toReturn.Add(BuildOutputProvider(m.Output));
                }
            }
            return toReturn;
        }

        public IPostProvider BuildOutputProvider(Output output)
        {
            OutputProviderFactory factory;
            switch (output.Provider.Type)
            {
                case OutputType.Rest:
                default:
                    factory = new HttpPostProviderFactory();
                    break;
            }

            return factory.Build(output);
        }
    }
}
