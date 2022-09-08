using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoEntity.Models;

namespace ToDoEntity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private static List<ToDo> todo = new List<ToDo>
        {
            new ToDo { Id = 1, Name ="Completar Crud", Description="Completar este crud usando Entity", isDeleted=false, isComplete = false},
            new ToDo { Id = 2, Name ="Post", Description="Probar Post", isDeleted=false, isComplete = true}

        };

        [HttpGet]
        public async Task<ActionResult<List<ToDo>>> Get()
        {
            return Ok(todo);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDo>> Get(int id)
        {
            var task = todo.Find(t => t.Id == id);
            if (task == null)
            {
                return BadRequest("Tarea no encontrada.");
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<List<ToDo>>> AddToDo([FromBody] ToDo task)
        {
            todo.Add(task);
            return Ok(todo);
        }

        [HttpPut]
        public async Task<ActionResult<ToDo>> UpdateToDo([FromBody] ToDo request)
        {
            var task = todo.Find(t => t.Id == request.Id);
            if (task == null)
            {
                return BadRequest("Tarea no encontrada.");
            }
            task.Name = request.Name;
            task.Description = request.Description;
            task.isComplete = request.isComplete;
            task.isDeleted = request.isDeleted;
            return Ok("Tarea actualizada");
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult<List<ToDo>>> DeleteToDo(string name)
        {
            var task = todo.Find(t => t.Name == name);
            if (task == null)
            {
                return BadRequest("Tarea no encontrada.");
            }

            todo.Remove(task);
            return Ok(todo);
        }
    }
}
