using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using System;
using ConnectedCar.Core.Shared.Data;
using ConnectedCar.Core.Shared.Services;
using ConnectedCar.Core.Services.Context;
using ConnectedCar.Core.Services.Translator;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConnectedCar.Core.Services
{
    public class UserService : BaseService,IUserService
    {
        public UserService(IServiceContext serviceContext, ITranslator translator) : base(serviceContext, translator)
        {
        }

        public async Task CreateUser(User user)
        {
            if (user == null || !user.Validate())
                throw new InvalidOperationException();

            AdminCreateUserRequest createRequest = new AdminCreateUserRequest
            {
                UserPoolId = GetServiceContext().GetServiceConfig().CognitoConfig.UserPoolId,
                Username = user.Username,
                TemporaryPassword = user.Password,
                MessageAction = "SUPPRESS",
                DesiredDeliveryMediums = new List<string> { DeliveryMediumType.EMAIL },
                ForceAliasCreation = false
            };

            await GetServiceContext().GetCognitoProvider().AdminCreateUserAsync(createRequest);
        }

        public async Task SetPermanentPassword(User user)
        {
            if (user == null || !user.Validate())
                throw new InvalidOperationException();

            AdminSetUserPasswordRequest passwordRequest = new AdminSetUserPasswordRequest
            {
                UserPoolId = GetServiceContext().GetServiceConfig().CognitoConfig.UserPoolId,
                Username = user.Username,
                Password = user.Password,
                Permanent = true
            };

            await GetServiceContext().GetCognitoProvider().AdminSetUserPasswordAsync(passwordRequest);
        }


    }
}