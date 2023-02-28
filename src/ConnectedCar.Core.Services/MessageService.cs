using System;
using ConnectedCar.Core.Shared.Data;
using ConnectedCar.Core.Shared.Services;
using ConnectedCar.Core.Services.Context;
using ConnectedCar.Core.Services.Translator;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConnectedCar.Core.Services
{
    public class MessageService : BaseService,IMessageService
    {
        public MessageService(IServiceContext serviceContext, ITranslator translator) : base(serviceContext, translator)
        {
        }

        public async Task SendCreateUser(User user)
        {
            if (user == null || !user.Validate())
                throw new InvalidOperationException();

            string url = GetServiceContext().GetServiceConfig().SQSConfig.UserQueueUrl;
            string message = JsonConvert.SerializeObject(user, Formatting.Indented);

            var sqsClient = GetServiceContext().GetSQSClient();

            await sqsClient.SendMessageAsync(url, message);
        }
    }
}