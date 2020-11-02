using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Velvetech.Domain.Entities
{
	public class Group
	{
		public Guid Id { get; set; }

		[Required]
		[MaxLength(25)]
		public string Name { get; set; }

		public List<StudentGroup> StudentGroups { get; set; }

		public Group()
		{
			StudentGroups = new List<StudentGroup>();
		}
	}
}
