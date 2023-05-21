using BloGGG.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace BloGGG.Authorization;

public class PostAdminAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, PostModel>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, PostModel resource)
    {
        if (context.User == null)
        {
            Console.WriteLine("Req {0}", requirement);
            return Task.CompletedTask;
        }

        if (context.User.IsInRole(Constants.PostAdministratorsRole))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}