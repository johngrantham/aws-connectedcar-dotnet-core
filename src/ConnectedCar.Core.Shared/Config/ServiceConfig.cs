namespace ConnectedCar.Core.Shared.Config
{
    public class ServiceConfig
    {
        public AccessConfig AccessConfig { get; set; }

        public CognitoConfig CognitoConfig { get; set; }

        public DynamoConfig DynamoConfig { get; set; }

        public SQSConfig SQSConfig { get; set; }
    }
}
