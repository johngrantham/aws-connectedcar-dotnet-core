using ConnectedCar.Core.Shared.Data.Enums;
using ConnectedCar.Core.Shared.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConnectedCar.Core.Shared.Services
{
    public interface IDealerService
    {
        Task<string> CreateDealer(Dealer dealer);

        Task DeleteDealer(string dealerId);

        Task<Dealer> GetDealer(string dealerId);

        Task<List<Dealer>> GetDealers(StateCodeEnum stateCode);
    }
}
