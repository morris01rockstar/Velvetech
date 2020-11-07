using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Velvetech.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Velvetech.Domain.Dtos;
using System.Collections.Generic;
using Velvetech.Domain.Entities;
using System;

namespace Velvetech.Presentation.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class GroupsController : ControllerBase
	{
		private readonly ILogger<GroupsController> _logger;
		private readonly IGroupManager _groupManager;

		public GroupsController(ILogger<GroupsController> logger, IGroupManager groupManager)
		{
			_logger = logger;
			_groupManager = groupManager;
		}

		[HttpGet]
		public async Task<ActionResult<List<GroupDto>>> Get(string groupName, int page = 1, int pageSize = 10)
		{
			var groups = _groupManager.Groups();

			if (!string.IsNullOrEmpty(groupName))
			{
				groups = groups.Where(g => g.Name == groupName);
			}

			if (page <= 0)
				page = 1;

			return await groups.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Group>> GetGroup(Guid id)
		{
			var group = await _groupManager.FindByIdAsync(id);

			if (group == null)
			{
				return NotFound();
			}

			return group;
		}

		[HttpPost]
		public async Task<ActionResult<Group>> Create([FromBody] Group group)
		{
			try
			{
				if (_groupManager.GroupExists(group.Name))
					return BadRequest("A group with this name already exists");

				return await _groupManager.CreateAsync(group);
			}
			catch (Exception)
			{
				return BadRequest("Unhandled Exception");
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<Group>> Remove(Guid id)
		{
			var group = await _groupManager.FindByIdAsync(id);

			if (group == null)
			{
				return NotFound();
			}

			return await _groupManager.DeleteAsync(group);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Edit(Guid id, Group group)
		{
			if (id != group.Id)
			{
				return BadRequest();
			}

			await _groupManager.UpdateAsync(group);

			return NoContent();
		}
	}
}
