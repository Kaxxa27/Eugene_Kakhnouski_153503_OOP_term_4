using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiApplication.DAL.Interfaces;
using TaxiApplication.Domain.Entity;

namespace TaxiApplication.DAL.Repositories;

public class EfUnitOfWork : IUnitOfWork
{
	private readonly TaxiApplicationDbContext _context;
	private readonly IRepository<User> _userRepository;

	public EfUnitOfWork(TaxiApplicationDbContext context)
	{
		_context = context;
		_userRepository = new EfRepository<User>(context);
	}

	IRepository<User> IUnitOfWork.UserRepository => _userRepository;

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
