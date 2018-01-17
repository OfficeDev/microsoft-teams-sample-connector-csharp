namespace TeamsToDoAppConnector.Models
{
    /// <summary>
    /// Represents the model to store channel subscriptions.
    /// </summary>
    public class Subscription
    {
        public string GroupName { get; set; }
        public string WebHookUri { get; set; }
    }
}