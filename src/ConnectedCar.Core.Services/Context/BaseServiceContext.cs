using Amazon;
using ConnectedCar.Core.Shared.Config;
using ConnectedCar.Core.Services.Data.Items;

namespace ConnectedCar.Core.Services.Context
{
    public abstract class BaseServiceContext
    {
        protected void Initialize()
        {
            AWSConfigsDynamoDB.Context.TypeMappings[typeof(DealerItem)] = new Amazon.Util.TypeMapping(
                typeof(DealerItem),
                GetServiceConfig().DynamoConfig.DealerTableName);

            AWSConfigsDynamoDB.Context.TypeMappings[typeof(TimeslotItem)] = new Amazon.Util.TypeMapping(
                typeof(TimeslotItem),
                GetServiceConfig().DynamoConfig.TimeslotTableName);

            AWSConfigsDynamoDB.Context.TypeMappings[typeof(AppointmentItem)] = new Amazon.Util.TypeMapping(
                typeof(AppointmentItem),
                GetServiceConfig().DynamoConfig.AppointmentTableName);

            AWSConfigsDynamoDB.Context.TypeMappings[typeof(RegistrationItem)] = new Amazon.Util.TypeMapping(
                typeof(RegistrationItem),
                GetServiceConfig().DynamoConfig.RegistrationTableName);

            AWSConfigsDynamoDB.Context.TypeMappings[typeof(CustomerItem)] = new Amazon.Util.TypeMapping(
                typeof(CustomerItem),
                GetServiceConfig().DynamoConfig.CustomerTableName);

            AWSConfigsDynamoDB.Context.TypeMappings[typeof(VehicleItem)] = new Amazon.Util.TypeMapping(
                typeof(VehicleItem),
                GetServiceConfig().DynamoConfig.VehicleTableName);

            AWSConfigsDynamoDB.Context.TypeMappings[typeof(EventItem)] = new Amazon.Util.TypeMapping(
                typeof(EventItem),
                GetServiceConfig().DynamoConfig.EventTableName);
        }

        public abstract ServiceConfig GetServiceConfig();
    }
}
