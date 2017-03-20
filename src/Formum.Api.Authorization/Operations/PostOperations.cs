using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Formum.Api.Authorization.Operations
{
    public class PostOperations
    {
        public static OperationAuthorizationRequirement Delete = new OperationAuthorizationRequirement { Name = "PostDelete" };
        public static OperationAuthorizationRequirement Edit = new OperationAuthorizationRequirement { Name = "PostEdit" };
    }
}
