using System;
using System.Threading;
using System.Threading.Tasks;

namespace Velvetech.Domain.Interfaces
{
	public interface IUnitOfWork
	{
		IStudentsRepository StudentsRepository { get; }
		IGroupsRepository GroupsRepository { get; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
	}
}
