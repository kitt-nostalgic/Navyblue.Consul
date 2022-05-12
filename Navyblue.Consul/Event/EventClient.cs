using Navyblue.Consul.Event.Model;

namespace Navyblue.Consul.Event;

using Event = Model.Event;
using EventParams = EventParams;

/// <summary>
///
/// </summary>
public interface IEventClient
{
    ConsulResponse<Event> EventFire(string @event, string payload, EventParams? eventParams, QueryParams queryParams);

    ConsulResponse<IList<Event>> EventList(EventListRequest eventListRequest);
}