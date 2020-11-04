using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Velvetech.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Velvetech.Domain.Dtos;

namespace Velvetech.Presentation.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class GroupsController : ControllerBase
	{
		private readonly ILogger<StudentsController> _logger;
		private readonly IUnitOfWork _unitOfWork;

		public GroupsController(ILogger<StudentsController> logger, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public async Task<IActionResult> Get(string groupName)
		{
			var groups = _unitOfWork.GroupsRepository.GetAll()
				.Select(g => new GroupDto { Id = g.Id, Name = g.Name, StudentsCount = g.StudentGroups.Count() });

			if (!string.IsNullOrEmpty(groupName))
			{
				groups = groups.Where(g => g.Name == groupName);
			}

			return Ok(await groups.ToListAsync());
		}
	}
}
