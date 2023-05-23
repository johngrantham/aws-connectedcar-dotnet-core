using Amazon.CognitoIdentityProvider;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Amazon;
using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using ConnectedCar.Core.Shared.Config;

namespace ConnectedCar.Core.Services.Context
{
    public class LocalServiceContext : BaseServiceContext,IServiceContext
    {
        private ServiceConfig serviceConfig = null;

        public LocalServiceContext()
        {
            this.Initialize();
        }

        public override ServiceConfig GetServiceConfig()
        {
            if (serviceConfig == null)
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .AddJsonFile(@"serviceConfig.json")
                    .Build();
				
                ServiceConfig config = new ServiceConfig
                {
                    AccessConfig = new AccessConfig
                    { 
                        Region = configuration.GetSection("AccessConfig:Region").Value,
                        AccessKeyId = configuration.GetSection("AccessConfig:AccessKey").Value,
                        SecretAccessKey = configuration.GetSection("AccessConfig:SecretKey").Value,
                        SessionToken = configuration.GetSection("AccessConfig:SessionToken").Value
                    },
                    CognitoConfig = new CognitoConfig
                    {
                        UserPoolId = configuration.GetSection("CognitoConfig:UserPoolId").Value
                    },
                    DynamoConfig = new DynamoConfig
                    {
                        DealerTableName = configuration.GetSection("DynamoConfig:DealerTableName").Value,
                        TimeslotTableName = configuration.GetSection("DynamoConfig:TimeslotTableName").Value,
                        AppointmentTableName = configuration.GetSection("DynamoConfig:AppointmentTableName").Value,
                        RegistrationTableName = configuration.GetSection("DynamoConfig:RegistrationTableName").Value,
                        CustomerTableName = configuration.GetSection("DynamoConfig:CustomerTableName").Value,
                        VehicleTableName = configuration.GetSection("DynamoConfig:VehicleTableName").Value,
                        EventTableName = configuration.GetSection("DynamoConfig:EventTableName").Value
                    },
                    SQSConfig = new SQSConfig
                    {
                        UserQueueUrl = configuration.GetSection("SQSConfig:UserQueueUrl").Value
                    }
                };

                serviceConfig = config;
            }

            return serviceConfig;
        }

        public AmazonCognitoIdentityProviderClient GetCognitoProvider()
        {
            var credentials = new SessionAWSCredentials(
                GetServiceConfig().AccessConfig.AccessKeyId, 
                GetServiceConfig().AccessConfig.SecretAccessKey,
                GetServiceConfig().AccessConfig.SessionToken);

            var region = RegionEndpoint.GetBySystemName(GetServiceConfig().AccessConfig.Region);
            return new AmazonCognitoIdentityProviderClient(credentials, region);
        }

        public DynamoDBContext GetDynamoDbContext()
        {
            var credentials = new SessionAWSCredentials(
                GetServiceConfig().AccessConfig.AccessKeyId, 
                GetServiceConfig().AccessConfig.SecretAccessKey,
                GetServiceConfig().AccessConfig.SessionToken);

            var region = RegionEndpoint.GetBySystemName(GetServiceConfig().AccessConfig.Region);
            var dbClient = new AmazonDynamoDBClient(credentials, region);
            return new DynamoDBContext(dbClient, new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 });
        }

        public AmazonSQSClient GetSQSClient()
        {
            var credentials = new SessionAWSCredentials(
                GetServiceConfig().AccessConfig.AccessKeyId, 
                GetServiceConfig().AccessConfig.SecretAccessKey,
                GetServiceConfig().AccessConfig.SessionToken);

            var region = RegionEndpoint.GetBySystemName(GetServiceConfig().AccessConfig.Region);
            var config = new AmazonSQSConfig { RegionEndpoint = region };
            return new AmazonSQSClient(credentials, config);
        }

    }
}
