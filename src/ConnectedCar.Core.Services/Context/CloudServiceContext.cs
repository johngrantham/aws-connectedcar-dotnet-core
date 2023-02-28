using Amazon.CognitoIdentityProvider;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.SQS;
using System;
using System.Runtime.CompilerServices;
using ConnectedCar.Core.Shared.Config;

namespace ConnectedCar.Core.Services.Context
{
    public class CloudServiceContext : BaseServiceContext,IServiceContext
    {
        private ServiceConfig serviceConfig = null;
        private AmazonCognitoIdentityProviderClient cognitoClient = null;
        private DynamoDBContext dbContext = null;
        private AmazonSQSClient sqsClient = null;

        public CloudServiceContext()
        {
            this.Initialize();
        }

        public override ServiceConfig GetServiceConfig()
        {
            if (serviceConfig == null)
            {
                ServiceConfig config = new ServiceConfig
                {
                    AccessConfig = new AccessConfig
                    { 
                        Region = Environment.GetEnvironmentVariable("AWS_REGION")
                    },
                    CognitoConfig = new CognitoConfig
                    {
                        UserPoolId = Environment.GetEnvironmentVariable("UserPoolId")
                    },
                    DynamoConfig = new DynamoConfig
                    {
                        DealerTableName = Environment.GetEnvironmentVariable("DealerTableName"),
                        TimeslotTableName = Environment.GetEnvironmentVariable("TimeslotTableName"),
                        AppointmentTableName = Environment.GetEnvironmentVariable("AppointmentTableName"),
                        RegistrationTableName = Environment.GetEnvironmentVariable("RegistrationTableName"),
                        CustomerTableName = Environment.GetEnvironmentVariable("CustomerTableName"),
                        VehicleTableName = Environment.GetEnvironmentVariable("VehicleTableName"),
                        EventTableName = Environment.GetEnvironmentVariable("EventTableName")
                    },
                    SQSConfig = new SQSConfig
                    {
                        UserQueueUrl = Environment.GetEnvironmentVariable("UserQueueUrl")
                    }
                };

                serviceConfig = config;
            }

            return serviceConfig;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public AmazonCognitoIdentityProviderClient GetCognitoProvider()
        {
            if (cognitoClient == null) {
                var client = new AmazonCognitoIdentityProviderClient();
                this.cognitoClient = client;
            }

            return cognitoClient;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DynamoDBContext GetDynamoDbContext()
        {
            if (dbContext == null)
            {
                var client = new AmazonDynamoDBClient();
                var context = new DynamoDBContext(client, new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 });
                this.dbContext = context;
            }

            return dbContext;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public AmazonSQSClient GetSQSClient()
        {
            if (sqsClient == null)
            {
                var client = new AmazonSQSClient();
                sqsClient = client;
            }

            return sqsClient;
        }
    }
}
