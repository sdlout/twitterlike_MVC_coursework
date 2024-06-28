using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using twitterlike_MVC_coursework.DbContext;
using twitterlike_MVC_coursework.Models;

namespace twitterlike_MVC_coursework.Controllers;

public class HomeController : Controller
{
    private readonly ulong _userId = 5;
    
    private readonly twitterlikeDbContext _context;
    
    public HomeController(twitterlikeDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Fetch posts from the database including the related User data
        var posts = await _context.Posts
            .Include(p => p.User)
            .ToListAsync();

        // Pass the posts to the view
        return View(posts);
    }
    
    // New Profile Action
    public async Task<IActionResult> Profile()
    {
        var user = await _context.Users
            .Include(u => u.Posts.OrderByDescending(p => p.WasPosted).Take(5))
            .FirstOrDefaultAsync(u => u.Id == _userId);

        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }
    
    // Action to handle post creation
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] PostModel post)
    {
        if (post == null || string.IsNullOrEmpty(post.ImageUri) || string.IsNullOrEmpty(post.Text))
        {
            return BadRequest(new { success = false, message = "Invalid post data" });
        }
        post.UserId = _userId;
        post.WasPosted = DateTime.Now;
        post.Likes = 0;
        post.Reposts = 0;
        post.Views = 0;

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        return Ok(new { success = true });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}