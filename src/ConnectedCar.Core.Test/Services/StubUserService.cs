using ConnectedCar.Core.Shared.Services;
using ConnectedCar.Core.Shared.Data.Entities;
using ConnectedCar.Core.Shared.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace ConnectedCar.Core.Test.Services
{
    public class StubUserService : IUserService
    {
        public Task CreateUser(User user)
        {
            if (user == null || !user.Validate())
                throw new InvalidOperationException();

            return Task.CompletedTask;
        }

        public Task SetPermanentPassword(User user)
        {
            if (user == null || !user.Validate())
                throw new InvalidOperationException();

            return Task.CompletedTask;
        }

        public Task<string> Authenticate(User user)
        {
            if (user == null || !user.Validate())
                throw new InvalidOperationException();

            return Task.FromResult<string>(null);
        }
    }
}