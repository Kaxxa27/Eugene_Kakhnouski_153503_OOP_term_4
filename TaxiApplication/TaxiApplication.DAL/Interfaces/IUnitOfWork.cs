using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Domain.Entity;
using TaxiApplication.Domain.Entity.Profile;

namespace TaxiApplication.DAL.Interfaces;
public interface IUnitOfWork
{
	IRepository<User> UserRepository { get; }
	IRepository<Client> ClientRepository { get; }
	IRepository<ClientProfile> ClientProfileRepository { get; }
	//IRepository<Driver> DriverRepository { get; }
	public Task RemoveDatbaseAsync();
	public Task CreateDatabaseAsync();
	public Task SaveAllAsync();
}

