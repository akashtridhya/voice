using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace voice.api
{
    public class Program
    {
#if DEBUG
        public static string environmentName = "Debug";
#elif STAG
        public static string environmentName = "Stag";
#elif QA
        public static string environmentName = "Qa";
#elif RELEASE
        public static string environmentName = "Release";
#elif PRODUCTION
        public static string environmentName = "Production";
#endif

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
             WebHost.CreateDefaultBuilder(args)
                .UseEnvironment(environmentName)
                .UseStartup<Startup>()
                .CaptureStartupErrors(true)
                .UseSetting("detailedErrors", "true")
                .Build();
    }
}