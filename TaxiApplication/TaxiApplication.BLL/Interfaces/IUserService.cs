using TaxiApplication.Domain.Entity;
using TaxiApplication.Domain.Responce.Interfaces;

namespace TaxiApplication.BLL.Interfaces;

public interface IUserService
{
	Task<IBaseResponse<User>> GetUser(int id);
	Task<IBaseResponse<IEnumerable<User>>> GetAllUsers();
	Task<IBaseResponse<User>> AddUser(User user);
	Task<IBaseResponse<bool>> DeleteUser(int id);
	Task<IBaseResponse<User>> FirstOrDefault(Func<User, bool> filter);
}
