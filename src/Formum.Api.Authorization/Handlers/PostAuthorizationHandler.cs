using System.Threading.Tasks;
using Formum.Api.Authorization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Formum.Api.Authorization.Handlers
{
    public class PostAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, PostAuthorizationModel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement,
            PostAuthorizationModel resource)
        {
            var noOp = Task.CompletedTask;

            if (requirement.Name == "PostEdit")
            {
                //sub claim is typically the subject claim which is also normally the user id of the caller
                if (context.User.HasClaim("sub", resource.OwnerId.ToString()))
                {
                    context.Succeed(requirement);
                    return noOp;
                }
            }

            if (requirement.Name == "PostDelete")
            {
                if (context.User.HasClaim("sub", resource.OwnerId.ToString()))
                {
                    context.Succeed(requirement);
                    return noOp;
                }
            }

            return noOp;
        }
    }
}
