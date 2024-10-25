using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.DAL.Interfaces;
using TaxiApplication.Domain.Entity;
using TaxiApplication.Domain.Entity.Profile;

namespace TaxiApplication.DAL.Repositories;

public class EfUnitOfWork : IUnitOfWork
{
	private readonly TaxiApplicationDbContext _context;
	private readonly IRepository<Client> _clientRepository;
	private readonly IRepository<User> _userRepository;
	private readonly IRepository<ClientProfile> _clientProfileRepository;

	public EfUnitOfWork(TaxiApplicationDbContext context)
	{
		_context = context;
		_clientRepository = new EfRepository<Client>(context);
		_userRepository = new EfRepository<User>(context);
		_clientProfileRepository = new EfRepository<ClientProfile>(context);
	}

	IRepository<Client> IUnitOfWork.ClientRepository => _clientRepository;
	IRepository<User> IUnitOfWork.UserRepository => _userRepository;
	IRepository<ClientProfile> IUnitOfWork.ClientProfileRepository => _clientProfileRepository;

	public async Task CreateDatabaseAsync()
	{
		await _context.Database.EnsureCreatedAsync();
	}
	public async Task RemoveDatbaseAsync()
	{
		await _context.Database.EnsureDeletedAsync();
	}
	public async Task SaveAllAsync()
	{
		await _context.SaveChangesAsync();
	}
}
