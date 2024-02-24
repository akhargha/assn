using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Data;
using System.Linq;
using MyMvcApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MyMvcApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDbContext _context;

        public SearchController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                // Escaping the search query to handle special characters or phrases correctly
                var searchPhrase = $"\"{query}\""; // Encloses the search term in double quotes
                var posts = _context.Posts
                                    .Where(p => EF.Functions.Contains(p.Title, searchPhrase) || EF.Functions.Contains(p.Body, searchPhrase))
                                    .ToList();
                return View(posts);
            }
            return View(new List<Post>()); // Return an empty list if the query is null or empty to avoid errors
        }
    }
}
