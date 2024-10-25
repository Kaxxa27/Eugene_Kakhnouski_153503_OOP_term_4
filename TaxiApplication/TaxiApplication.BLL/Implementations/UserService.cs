using TaxiApplication.BLL.Interfaces;
using TaxiApplication.DAL.Interfaces;
using TaxiApplication.Domain.Entity;
using TaxiApplication.Domain.Enum;
using TaxiApplication.Domain.Responce;
using TaxiApplication.Domain.Responce.Interfaces;

namespace TaxiApplication.BLL.Implementations;

public class UserService : IUserService
{
	IUnitOfWork _unitOfWork;

	public UserService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<IBaseResponse<User>> AddUser(User user)
	{
		try
		{
			var RealUser = await _unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Name == user.Name);
			if (RealUser is not null)
			{
				return new BaseResponse<User>()
				{
					Description = "Пользователь с таким логином уже сущевствует.",
					StatusCode = StatusCode.UserAlredyExist
				};
			};

			var NewUser = new User()
			{
				Name = user.Name,
				Surname = user.Surname,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				//Role = Enum.Parse<Role>(model.Role),
				Role = Role.User,
				Password = user.Password,
			};

			await _unitOfWork.UserRepository.AddAsync(NewUser);

			return new BaseResponse<User>()
			{
				Data = NewUser,
				Description = "Пользователь добавлен.",
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[UserService.AddUser] error: {ex.Message})");
			return new BaseResponse<User>()
			{
				StatusCode = StatusCode.AllError,
				Description = $"Внутренняя ошибка: {ex.Message}"
			};
		}


	}

	public async Task<IBaseResponse<bool>> DeleteUser(int id)
	{
		try
		{
			var RealUser = await _unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Id == id);
			if (RealUser is null)
			{
				return new BaseResponse<bool>()
				{
					Data = false,
					Description = "Пользователь не найден.",
					StatusCode = StatusCode.UserNotFound
				};
			};


			await _unitOfWork.UserRepository.DeleteAsync(RealUser);

			return new BaseResponse<bool>()
			{
				Data = true,
				StatusCode = StatusCode.OK
			};
		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[UserService.DeleteUser] error: {ex.Message})");
			return new BaseResponse<bool>()
			{
				StatusCode = StatusCode.AllError,
				Description = $"Внутренняя ошибка: {ex.Message}"
			};
		}
	}

	public async Task<IBaseResponse<User>> FirstOrDefault(Func<User, bool> filter)
	{
		try
		{
			var RealUser = _unitOfWork.UserRepository.FirstOrDefaultAsync(filter).Result;
			if (RealUser is null)
			{
				return new BaseResponse<User>()
				{
					Description = "Пользователь не найден.",
					StatusCode = StatusCode.UserNotFound
				};
			}

			return new BaseResponse<User>()
			{
				Data = RealUser,
				Description = "Пользователь найден.",
				StatusCode = StatusCode.OK
			};

		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[UserService.FirstOrDefault] error: {ex.Message})");
			return new BaseResponse<User>()
			{
				StatusCode = StatusCode.AllError,
				Description = $"Внутренняя ошибка: {ex.Message}"
			};

		}
	}

	public async Task<IBaseResponse<IEnumerable<User>>> GetAllUsers()
	{
		try
		{
			var AllUsers = _unitOfWork.UserRepository.ListAllAsync().Result;
			if (AllUsers is null)
			{
				return new BaseResponse<IEnumerable<User>>()
				{
					Description = "Пользователи отсутсвуют.",
					StatusCode = StatusCode.UserNotFound
				};
			}

			return new BaseResponse<IEnumerable<User>>()
			{ 
				Data = AllUsers,
				StatusCode = StatusCode.OK
			};

		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[UserService.GetAllUsers] error: {ex.Message})");
			return new BaseResponse<IEnumerable<User>>()
			{
				StatusCode = StatusCode.AllError,
				Description = $"Внутренняя ошибка: {ex.Message}"
			};
		}
	}

	public async Task<IBaseResponse<User>> GetUser(int id)
	{
		try
		{
			var RealUser = _unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Id == id).Result;
			if (RealUser is null)
			{
				return new BaseResponse<User>()
				{
					Description = "Пользователь отсутсвует.",
					StatusCode = StatusCode.UserNotFound
				};
			}

			return new BaseResponse<User>()
			{
				Data = RealUser,
				StatusCode = StatusCode.OK
			};

		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync($"[UserService.GetAllUsers] error: {ex.Message})");
			return new BaseResponse<User>()
			{
				StatusCode = StatusCode.AllError,
				Description = $"Внутренняя ошибка: {ex.Message}"
			};
		}
	}

    public async Task<IBaseResponse<bool>> UpdateUser(User user)
    {
        try
        {
            var RealUser = _unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Name == user.Name).Result;
            if (RealUser is null)
            {
                return new BaseResponse<bool>()
                {
                    Description = "Пользователь отсутсвует.",
                    StatusCode = StatusCode.UserNotFound
                };
            }
			await _unitOfWork.UserRepository.UpdateAsync(user);

            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.OK
            };

        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync($"[UserService.GetAllUsers] error: {ex.Message})");
			return new BaseResponse<bool>()
			{
				Data = false,
                StatusCode = StatusCode.AllError,
                Description = $"Внутренняя ошибка: {ex.Message}"
            };
        }
    }
}
