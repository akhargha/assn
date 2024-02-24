// Controllers/LinkTypeController.cs
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Data;
using System.Linq;

namespace MyMvcApp.Controllers
{
    public class LinkTypeController : Controller
    {
        private readonly AppDbContext _context;

        public LinkTypeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var linkTypes = _context.LinkTypes.ToList();
            return View(linkTypes);
        }
    }
}
