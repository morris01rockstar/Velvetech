using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Velvetech.Domain.Entities;

namespace Velvetech.Domain.Interfaces
{
	public interface IGroupsRepository
	{
		IQueryable<Group> GetAll();
		Task<Group> FindFirstAsync(Expression<Func<Group, bool>> predicate);
		Group Add(Group group);
		Group Remove(Group group);
	}
}
