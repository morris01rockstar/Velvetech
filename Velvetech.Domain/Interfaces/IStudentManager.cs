using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velvetech.Domain.Dtos;
using Velvetech.Domain.Entities;

namespace Velvetech.Domain.Interfaces
{
	public interface IStudentManager
	{
		IQueryable<StudentDto> Students();
		Task<Student> CreateAsync(Student student);
		Task<Student> DeleteAsync(Student student);
		Task UpdateAsync(Student student);
		Task<Student> FindByIdAsync(Guid studentId);
		Task AddToGroupAsync(Student student, Group group);
		Task RemoveFromGroupAsync(Student student, Group group);
	}
}
