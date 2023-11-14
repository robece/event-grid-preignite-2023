namespace MQTTBroker
{
    public class Settings
    {        
        public string version { get; set; } = string.Empty;
        public string mqttEndpoint { get; set; } = string.Empty;
        public int mqttPort { get; set; } = 8883;
        public string clientId { get; set; } = string.Empty;
        public string mqttBrokertopic { get; set; } = string.Empty;
        public string rootCertificateFileName { get; set; } = string.Empty;
        public string clientCertificateFileName { get; set; } = string.Empty;
        public string clientCertificateKeyFileName { get; set; } = string.Empty;
    }
}
