using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace BloGGG.Authorization;

public static class PostOperations
{
    public static OperationAuthorizationRequirement Create =
        new OperationAuthorizationRequirement { Name = Constants.CreateOperationName };
}

public static class Constants
{
    public const string CreateOperationName = "Create";
    public const string PostAdministratorsRole = "PostAdministrators";
    public const string PostManagersRole = "PostManagers";
}