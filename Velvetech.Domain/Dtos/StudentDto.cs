using System;
using System.Collections.Generic;
using System.Text;

namespace Velvetech.Domain.Dtos
{
	public class StudentDto
	{
		public Guid Id { get; set; }
		public string Gender { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string Identifier { get; set; }
		public IEnumerable<string> Groups { get; set; }
	}
}
