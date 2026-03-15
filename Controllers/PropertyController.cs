using Microsoft.AspNetCore.Mvc;
using RealEstateAuction.Data;
using RealEstateAuction.Models;
using System.Linq;

namespace WebBDS.Controllers
{
    public class PropertyController : Controller
    {
        private readonly AppDbContext _context;

        public PropertyController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Projects()
        {
            var data = _context.Properties.ToList();
            return View(data);
        }
    }
}