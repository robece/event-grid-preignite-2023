namespace Benchmark.StorageQueueAndPull.Sender
{
    public class Settings
    {
        public string version { get; set; } = string.Empty;

        // webserver
        public string webServerProcessStartPath { get; set; } = string.Empty;
        public string webServerProcessStopPath { get; set; } = string.Empty;
        public string webServerProcessInfoPath { get; set; } = string.Empty;

        // namespace
        public string namespaceTopicEndpoint { get; set; } = string.Empty;
        public string namespaceTopicEndpointAccessKey { get; set; } = string.Empty;
        public string namespaceTopicName { get; set; } = string.Empty;
    }
}
