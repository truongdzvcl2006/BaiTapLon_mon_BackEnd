using Microsoft.AspNetCore.Mvc;
using RealEstateAuction.Data;
using PropertyModel = RealEstateAuction.Models.Property;

namespace RealEstateAuction.Controllers
{
    public class BatDongSanController : Controller
    {
        private readonly AppDbContext _context;

        public BatDongSanController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var list = _context.Properties.ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RealEstateAuction.Models.Property property)
        {
            if (ModelState.IsValid)
            {
                _context.Properties.Add(property);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(property);
        }
        public class AccountController : Controller
        {
            public IActionResult Login()
            {
                return View();
            }

            public IActionResult Register()
            {
                return View();
            }
        }
    }
}