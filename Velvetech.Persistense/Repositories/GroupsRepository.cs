using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Interfaces;

namespace Velvetech.Persistense.Repositories
{
	public class GroupsRepository : IGroupsRepository
	{
		private readonly ApplicationDbContext _context;

		public GroupsRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public Group Add(Group group)
		{
			return _context.Groups.Add(group).Entity;
		}

		public Group Remove(Group group)
		{
			return _context.Groups.Remove(group).Entity;
		}

		public async Task<Group> FindFirstAsync(Expression<Func<Group, bool>> predicate)
		{
			return await _context.Groups
				//.Include(s => s.StudentGroups)
				.Where(predicate)
				.FirstOrDefaultAsync();
		}

		public IQueryable<Group> GetAll()
		{
			return _context.Groups
				.Include(g => g.StudentGroups)
				.ThenInclude(g => g.Student);
		}
	}
}
