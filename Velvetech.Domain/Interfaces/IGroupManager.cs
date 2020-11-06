using System;
using System.Linq;
using System.Threading.Tasks;
using Velvetech.Domain.Dtos;
using Velvetech.Domain.Entities;

namespace Velvetech.Domain.Interfaces
{
	public interface IGroupManager
	{
		IQueryable<GroupDto> Groups();
		Task<Group> CreateAsync(Group group);
		Task<Group> DeleteAsync(Group group);
		Task UpdateAsync(Group student);
		Task<Group> FindByIdAsync(Guid groupId);
		Task<Group> FindByNameAsync(string groupName);
		bool GroupExists(string groupName);
	}
}
