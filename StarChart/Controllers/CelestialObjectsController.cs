using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    public class CelestialObjectsController : Controller
    {

        private readonly ApplicationDbContext _context;
        public CelestialObjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
