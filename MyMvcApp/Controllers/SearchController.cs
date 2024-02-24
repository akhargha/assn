using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Data;
using MyMvcApp.Models;
using MyMvcApp.Models.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace MyMvcApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDbContext _context;
        private const int PageSize = 10;

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
        var postViewModels = _context.Posts
            .Where(p => EF.Functions.Contains(p.Title, searchPhrase) || EF.Functions.Contains(p.Body, searchPhrase) && p.PostTypeId == 1)
            .OrderByDescending(p => p.Score)
            .Skip((page - 1) * PageSize)
            .Take(PageSize)
            .ToList()
            .Select(p => new PostViewModel
            {
                Post = p,
                DisplayName = _context.Users.FirstOrDefault(u => u.Id == p.OwnerUserId)?.DisplayName ?? "Unknown User",
                ReputationScore = _context.Users.FirstOrDefault(u => u.Id == p.OwnerUserId)?.Reputation ?? -1,
                Badges = new List<string>() // Initialize with an empty list; we'll fill it next
            }).ToList();

        // Separately fill in the badges for each post's owner
        foreach (var viewModel in postViewModels)
        {
            if (viewModel.Post?.OwnerUserId != null)
            {
                var badges = _context.Badges
                    .Where(b => b.UserId == viewModel.Post.OwnerUserId)
                    .Select(b => b.Name)
                    .ToList();
                viewModel.Badges = badges;
            }
        }

        var totalPosts = _context.Posts
            .Count(p => EF.Functions.Contains(p.Title, searchPhrase) || EF.Functions.Contains(p.Body, searchPhrase) && p.PostTypeId == 1);

        ViewBag.TotalPages = (int)Math.Ceiling(totalPosts / (double)PageSize);
        ViewBag.Query = query;

        return View(postViewModels);
    }

    return View(new List<PostViewModel>());
}

    }
}
