using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.Domain.Entity;

namespace TaxiApplication.DAL.Interfaces;
public interface IUnitOfWork
{
	IRepository<User> UserRepository { get; }
	//IRepository<Driver> DriverRepository { get; }
	public Task RemoveDatbaseAsync();
	public Task CreateDatabaseAsync();
	public Task SaveAllAsync();
}

