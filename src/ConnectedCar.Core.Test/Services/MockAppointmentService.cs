using ConnectedCar.Core.Shared.Services;
using ConnectedCar.Core.Shared.Data.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ConnectedCar.Core.Test.Services
{
    public class MockAppointmentService : IAppointmentService
    {
        private Dictionary<string,Appointment> appointments = new Dictionary<string,Appointment>();

         public Task<string> CreateAppointment(Appointment appointment)
        {
            if (appointment == null || !appointment.Validate())
                throw new InvalidOperationException();

            appointment.AppointmentId = Guid.NewGuid().ToString();
            appointment.CreateDateTime = DateTime.Now;
            appointment.UpdateDateTime = DateTime.Now;

            appointments.Add(appointment.AppointmentId, appointment);

            return Task.FromResult(appointment.AppointmentId);
        }

        public Task DeleteAppointment(string appointmentId)
        {
            if (string.IsNullOrEmpty(appointmentId))
                throw new InvalidOperationException();

            appointments.Remove(appointmentId);

            return Task.CompletedTask;
        }

        public Task<Appointment> GetAppointment(string appointmentId)
        {
            if (string.IsNullOrEmpty(appointmentId))
                throw new InvalidOperationException();

            if (appointments.ContainsKey(appointmentId))
            {
                Appointment appointment = appointments[appointmentId];
                return Task.FromResult(appointment);
            }

            return Task.FromResult<Appointment>(null);
        }

        public Task<List<Appointment>> GetRegistrationAppointments(string username, string vin)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(vin))
                throw new InvalidOperationException();

            var results = appointments
                .Where(p => p.Value.RegistrationKey.Username == username && p.Value.RegistrationKey.Vin ==  vin)
                .Select(p => p.Value)
                .ToList();

            return Task.FromResult(results);                
        }

        public Task<List<Appointment>> GetTimeslotAppointments(string dealerId, string serviceDateHour)
        {
            if (string.IsNullOrEmpty(dealerId) || string.IsNullOrEmpty(serviceDateHour))
                throw new InvalidOperationException();

            var results = appointments
                .Where(p => p.Value.TimeslotKey.DealerId == dealerId && p.Value.TimeslotKey.ServiceDateHour ==  serviceDateHour)
                .Select(p => p.Value)
                .ToList();

            return Task.FromResult(results);                
        }

        public Task BatchUpdate(List<Appointment> appointments)
        {
            if (appointments == null)
                throw new InvalidOperationException();

            return Task.CompletedTask;
        }
    }
}