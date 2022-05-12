using Navyblue.BaseLibrary;
using Navyblue.Consul.Query.Model;
using Navyblue.Consul.Transport;

namespace Navyblue.Consul.Query;

public sealed class QueryConsulClient : IQueryClient
{
    private readonly IConsulRawClient _consulRawClient;

    public QueryConsulClient(IConsulRawClient consulRawClient)
    {
        this._consulRawClient = consulRawClient;
    }

    public ConsulResponse<QueryExecution> ExecutePreparedQuery(string uuid, QueryParams? queryParams)
    {
        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/query/" + uuid + "/execute", queryParams);

        if (httpResponse.StatusCode == 200)
        {
            QueryExecution queryExecution = httpResponse.Content.FromJson<QueryExecution>(); ;
            return new ConsulResponse<QueryExecution>(queryExecution, httpResponse);
        }

        throw new OperationException(httpResponse);
    }
}