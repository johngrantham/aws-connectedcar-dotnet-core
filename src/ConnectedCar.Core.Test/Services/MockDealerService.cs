using ConnectedCar.Core.Shared.Services;
using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Shared.Data.Enums;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ConnectedCar.Core.Test.Services
{
    public class MockDealerService : IDealerService
    {
        private Dictionary<string,Dealer> dealers = new Dictionary<string,Dealer>();

        public Task<string> CreateDealer(Dealer dealer)
        {
            if (dealer == null || !dealer.Validate())
                throw new InvalidOperationException();

            dealer.DealerId = Guid.NewGuid().ToString();
            dealer.CreateDateTime = DateTime.Now;
            dealer.UpdateDateTime = DateTime.Now;

            dealers.Add(dealer.DealerId, dealer);

            return Task.FromResult(dealer.DealerId);
        }

        public Task DeleteDealer(string dealerId)
        {
            if (string.IsNullOrEmpty(dealerId))
                throw new InvalidOperationException();
                
            dealers.Remove(dealerId);

            return Task.CompletedTask;
        }

        public Task<Dealer> GetDealer(string dealerId)
        {
            if (string.IsNullOrEmpty(dealerId))
                throw new InvalidOperationException();
                
            if (dealers.ContainsKey(dealerId))
            {
                return Task.FromResult(dealers[dealerId]);
            }                

            return Task.FromResult<Dealer>(null);
        }

        public Task<List<Dealer>> GetDealers(StateCodeEnum stateCode)
        {
            var filtered = dealers
                .Where(item => item.Value.StateCode == stateCode)
                .Select(item => item.Value)
                .ToList();

            return Task.FromResult(filtered); 
        }
    }
}