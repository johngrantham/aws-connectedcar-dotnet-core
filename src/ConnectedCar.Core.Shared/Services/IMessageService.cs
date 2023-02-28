using ConnectedCar.Core.Shared.Data;
using System.Threading.Tasks;

namespace ConnectedCar.Core.Shared.Services
{
    public interface IMessageService
    {
        Task SendCreateUser(User user);
    }
}
