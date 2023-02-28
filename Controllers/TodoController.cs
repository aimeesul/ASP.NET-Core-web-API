using ContosoTodo.Models;
using ContosoTodo.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoTodo.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    public TodoController()
    {
    }

    [HttpGet]
    public ActionResult<List<Todo>> GetAll() =>
        TodoService.GetAll();

    [HttpGet("{id}")]
    public ActionResult<Todo> Get(int id)
    {
        var Todo = TodoService.Get(id);

        if(Todo == null)
            return NotFound();

        return Todo;
    }

    [HttpPost]
    public IActionResult Create(Todo Todo)
    {            
        TodoService.Add(Todo);
        return CreatedAtAction(nameof(Get), new { id = Todo.Id }, Todo);
    }

   [HttpPut("{id}")]
    public IActionResult Update(int id, Todo Todo)
    {
        if (id != Todo.Id)
            return BadRequest();
            
        var existingTodo = TodoService.Get(id);
        if(existingTodo is null)
            return NotFound();
    
        TodoService.Update(Todo);           
    
        return NoContent();
    } 

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var Todo = TodoService.Get(id);
    
        if (Todo is null)
            return NotFound();
        
        TodoService.Delete(id);
    
        return NoContent();
    }
}