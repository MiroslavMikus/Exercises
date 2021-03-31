using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace HalloConfig
{
    public class TreeView
    {
        readonly IConfigurationRoot _configRoot;

        public TreeView(IConfigurationRoot config)
        {
            _configRoot = config;
        }

        public void WriteToConsole()
        {
            void RecurseChildren(IHasTreeNodes node, IEnumerable<IConfigurationSection> children)
            {
                foreach (IConfigurationSection child in children)
                {
                    (string Value, IConfigurationProvider Provider) valueAndProvider =
                        GetValueAndProvider(_configRoot, child.Path);

                    IHasTreeNodes parent = node;
                    if (valueAndProvider.Provider != null)
                    {
                        node.AddNode(new Table()
                            .Border(TableBorder.None)
                            .HideHeaders()
                            .AddColumn("Key")
                            .AddColumn("Value")
                            .AddColumn("Provider")
                            .HideHeaders()
                            .AddRow($"[yellow]{child.Key}[/]", valueAndProvider.Value,
                                $@"([grey]{valueAndProvider.Provider}[/])")
                        );
                    }
                    else
                    {
                        parent = node.AddNode($"[yellow]{child.Key}[/]");
                    }

                    RecurseChildren(parent, child.GetChildren());
                }
            }

            var tree = new Tree(string.Empty);

            RecurseChildren(tree, _configRoot.GetChildren());

            AnsiConsole.Render(tree);
        }

        private static (string Value, IConfigurationProvider Provider) GetValueAndProvider(
            IConfigurationRoot root,
            string key)
        {
            foreach (IConfigurationProvider provider in root.Providers.Reverse())
            {
                if (provider.TryGet(key, out string value))
                {
                    return (value, provider);
                }
            }

            return (null, null);
        }
    }
}