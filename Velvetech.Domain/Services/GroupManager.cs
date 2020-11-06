using System;
using System.Linq;
using System.Threading.Tasks;
using Velvetech.Domain.Dtos;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Interfaces;

namespace Velvetech.Domain.Services
{
	public class GroupManager : IGroupManager
	{
		private readonly IUnitOfWork _unitOfWork;

		public GroupManager(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IQueryable<GroupDto> Groups()
		{
			return _unitOfWork.GroupsRepository.GetAll()
				.Select(g => new GroupDto { Id = g.Id, Name = g.Name, StudentsCount = g.StudentGroups.Count() });
		}

		public async Task<Group> CreateAsync(Group group)
		{
			var addGroup = _unitOfWork.GroupsRepository.Add(group);

			await _unitOfWork.SaveChangesAsync();

			return addGroup;
		}

		public async Task<Group> DeleteAsync(Group group)
		{
			var removeGroup = _unitOfWork.GroupsRepository.Remove(group);

			await _unitOfWork.SaveChangesAsync();

			return removeGroup;
		}

		public async Task UpdateAsync(Group group)
		{
			_unitOfWork.GroupsRepository.Update(group);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task<Group> FindByIdAsync(Guid groupId)
		{
			return await _unitOfWork.GroupsRepository.FindFirstAsync(s => s.Id == groupId);
		}

		public async Task<Group> FindByNameAsync(string groupName)
		{
			return await _unitOfWork.GroupsRepository.FindFirstAsync(s => s.Name == groupName);
		}

		public bool GroupExists(string groupName)
		{
			return _unitOfWork.GroupsRepository.GetAll().Any(g => g.Name == groupName);
		}
	}
}
