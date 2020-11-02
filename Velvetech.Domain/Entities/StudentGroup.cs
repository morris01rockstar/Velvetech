using System;
using System.Collections.Generic;
using System.Text;

namespace Velvetech.Domain.Entities
{
	public class StudentGroup
	{
		public Guid StudentId { get; set; }
		public Student Student { get; set; }

		public Guid GroupId { get; set; }
		public Group Group { get; set; }
	}
}
