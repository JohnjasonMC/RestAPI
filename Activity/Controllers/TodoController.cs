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
            return View(todos);
        }

        public async Task<IActionResult> Details(int id)
        {
            var todo = await _repository.GetTodoAsync(id);
            return View(todo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Todo todo)
        {
            if(ModelState.IsValid)
            {
                await _repository.CreateTodoAsync(todo);
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var todo = await _repository.GetTodoAsync(id);
            return View(todo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Todo todo)
        {
            if(ModelState.IsValid)
            {
                await _repository.UpdateTodoAsync(todo);
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _repository.GetTodoAsync(id);
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
