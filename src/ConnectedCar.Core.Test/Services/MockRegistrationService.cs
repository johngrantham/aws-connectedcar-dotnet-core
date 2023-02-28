using ConnectedCar.Core.Shared.Services;
using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Shared.Data.Enums;
using ConnectedCar.Core.Shared.Data.Updates;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ConnectedCar.Core.Test.Services
{
    public class MockRegistrationService : IRegistrationService
    {
        private Dictionary<string,Registration> registrations = new Dictionary<string,Registration>();

        public Task CreateRegistration(Registration registration)
        {
            if (registration == null || !registration.Validate())
                throw new InvalidOperationException();

            if (registrations.ContainsKey(GetKey(registration.Username, registration.Vin)))
                throw new InvalidOperationException();

            registration.StatusCode = StatusCodeEnum.Active;
            registration.CreateDateTime = DateTime.Now;
            registration.UpdateDateTime = DateTime.Now;

            registrations.Add(GetKey(registration.Username, registration.Vin), registration);

            return Task.CompletedTask;
        }

        public Task UpdateRegistration(RegistrationPatch patch)
        {
            if (patch == null || !patch.Validate())
                throw new InvalidOperationException();

            string key = GetKey(patch.Username, patch.Vin);

            if (registrations.ContainsKey(key))
            {
                Registration registration = registrations[key];

                registration.StatusCode = patch.StatusCode;
                registration.UpdateDateTime = DateTime.Now;
            }

            return Task.CompletedTask;
        }

        public Task DeleteRegistration(string username, string vin)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(vin))
                throw new InvalidOperationException();

            registrations.Remove(GetKey(username, vin));
                
            return Task.CompletedTask;
        }

        public Task<Registration> GetRegistration(string username, string vin)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(vin))
                throw new InvalidOperationException();

            string key = GetKey(username, vin);
                
            if (registrations.ContainsKey(key))
            {
                return Task.FromResult(registrations[key]);
            }                

            return Task.FromResult<Registration>(null);
        }

        public Task<List<Registration>> GetCustomerRegistrations(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new InvalidOperationException();
                
            var results = registrations
                .Where(p => p.Value.Username == username)
                .Select(p => p.Value)
                .ToList();

            return Task.FromResult(results); 
        }

        public Task<List<Registration>> GetVehicleRegistrations(string vin)
        {
            if (string.IsNullOrEmpty(vin))
                throw new InvalidOperationException();
                
            var results = registrations
                .Where(p => p.Value.Vin == vin)
                .Select(p => p.Value)
                .ToList();

            return Task.FromResult(results); 
        }

        public Task BatchUpdate(List<Registration> registrations)
        {
            if (registrations == null)
                throw new InvalidOperationException();

            return Task.CompletedTask;
        }

        private static string GetKey(string username, string vin) {
            return username + "/" + vin;
        }
    }
}