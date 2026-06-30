using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpensesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ExpensesController(AppDbContext context)
        {
                _context = context;
        }

        [HttpGet]
        public IActionResult GetExpenses()
        {
            var expenses=_context.Expenses.ToList();
            return Ok(expenses);
        }

        [HttpPost]
        public IActionResult AddExpense(Expense newExpense)
        {
            _context.Expenses.Add(newExpense);
            _context.SaveChanges();
            return Ok(newExpense);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExpense(int id, Expense updatedExpense)
        {
            var expense = _context.Expenses.FirstOrDefault(x => x.Id == id);
            if (expense == null)
                return NotFound("Expense Not Found !");
            expense.Title=updatedExpense.Title;
            expense.Amount=updatedExpense.Amount;
            expense.Category=updatedExpense.Category;
            expense.Date=updatedExpense.Date;
            _context.SaveChanges();
            return Ok(expense);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteExpense(int id)
        {
            var expense=_context.Expenses.FirstOrDefault(x=>x.Id == id);
            if (expense == null)
                return NotFound("Expenses Not Found !");
            _context.Expenses.Remove(expense);
            _context.SaveChanges();
            return Ok("Expenses Deleted  SuccesFully");
        }
    }
}
