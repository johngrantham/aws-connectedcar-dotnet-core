using ConnectedCar.Core.Shared.Services;
using ConnectedCar.Core.Shared.Data.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ConnectedCar.Core.Test.Services
{
    public class MockTimeslotService : ITimeslotService
    {
        private Dictionary<string,Timeslot> timeslots = new Dictionary<string,Timeslot>();

        public Task CreateTimeslot(Timeslot timeslot)
        {
            if (timeslot == null || !timeslot.Validate())
                throw new InvalidOperationException();

            timeslot.CreateDateTime = DateTime.Now;
            timeslot.UpdateDateTime = DateTime.Now;

            timeslots.Add(GetKey(timeslot.DealerId, timeslot.ServiceDateHour), timeslot);

            return Task.CompletedTask;
        }

        public Task DeleteTimeslot(string dealerId, string serviceDateHour)
        {
            if (string.IsNullOrEmpty(dealerId) || string.IsNullOrEmpty(serviceDateHour))
                throw new InvalidOperationException();

            timeslots.Remove(GetKey(dealerId, serviceDateHour));
                
            return Task.CompletedTask;
        }

        public Task<Timeslot> GetTimeslot(string dealerId, string serviceDateHour)
        {
            if (string.IsNullOrEmpty(dealerId) || string.IsNullOrEmpty(serviceDateHour))
                throw new InvalidOperationException();

            string key = GetKey(dealerId, serviceDateHour);
                
            if (timeslots.ContainsKey(key))
            {
                return Task.FromResult(timeslots[key]);
            }                

            return Task.FromResult<Timeslot>(null);
        }

        public Task<List<Timeslot>> GetTimeslots(string dealerId, string startDate, string endDate)
        {
             if (string.IsNullOrEmpty(dealerId) || string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate))
                throw new InvalidOperationException();

            var results = timeslots
                .Where(p => p.Value.DealerId == dealerId &&
                       p.Value.ServiceDateHour.Substring(0,10).CompareTo(startDate) >= 0 &&
                       p.Value.ServiceDateHour.Substring(0,10).CompareTo(endDate) <= 0)
                .Select(p => p.Value)
                .ToList();

            return Task.FromResult(results); 
        }

        public Task BatchUpdate(List<Timeslot> timeslots)
        {
            if (timeslots == null)
                throw new InvalidOperationException();

            return Task.CompletedTask;
        }

        private static string GetKey(string dealerId, string serviceDateHour) {
            return dealerId + "/" + serviceDateHour;
        }
    }
}