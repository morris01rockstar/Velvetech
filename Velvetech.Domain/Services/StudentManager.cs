using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Velvetech.Domain.Dtos;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Interfaces;

namespace Velvetech.Domain.Services
{
	public class StudentManager : IStudentManager
	{
		private readonly IUnitOfWork _unitOfWork;

		public StudentManager(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IQueryable<StudentDto> Students()
		{
			return _unitOfWork.StudentsRepository.GetAll()
				.Select(s => new StudentDto { Id = s.Id, LastName = s.LastName, FirstName = s.FirstName, MiddleName = s.MiddleName, Gender = s.Gender, Identifier = s.Identifier, Groups = s.StudentGroups.Select(sg => sg.Group.Name) });
		}

		public async Task<Student> CreateAsync(Student student)
		{
			var addStudent = _unitOfWork.StudentsRepository.Add(student);

			await _unitOfWork.SaveChangesAsync();

			return addStudent;
		}

		public async Task<Student> DeleteAsync(Student student)
		{
			var removeStudent = _unitOfWork.StudentsRepository.Remove(student);

			await _unitOfWork.SaveChangesAsync();

			return removeStudent;
		}

		public async Task UpdateAsync(Student student)
		{
			_unitOfWork.StudentsRepository.Update(student);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task<Student> FindByIdAsync(Guid studentId)
		{
			return await _unitOfWork.StudentsRepository.FindFirstAsync(s => s.Id == studentId);
		}

		public async Task AddToGroupAsync(Student student, Group group)
		{
			student.StudentGroups.Add(new StudentGroup { GroupId = group.Id, StudentId = student.Id });

			await _unitOfWork.SaveChangesAsync();
		}

		public async Task RemoveFromGroupAsync(Student student, Group group)
		{
			var studentGroup = student.StudentGroups.FirstOrDefault(sg => sg.GroupId == group.Id);

			student.StudentGroups.Remove(studentGroup);

			await _unitOfWork.SaveChangesAsync();
		}
	}
}
