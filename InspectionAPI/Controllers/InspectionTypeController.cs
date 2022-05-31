#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InspectionAPI.Data;
using InspectionAPI.Models;

namespace InspectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionTypeController : ControllerBase
    {
        private readonly DataContext _context;

        public InspectionTypeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InspectionType>>> GetInspectionTypes()
        {
            return await _context.InspectionTypes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InspectionType>> GetInspectionType(int id)
        {
            var inspectionType = await _context.InspectionTypes.FindAsync(id);

            if (inspectionType == null)
            {
                return NotFound();
            }

            return inspectionType;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInspectionType(int id, InspectionType inspectionType)
        {
            if (id != inspectionType.Id)
            {
                return BadRequest();
            }

            _context.Entry(inspectionType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InspectionTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<InspectionType>> PostInspectionType(InspectionType inspectionType)
        {
            _context.InspectionTypes.Add(inspectionType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInspectionType", new { id = inspectionType.Id }, inspectionType);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInspectionType(int id)
        {
            var inspectionType = await _context.InspectionTypes.FindAsync(id);
            if (inspectionType == null)
            {
                return NotFound();
            }

            _context.InspectionTypes.Remove(inspectionType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InspectionTypeExists(int id)
        {
            return _context.InspectionTypes.Any(e => e.Id == id);
        }
    }
}
