using Serilog;

namespace Hello.Assembly_Context.CustomerWithDependency
{
    public class WithDependencyPlugin : IPlugin
    {
        public string Name {
            get
            {
                Log.Logger.Information("Logging with serilog");
                return nameof(WithDependencyPlugin);
            }
        }

        public WithDependencyPlugin()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}