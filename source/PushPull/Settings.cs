namespace PushPull
{
    public class Settings
    {        
        public string version { get; set; } = string.Empty;
        public string processorStorageConnectionString { get; set; } = string.Empty;
        public string processorStorageContainer { get; set; } = string.Empty;
        public string processorConnectionString { get; set; } = string.Empty;
        public string processorHub { get; set; } = string.Empty;
        public string namespaceTopicEndpoint { get; set; } = string.Empty;
        public string namespaceTopicEndpointAccessKey { get; set; } = string.Empty;
        public string namespaceTopicName { get; set; } = string.Empty;
        public string namespaceTopicSubscriptionName { get; set; } = string.Empty;
        public int namespaceTopicSubscriptionMaxEvents { get; set; } = 10;
    }
}
