using Amazon.CognitoIdentityProvider;
using Amazon.DynamoDBv2.DataModel;
using Amazon.SQS;
using ConnectedCar.Core.Shared.Config;

namespace ConnectedCar.Core.Services.Context
{
    public interface IServiceContext
    {
        ServiceConfig GetServiceConfig();

        AmazonCognitoIdentityProviderClient GetCognitoProvider();

        DynamoDBContext GetDynamoDbContext();

        AmazonSQSClient GetSQSClient();

    }
}
