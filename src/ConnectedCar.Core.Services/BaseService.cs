using ConnectedCar.Core.Services.Context;
using ConnectedCar.Core.Services.Translator;

namespace ConnectedCar.Core.Services
{
    public class BaseService
    {
        private IServiceContext serviceContext;
        private ITranslator translator;

        public BaseService(IServiceContext serviceContext, ITranslator translator)
        {
            this.serviceContext = serviceContext;
            this.translator = translator;
        }

        protected IServiceContext GetServiceContext()
        {
            return serviceContext;
        }

        protected ITranslator GetTranslator() 
        {
            return translator;
        }
    }
}
