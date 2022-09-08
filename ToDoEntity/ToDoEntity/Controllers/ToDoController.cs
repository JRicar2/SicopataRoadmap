using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoEntity.Models;

namespace ToDoEntity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoDBContext context;

        public ToDoController(ToDoDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ToDo>>> Get()
        {
            return Ok(await context.Tasks.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDo>> Get(int id)
        {
            var task = await context.Tasks.FindAsync(id);
            if (task == null)
            {
                return BadRequest("Tarea no encontrada.");
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<List<ToDo>>> AddToDo([FromBody] ToDo task)
        {
            context.Tasks.Add(task);
            await context.SaveChangesAsync();
            return Ok(await context.Tasks.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<ToDo>> UpdateToDo([FromBody] ToDo request)
        {
            var dbTask = await context.Tasks.FindAsync(request.Id);
            if (dbTask == null)
            {
                return BadRequest("Tarea no encontrada.");
            }
            dbTask.Name = request.Name;
            dbTask.Description = request.Description;
            dbTask.isComplete = request.isComplete;
            dbTask.isDeleted = request.isDeleted;
            await context.SaveChangesAsync();

            return Ok("Tarea actualizada");
        }

        [HttpDelete("id")]
        public async Task<ActionResult<List<ToDo>>> DeleteToDo(int id)
        {
            var dbTask =await context.Tasks.FindAsync(id);
            if (dbTask == null)
            {
                return BadRequest("Tarea no encontrada.");
            }

            context.Tasks.Remove(dbTask);
            await context.SaveChangesAsync();
            return Ok(await context.Tasks.ToListAsync());
        }
    }
}
