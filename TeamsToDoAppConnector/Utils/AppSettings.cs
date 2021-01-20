using System.Configuration;

namespace TeamsToDoAppConnector.Utils
{
    /// <summary>
    /// Represents a class to stores settings for easy access.
    /// </summary>
    public static class AppSettings
    {
        public static string BaseUrl { get; set; }
        public static string ClientAppId { get; set; }

        static AppSettings()
        {
            BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            ClientAppId = ConfigurationManager.AppSettings["ClientAppId"];
        }
    }
}