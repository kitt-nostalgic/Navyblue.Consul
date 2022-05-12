using Navyblue.Consul.Query.Model;

namespace Navyblue.Consul.Query;

public interface IQueryClient
{
    ConsulResponse<QueryExecution> ExecutePreparedQuery(string uuid, QueryParams? queryParams);
}