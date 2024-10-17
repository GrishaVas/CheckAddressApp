using Microsoft.Extensions.Configuration;

namespace CheckAddressApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            var configuration = new ConfigurationBuilder().AddJsonFile("configuration.json").Build();
            ApplicationConfiguration.Initialize();
            Application.Run(new CheckAddressForm(configuration));
        }
    }
}