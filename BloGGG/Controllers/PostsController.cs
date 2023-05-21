using BloGGG.Data;
using BloGGG.Models;
using Microsoft.AspNetCore.Mvc;
using BloGGG.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace BloGGG.Controllers;

public class PostsController : Controller
{
    private readonly PostsContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IServiceProvider _serviceProvider;
    private readonly IAuthorizationService _authorizationService;

    public PostsController(PostsContext postsContext, UserManager<IdentityUser> userManager,
        IServiceProvider serviceProvider, IAuthorizationService authorizationService)
    {
        _context = postsContext;
        _userManager = userManager;
        _serviceProvider = serviceProvider;
        _authorizationService = authorizationService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var uid = _userManager.GetUserId(User);
        ViewData["uid"] = uid;
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
        var owner = await _userManager.GetUserAsync(User);
        if (owner == null)
        {
            return null;
        }
        _context.Add(new PostModel
        {
            PostAuthor = "noname",
            PostBody = p.PostBody,
            PostTags = p.PostTags,
            OwnerID = _userManager.GetUserId(User),
            PostTitle = p.PostTitle,
            Owner = owner
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

        return PartialView("posts", await _context.Posts.ToListAsync());
        return RedirectToAction("Index", "Posts");
    }

    [HttpGet("[controller]/update/{id:int}")]
    public async Task<IActionResult> Update(int id)
    {
        var result = await (from p in _context.Posts
            where p.ID == id
            select p).FirstOrDefaultAsync();
        return PartialView("Update", result);
    }


    [HttpPost("[controller]/update/{id:int}")]
    public async Task<IActionResult> Update(PostModel postModel, int id)
    {
        Console.WriteLine(postModel.ToJson());
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

        return PartialView("post", result);
        return RedirectToAction("Index", "Posts");
    }
}