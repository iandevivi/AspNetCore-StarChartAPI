﻿using System;
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

        [HttpGet("{id:int}", Name ="GetById")]
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
            var ces = _context.CelestialObjects.Where(c=>c.Name == name).ToList();
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
    }
}
