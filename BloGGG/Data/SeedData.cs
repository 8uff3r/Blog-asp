using BloGGG.Authorization;
using BloGGG.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BloGGG.Data;

public class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
    {
        // For sample purposes seed both with the same password.
        // Password is set with the following:
        // dotnet user-secrets set SeedUserPW <pw>
        // The admin user can do anything

        var adminId = await EnsureUser(serviceProvider, testUserPw, "admin@8uff3r.tech");
        await EnsureRole(serviceProvider, adminId, Constants.PostAdministratorsRole);

        // allowed user can create and edit contacts that they create
        var managerId = await EnsureUser(serviceProvider, testUserPw, "manager@8uff3r.tech");
        await EnsureRole(serviceProvider, managerId, Constants.PostManagersRole);

        using var context = new PostsContext(serviceProvider.GetRequiredService<DbContextOptions<PostsContext>>());
        if (context.Posts.Any()) return;
        var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

        var admin = await userManager.FindByIdAsync(adminId);
        if (admin == null)
        {
            return;
        }

        context.Posts.AddRange(
            new PostModel
            {
                PostTitle = "Lorel Ispul",
                PostBody = "Test Post Body Lorem Ipsum Doler Amet",
                PostAuthor = "Me",
                OwnerID = adminId,
                PostTags = new[] { "Music" },
                // Owner = admin
            },
            new PostModel
            {
                PostTitle = "Lorel Ispul 2",
                PostBody = "Test Post 22 Body Lorem Ipsum Doler Amet",
                PostAuthor = "Me",
                OwnerID = adminId,
                PostTags = new[] { "Music", "Philosophy" },
                Owner = admin
            }
        );
        context.SaveChanges();
    }

    private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
        string testUserPw, string userName)
    {
        var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

        var user = await userManager!.FindByNameAsync(userName);
        if (user == null)
        {
            user = new IdentityUser
            {
                UserName = userName,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(user, testUserPw);
        }

        if (user == null)
        {
            throw new Exception("The password is probably not strong enough!");
        }

        return user.Id;
    }

    private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
        string uid, string role)
    {
        var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

        if (roleManager == null)
        {
            throw new Exception("roleManager null");
        }

        IdentityResult IR;
        if (!await roleManager.RoleExistsAsync(role))
        {
            IR = await roleManager.CreateAsync(new IdentityRole(role));
        }

        var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

        if (userManager == null)
        {
            throw new Exception("userManager is null");
        }

        var user = await userManager.FindByIdAsync(uid);

        if (user == null)
        {
            throw new Exception("The testUserPw password was probably not strong enough!");
        }

        IR = await userManager.AddToRoleAsync(user, role);

        return IR;
    }
}