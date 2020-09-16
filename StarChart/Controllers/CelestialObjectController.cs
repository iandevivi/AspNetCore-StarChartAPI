using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

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

        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var celestialObject = _context.CelestialObjects.Find(id);
            if (celestialObject == null)
                return NotFound();

            celestialObject.Satellites = _context.CelestialObjects.Where(c => c.OrbitedObjectId == id).ToList();
            return Ok(celestialObject);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var ces = _context.CelestialObjects.Where(c => c.Name == name).ToList();
            if (!ces.Any())
                return NotFound();
            foreach (var ce in ces)
            {
                ce.Satellites = _context.CelestialObjects.Where(c => c.OrbitedObjectId == ce.Id).ToList();
            }
            return Ok(ces);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var ces = _context.CelestialObjects.ToList();
            foreach (var ce in ces)
            {
                ce.Satellites = _context.CelestialObjects.Where(c => c.OrbitedObjectId == ce.Id).ToList();
            }
            return Ok(ces);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CelestialObject celestialObject)
        {
            _context.CelestialObjects.Add(celestialObject);
            _context.SaveChanges();

            return CreatedAtRoute("GetById", new { id = celestialObject.Id }, celestialObject);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CelestialObject celestialObject)
        {
            var ce = _context.CelestialObjects.Find(id);
            if (ce == null)
                return NotFound();
            ce.Name = celestialObject.Name;
            ce.OrbitalPeriod = celestialObject.OrbitalPeriod;
            ce.OrbitedObjectId = celestialObject.OrbitedObjectId;

            _context.CelestialObjects.Update(ce);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}/{name}")]
        public IActionResult RenameObject(int id, string name)
        {
            var celestialObject = _context.CelestialObjects.Find(id);
            if (celestialObject == null)
                return NotFound();

            celestialObject.Name = name;

            _context.CelestialObjects.Update(celestialObject);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var list = _context.CelestialObjects.Where(c => c.Id == id || c.OrbitedObjectId == id).ToList();
            if (list == null)
                return NotFound();

            _context.CelestialObjects.RemoveRange(list);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
