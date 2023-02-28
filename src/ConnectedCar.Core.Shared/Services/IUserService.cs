using ConnectedCar.Core.Shared.Data;
using System.Threading.Tasks;

namespace ConnectedCar.Core.Shared.Services
{
    public interface IUserService
    {
        Task CreateUser(User user);

        Task SetPermanentPassword(User user);
    }
}
