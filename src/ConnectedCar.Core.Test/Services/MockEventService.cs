using ConnectedCar.Core.Shared.Services;
using ConnectedCar.Core.Shared.Data.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ConnectedCar.Core.Test.Services
{
    public class MockEventService : IEventService
    {
        private Dictionary<string,Event> events = new Dictionary<string,Event>();

        public Task CreateEvent(Event evnt)
        {
            if (evnt == null || !evnt.Validate())
                throw new InvalidOperationException();

            evnt.CreateDateTime = DateTime.Now;
            evnt.UpdateDateTime = DateTime.Now;

            events.Add(GetKey(evnt.Vin, evnt.Timestamp), evnt);

            return null;
        }

        public Task DeleteEvent(string vin, long timestamp)
        {
            if (string.IsNullOrEmpty(vin) || timestamp == 0)
                throw new InvalidOperationException();
                
            events.Remove(GetKey(vin, timestamp));

            return Task.CompletedTask;
        }

        public Task<Event> GetEvent(string vin, long timestamp)
        {
            if (string.IsNullOrEmpty(vin) || timestamp == 0)
                throw new InvalidOperationException();
                
            string key = GetKey(vin, timestamp);

            if (events.ContainsKey(key))
            {
                return Task.FromResult(events[key]);
            }                

            return Task.FromResult<Event>(null);
        }

        public Task<List<Event>> GetEvents(string vin)
        {
            var results = events
                .Where(p => p.Value.Vin == vin)
                .Select(p => p.Value)
                .ToList();

            return Task.FromResult(results); 
        }

        private static string GetKey(string vin, long timestamp) {
            return vin + "/" + timestamp;
        }
    }
}