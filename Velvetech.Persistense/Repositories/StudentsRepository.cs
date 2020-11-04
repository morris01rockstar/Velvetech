using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Velvetech.Domain.Dtos;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Interfaces;

namespace Velvetech.Persistense.Repositories
{
	public class StudentsRepository : IStudentsRepository
	{
		private readonly ApplicationDbContext _context;

		public StudentsRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public Student Add(Student student)
		{
			return _context.Students.Add(student).Entity;
		}

		public Student Remove(Student student)
		{
			return _context.Students.Remove(student).Entity;
		}

		public async Task<Student> FindFirstAsync(Expression<Func<Student, bool>> predicate)
		{
			return await _context.Students
				.Include(s => s.StudentGroups)
				.Where(predicate)
				.FirstOrDefaultAsync();
		}

		public IQueryable<Student> GetAll()
		{
			return _context.Students
				.Include(s => s.StudentGroups)
				.ThenInclude(sc => sc.Group);
		}

		public void Update(Student student)
		{
			_context.Entry(student).State = EntityState.Modified;
		}
	}
}
