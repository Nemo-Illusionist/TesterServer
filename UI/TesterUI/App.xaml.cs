using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using TesterUI.MVVM.Models;

namespace TesterUI
{
    public partial class App : Application
    {
        public static Context Context { get; private set; }
        public static AppSettings Configuration { get; private set; } = new AppSettings();

        protected override void OnStartup(StartupEventArgs e)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            config.Bind(Configuration);

            InitAppContext();
        }

        private static void InitAppContext()
        {
            Context = new Context();
        }
    }

    public class AppSettings
    {
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string Auth { get; set; }
        public string Broker { get; set; }
    }
}