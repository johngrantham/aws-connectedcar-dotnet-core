using ConnectedCar.Core.Shared.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectedCar.Core.Shared.Services
{
    public interface ITimeslotService
    {
        Task CreateTimeslot(Timeslot timeslot);

        Task DeleteTimeslot(string dealerId, string serviceDateHour);

        Task<Timeslot> GetTimeslot(string dealerId, string serviceDateHour);

        Task<List<Timeslot>> GetTimeslots(string dealerId, string startDate, string endDate);

        Task BatchUpdate(List<Timeslot> timeslots);
    }
}
