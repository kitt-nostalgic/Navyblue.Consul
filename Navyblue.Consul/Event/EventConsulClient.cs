using Navyblue.BaseLibrary;
using Navyblue.Consul.Event.Model;
using Navyblue.Consul.Transport;

namespace Navyblue.Consul.Event;

/// <summary>
///
/// </summary>
public sealed class EventConsulClient : IEventClient
{
    private readonly IConsulRawClient _consulRawClient;

    public EventConsulClient(IConsulRawClient consulRawClient)
    {
        this._consulRawClient = consulRawClient;
    }


    public ConsulResponse<Model.Event> EventFire(string @event, string payload, EventParams? eventParams, QueryParams queryParams)
    {
        ConsulHttpResponse httpResponse = _consulRawClient.MakePutRequest("/v1/event/fire/" + @event, payload, eventParams, queryParams);

        if (httpResponse.StatusCode == 200)
        {
            Model.Event value = httpResponse.Content.FromJson<Model.Event>();
            return new ConsulResponse<Model.Event>(value, httpResponse);
        }

        throw new OperationException(httpResponse);
    }

    public ConsulResponse<IList<Model.Event>> EventList(QueryParams? queryParams)
    {
        return EventList(null, queryParams);
    }

    public ConsulResponse<IList<Model.Event>> EventList(string @event, QueryParams? queryParams)
    {
        var request = new EventListRequest(@event,null,null,null, queryParams,null);

        return EventList(request);
    }

    public ConsulResponse<IList<Model.Event>> EventList(EventListRequest eventListRequest)
    {
        ConsulHttpResponse httpResponse = _consulRawClient.MakeGetRequest("/v1/event/list", eventListRequest.AsUrlParameters());

        if (httpResponse.StatusCode == 200)
        {
            IList<Model.Event> value = httpResponse.Content.FromJson<List<Model.Event>>();
            return new ConsulResponse<IList<Model.Event>>(value, httpResponse);
        }

        throw new OperationException(httpResponse);
    }
}