using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp2.Models;

namespace WebApp2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GTPController : ControllerBase
    {
        private readonly GTPContext _context;

        public GTPController(GTPContext context)
        {
            _context = context;

            if (_context.GTPs.Count() == 0)
            {
                _context.GTPs.Add(new MyGTP { Name = "a09" });
                _context.GTPs.Add(new MyGTP { Name = "a10" });
                _context.SaveChanges();
            }
        }

        // GET: api/GTP
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MyGTP>>> GetTodoItems()
        {
            return await _context.GTPs.ToListAsync();
        }

        // GET: api/GTP/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MyGTP>> GetTodoItem(long id)
        {
            var todoItem = await _context.GTPs.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // POST: api/GTP
        [HttpPost]
        public async Task<ActionResult<MyGTP>> PostTodoItem(MyGTP item)
        {
            _context.GTPs.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }

        // PUT: api/GTP/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, MyGTP item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/GTP/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.GTPs.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.GTPs.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}