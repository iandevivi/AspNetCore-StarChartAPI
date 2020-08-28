using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{   
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        
        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var celestialObject = _context.CelestialObjects.Where(c => c.Id == id);
            if (celestialObject == null)
                return NotFound();
            
            return Ok(celestialObject);
        }
        
        [HttpGet]
        public IActionResult GetByName(string name)
        {
            return Ok(_context.CelestialObjects.Where(c => c.Name == name));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.CelestialObjects.ToList());
        }
    }
}
