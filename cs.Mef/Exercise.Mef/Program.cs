using Exercise.Mef.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise.Mef
{
    [Export]
    class Program : IPartImportsSatisfiedNotification
    {
        private const string _pluginDir = "plugins";

        private static CompositionContainer _container;

        //[Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        //PluginHost host;

        [Import]
        ExportFactory<PluginHost> HostFactory;

        static void Main(string[] args)
        {
            // create plugin directory
            Directory.CreateDirectory(_pluginDir);

            _container = CreateContainer();

            var factory = new Program();
            _container.ComposeParts(factory);

            var host1 = factory.HostFactory.CreateExport().Value;
            var host2 = factory.HostFactory.CreateExport().Value;
            var host3 = factory.HostFactory.CreateExport().Value;

            host1.Run("host1");
            host1.Dispose();
            Console.WriteLine(Environment.NewLine);

            host2.Run("host2");
            Console.WriteLine(Environment.NewLine);

            host3.Run("host3");
            Console.WriteLine(Environment.NewLine);

            //workers are non - shared instances
            Debug.Assert(!host1._workers[0].Equals(host2._workers[0]));
            Console.WriteLine("Worker plugins are non shared instances!");

            Console.ReadLine();
        }

        private static CompositionContainer CreateContainer()
        {
            /// load plugins from <see cref="_pluginDir"/> folder
            DirectoryCatalog dirCatalog = new DirectoryCatalog(_pluginDir);

            var conventions = new RegistrationBuilder();

            /// this exports the <see cref="PluginHost"/> type 
            /// -> you dont have to use the <see cref="ExportAttribute"/> over the <see cref="PluginHost"/> class
            conventions.ForType<PluginHost>().Export();


            // load plugins from the current assembly
            AssemblyCatalog assemblyCat = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly(), conventions);

            // combine these two catalogs
            AggregateCatalog catalog = new AggregateCatalog(assemblyCat, dirCatalog);

            return new CompositionContainer(catalog);
        }

        public void OnImportsSatisfied()
        {
            Console.WriteLine("Mef is done & you are ready to go" + Environment.NewLine);
        }
    }
}
