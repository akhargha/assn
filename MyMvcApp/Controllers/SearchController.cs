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
        private const int PageSize = 10; // Define the number of posts per page

        public SearchController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string query, int page = 1)
{
    ViewBag.CurrentPage = page;
    if (!string.IsNullOrEmpty(query))
    {
        var searchPhrase = $"\"{query}\"";
        var totalPosts = _context.Posts
                                .Where(p => (EF.Functions.Contains(p.Title, searchPhrase) || EF.Functions.Contains(p.Body, searchPhrase))
                                        && p.PostTypeId == 1) // Filter for PostTypeId = 1
                                .Count();

        ViewBag.TotalPages = (int)Math.Ceiling(totalPosts / (double)PageSize);
        ViewBag.Query = query;

        var posts = _context.Posts
                    .Where(p => (EF.Functions.Contains(p.Title, searchPhrase) || EF.Functions.Contains(p.Body, searchPhrase))
                            && p.PostTypeId == 1)
                    .OrderBy(p => p.Score) // Order posts by
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();

        return View(posts);
    }
    return View(new List<Post>());
}
    }
}
