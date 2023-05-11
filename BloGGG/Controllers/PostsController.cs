using BloGGG.Data;
using BloGGG.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace BloGGG.Controllers;

public class PostsController : Controller
{
    private readonly PostsContext _context;
    private readonly IServiceProvider _serviceProvider;

    public PostsController(PostsContext postsContext, IServiceProvider serviceProvider)
    {
        _context = postsContext;
        _serviceProvider = serviceProvider;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        return View(await _context.Posts.ToListAsync());
    }


    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(PostModel p)
    {
        p.PostTags = p.PostTagsString.Replace(" ", "").Split(",");
        _context.Add(new PostModel
        {
            PostAuthor = "noname",
            PostBody = p.PostBody,
            PostTags = p.PostTags,
            PostTitle = p.PostTitle
        });
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Posts");
    }

    [HttpPost("[controller]/Rm/{id:int}")]
    public async Task<IActionResult> RemovePost(int id)
    {
        var result = await (from p in _context.Posts
            where p.ID == id
            select p).FirstOrDefaultAsync();
        if (result != null)
        {
            _context.Remove(result);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index", "Posts");
    }

    [HttpGet("[controller]/update/{id:int}")]
    public async Task<IActionResult> Update(int id)
    {
        var result = await (from p in _context.Posts
            where p.ID == id
            select p).FirstOrDefaultAsync();
        Console.WriteLine(result.ToJson());
        return View(result);
    }

    public async Task<IActionResult> Update(PostModel postModel)
    {
        postModel.PostTags = postModel.PostTagsString.Replace(" ", "").Split(",");
        var result = await (from p in _context.Posts
            where p.ID == postModel.ID
            select p).FirstOrDefaultAsync();
        if (result != null)
        {
            _context.Update(result);
            result.PostTags = postModel.PostTags;
            result.PostTitle = postModel.PostTitle;
            result.PostBody = postModel.PostBody;
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index", "Posts");
    }
}