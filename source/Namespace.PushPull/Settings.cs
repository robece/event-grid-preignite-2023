namespace Namespace.PushPull
{
    public class Settings
    {
        public string version { get; set; } = string.Empty;

        // relay
        public string relayNamespace { get; set; } = string.Empty;
        public string relayConnectionName { get; set; } = string.Empty;
        public string relayKeyName { get; set; } = string.Empty;
        public string relayKey { get; set; } = string.Empty;
        public string relayWebhookPath { get; set; } = string.Empty;

        // namespace
        public string namespaceTopicEndpoint { get; set; } = string.Empty;
        public string namespaceTopicEndpointAccessKey { get; set; } = string.Empty;
        public string namespaceTopicName { get; set; } = string.Empty;
        public string namespaceTopicSubscriptionName { get; set; } = string.Empty;
        public int namespaceTopicSubscriptionMaxEvents { get; set; } = 10;

        //storage
        public string storageAccountUri { get; set; } = string.Empty;
        public string storageAccountName { get; set; } = string.Empty;
        public string storageAccountKey { get; set; } = string.Empty;
        public string storageAccountContainerName { get; set; } = string.Empty;
    }
}
