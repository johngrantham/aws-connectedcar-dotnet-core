using ConnectedCar.Core.Shared.Services;
using ConnectedCar.Core.Shared.Data;
using System.Threading.Tasks;
using System;

namespace ConnectedCar.Core.Test.Services
{
    public class StubMessageService : IMessageService
    {
       public Task SendCreateUser(User user)
        {
            if (user == null || !user.Validate())
                throw new InvalidOperationException();

            return Task.CompletedTask;
        }
    }
}