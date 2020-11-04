using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Velvetech.Domain.Entities;
using Velvetech.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Velvetech.Domain.Dtos;

namespace Velvetech.Presentation.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class StudentsController : ControllerBase
	{
		private readonly ILogger<StudentsController> _logger;
		private readonly IStudentManager _studentManager;

		public StudentsController(ILogger<StudentsController> logger, IStudentManager studentManager)
		{
			_logger = logger;
			_studentManager = studentManager;
		}

		[HttpGet]
		public async Task<IActionResult> Get(string gender, string lastName, string firstName, string middleName, string identifier, string groupName)
		{
			var students = _studentManager.Students();

			if (!string.IsNullOrEmpty(gender))
			{
				students = students.Where(s => s.Gender == gender);
			}

			if (!string.IsNullOrEmpty(lastName))
			{
				students = students.Where(s => s.LastName == lastName);
			}

			if (!string.IsNullOrEmpty(firstName))
			{
				students = students.Where(s => s.FirstName == firstName);
			}

			if (!string.IsNullOrEmpty(middleName))
			{
				students = students.Where(s => s.MiddleName == middleName);
			}

			if (!string.IsNullOrEmpty(identifier))
			{
				students = students.Where(s => s.Identifier == identifier);
			}

			if (!string.IsNullOrEmpty(groupName))
			{
				students = students.Where(s => s.Groups.Contains(groupName));
			}

			return Ok(await students.ToListAsync());
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Student>> GetStudent(Guid id)
		{
			var student = await _studentManager.FindByIdAsync(id);

			if (student == null)
			{
				return NotFound();
			}

			return student;
		}

		[HttpPost]
		public async Task<ActionResult<Student>> Create([FromBody] Student student)
		{
			try
			{
				return await _studentManager.CreateAsync(student);
			}
			catch (DbUpdateException)
			{
				return BadRequest("Identifier is not unique");
			}
			catch (Exception)
			{
				return BadRequest("Unhandled Exception");
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<Student>> Remove(Guid id)
		{
			var student = await _studentManager.FindByIdAsync(id);

			if (student == null)
			{
				return NotFound();
			}

			return await _studentManager.DeleteAsync(student);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Edit(Guid id, Student student)
		{
			if (id != student.Id)
			{
				return BadRequest();
			}

			await _studentManager.UpdateAsync(student);
			
			return NoContent();
		}
	}
}
