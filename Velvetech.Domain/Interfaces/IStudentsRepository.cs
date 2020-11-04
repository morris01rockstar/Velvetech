using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Velvetech.Domain.Dtos;
using Velvetech.Domain.Entities;

namespace Velvetech.Domain.Interfaces
{
	public interface IStudentsRepository
	{
		IQueryable<Student> GetAll();
		Task<Student> FindFirstAsync(Expression<Func<Student, bool>> predicate);
		Student Add(Student student);
		Student Remove(Student student);
	}
}
