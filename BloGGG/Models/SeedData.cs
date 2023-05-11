using BloGGG.Data;
using Microsoft.EntityFrameworkCore;

namespace BloGGG.Models;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new PostsContext(serviceProvider.GetRequiredService<DbContextOptions<PostsContext>>());
        if (context.Posts.Any()) return;

        context.Posts.AddRange(
            new PostModel
            {
                PostTitle = "Lorel Ispul",
                PostBody = "Test Post Body Lorem Ipsum Doler Amet",
                PostAuthor = "Me",
                PostTags = new[] { "Music" }
            },
            new PostModel
            {
                PostTitle = "Lorel Ispul 2",
                PostBody = "Test Post 22 Body Lorem Ipsum Doler Amet",
                PostAuthor = "Me",
                PostTags = new[] { "Music", "Philosophy" }
            }
        );
        context.SaveChanges();
    }
}