using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Velvetech.Domain.Dtos;
using Velvetech.Domain.Interfaces;

namespace Velvetech.Presentation.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AdminController : ControllerBase
	{
		private readonly IStudentManager _studentManager;
		private readonly IGroupManager _groupManager;

		public AdminController(IStudentManager studentManager, IGroupManager groupManager)
		{
			_studentManager = studentManager;
			_groupManager = groupManager;
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> AddStudentToGroup([FromBody] StudentGroupDto studentGroup)
		{
			var student = await _studentManager.FindByIdAsync(studentGroup.StudentId);

			if (student == null)
				return NotFound("Student was not found");

			var group = await _groupManager.FindByNameAsync(studentGroup.GroupName);

			if (group == null)
				return NotFound("Group was not found");

			if (_studentManager.IsInGroup(student, group))
				return BadRequest("Student already is in group");

			await _studentManager.AddToGroupAsync(student, group);

			return Ok();
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> RemoveStudentFromGroup([FromBody] StudentGroupDto studentGroup)
		{
			var student = await _studentManager.FindByIdAsync(studentGroup.StudentId);

			if (student == null)
				return NotFound("Student was not found");

			var group = await _groupManager.FindByNameAsync(studentGroup.GroupName);

			if (group == null)
				return NotFound("Group was not found");

			if (!_studentManager.IsInGroup(student, group))
				return BadRequest("The student is not a member of this group");

			await _studentManager.RemoveFromGroupAsync(student, group);

			return Ok();
		}
	}
}
