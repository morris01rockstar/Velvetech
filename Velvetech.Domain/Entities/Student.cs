using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Velvetech.Domain.Entities
{
	public class Student
	{
		public Guid Id { get; set; }

		[Required]
		public string Gender { get; set; }

		[Required]
		[MaxLength(40)]
		public string LastName { get; set; }

		[Required]
		[MaxLength(40)]
		public string FirstName { get; set; }

		[MaxLength(60)]
		public string MiddleName { get; set; }

		public string Identifier { get; set; }

		public List<StudentGroup> StudentGroups { get; set; }

		public Student()
		{
			StudentGroups = new List<StudentGroup>();
		}
	}
}
