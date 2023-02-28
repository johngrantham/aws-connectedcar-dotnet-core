using ConnectedCar.Core.Shared.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectedCar.Core.Shared.Services
{
    public interface IEventService
    {
        Task CreateEvent(Event evnt);

        Task DeleteEvent(string vin, long timestamp);

        Task<Event> GetEvent(String vin, long timestamp);

        Task<List<Event>> GetEvents(string vin);
    }
}
