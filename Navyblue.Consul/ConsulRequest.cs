namespace Navyblue.Consul;

public interface ConsulRequest
{
    IList<IUrlParameters?> AsUrlParameters();
}