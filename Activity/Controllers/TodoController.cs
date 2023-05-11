using Activity.Models;
using Activity.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Activity.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoRestRepository _repository;

        public TodoController(TodoRestRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var todos = await _repository.GetTodosAsync();
            return Ok(todos);
        }

        public async Task<IActionResult> Details(int id)
        {
            var todo = await _repository.GetTodoAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Todo todo)
        {
            if (ModelState.IsValid)
            {
                await _repository.CreateTodoAsync(todo);
                return CreatedAtAction(nameof(Index), new { id = todo.Id }, todo);
            }
            return BadRequest(ModelState);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var todo = await _repository.GetTodoAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Todo todo)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateTodoAsync(todo);
                return RedirectToAction(nameof(Index));
            }
            return BadRequest(ModelState);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _repository.GetTodoAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteTodoAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
