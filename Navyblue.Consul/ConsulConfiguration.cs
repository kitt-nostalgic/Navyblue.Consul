namespace Navyblue.Consul;

public class ConsulConfiguration
{
    public string Host { get; set; } = "localhost";

    public int Port { get; set; } = 8500;

    public string PrefixPath { get; set; } = "";

    public bool Enabled { get; set; }
}