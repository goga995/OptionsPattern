using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using OptionsPattern.Data;

using OptionsPattern.Models;

namespace OptionsPattern.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommandController : ControllerBase
    {
        private readonly CommandDbContext _context;

        public CommandController(CommandDbContext context)
        {
            _context = context;
        }

        // api/command
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var commands = await _context.Commands.ToListAsync();
            if (commands == null)
            {
                return NotFound();
            }
            return Ok(commands);
        }

        // api/command/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var comm = await _context.Commands.FirstOrDefaultAsync(x => x.Id == id);
            if (comm == null)
            {
                return NotFound();
            }
            return Ok(comm);
        }

        // api/command
        [HttpPost]
        public async Task<IActionResult> Create(Command command)
        {
            if (ModelState.IsValid)
            {
                command.Id = _context.Commands.Count() + 1;
                
                await _context.Commands.AddAsync(command);
                await _context.SaveChangesAsync();
               
                return CreatedAtAction("GetById",new {Id = command.Id} , command);
            }

            return new JsonResult("Somthing went wrong");
        }

        // api/command/{id}
        [HttpPatch]
        public async Task<IActionResult> Patch(int id, string name, string comm)
        {
            var command = await _context.Commands.FirstOrDefaultAsync(x => x.Id == id);

            if (command == null)
            {
                return BadRequest("Invalid Id");
            }
            command.Name = name;
            command.Comm = comm;
            _context.Update(command);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var command = await _context.Commands.FirstOrDefaultAsync(x => x.Id == id);

            if (command == null)
            {
                return BadRequest("Invalid Id");
            }

            _context.Commands.Remove(command);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}