using System.Threading.Tasks;
using ConnectedCar.Core.Shared.Data.Updates;

namespace ConnectedCar.Core.Shared.Orchestrators
{
    public interface IAdminOrchestrator
    {
        Task CreateCustomer(CustomerProvision provision);
        
        Task CreateCustomerUsingMessage(CustomerProvision provision);
         
    }
}