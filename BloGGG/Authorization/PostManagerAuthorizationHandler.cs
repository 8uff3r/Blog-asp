using BloGGG.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace BloGGG.Authorization;

public class PostManagerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, PostModel>
{
    protected override Task
        HandleRequirementAsync(AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            PostModel resource)
    {
        if (context.User == null! || resource == null!)
        {
            return Task.CompletedTask;
        }

        if (context.User.IsInRole(Constants.PostManagersRole))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}