using AutoMapper;
using Bugify.API.Data;
using Bugify.API.Models.Domain;
using Bugify.API.Models.DTO;
using Bugify.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bugify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly BugifyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;

        public TaskController(BugifyDbContext dbContext, IMapper mapper, ITaskRepository taskRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _taskRepository = taskRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var domainModel = await _taskRepository.GetAllTasksAsync();
            return Ok(_mapper.Map<List<TaskDto>>(domainModel));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetTaskById([FromRoute] Guid id)
        {
            var domainModel = await _taskRepository.GetTaskByIdAsync(id);
            if (domainModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<TaskDto>(domainModel));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto createTaskDto)
        {
            var domainModel = _mapper.Map<AddTask>(createTaskDto);
            domainModel = await _taskRepository.CreateAsync(domainModel);

            var createdDto = _mapper.Map<TaskDto>(domainModel);
            return CreatedAtAction(nameof(GetTaskById), new { id = domainModel.Id }, createdDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateTask([FromRoute] Guid id, [FromBody]UpdateTaskRequestDto updateTaskRequestDto)
        {
            var domainModel = _mapper.Map<AddTask>(updateTaskRequestDto);

            domainModel = await _taskRepository.UpdateAsync(id, domainModel);
            if (domainModel == null)
                return NotFound();
            return Ok(_mapper.Map<TaskDto>(domainModel));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid id)
        {
            var domainModel = await _taskRepository.DeleteAsync(id);
            if (domainModel == null)
                return NotFound();
            return Ok(_mapper.Map<TaskDto>(domainModel));
        }
    }
}
