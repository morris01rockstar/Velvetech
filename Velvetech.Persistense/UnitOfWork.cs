using System;
using System.Threading;
using System.Threading.Tasks;
using Velvetech.Domain.Interfaces;
using Velvetech.Persistense.Repositories;

namespace Velvetech.Persistense
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private ApplicationDbContext _context;

		private IStudentsRepository _studentsRepository;
		private IGroupsRepository _groupsRepository;

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
		}

		public IStudentsRepository StudentsRepository =>
			_studentsRepository ??= new StudentsRepository(_context);

		public IGroupsRepository GroupsRepository =>
			_groupsRepository ??= new GroupsRepository(_context);

		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return await _context.SaveChangesAsync(cancellationToken);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_context != null)
				{
					_context.Dispose();
					_context = null;
				}
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
