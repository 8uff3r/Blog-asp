using Microsoft.EntityFrameworkCore;
using BloGGG.Models;

namespace BloGGG.Data;

public class PostsContext : DbContext
{
    public PostsContext(DbContextOptions<PostsContext> options) : base(options)
    {
    }

    public DbSet<PostModel> Posts { get; set; }
}