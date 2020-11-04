using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Velvetech.Domain.Entities
{
	public class StudentGroup
	{
		public Guid StudentId { get; set; }
		[JsonIgnore]
		public Student Student { get; set; }

		public Guid GroupId { get; set; }
		[JsonIgnore]
		public Group Group { get; set; }
	}
}
