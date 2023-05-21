using BloGGG.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace BloGGG.Authorization;

public class PostIsOwnerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, PostModel>
{
    private readonly UserManager<IdentityUser> _userManager;

    public PostIsOwnerAuthorizationHandler(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }


    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, PostModel resource)
    {
        Console.WriteLine("Owner:{0}", resource.OwnerID);
        if (context.User == null || resource == null)
        {
            return Task.CompletedTask;
        }

        Console.WriteLine("Owner:{0}", resource.OwnerID);
        if (resource.OwnerID == _userManager.GetUserId(context.User))
        {
            Console.WriteLine("Owner:{0}", resource.OwnerID);
            context.Succeed(requirement);
        }

        Console.WriteLine("Req: {0}", requirement.Name);
        return Task.CompletedTask;
    }
}