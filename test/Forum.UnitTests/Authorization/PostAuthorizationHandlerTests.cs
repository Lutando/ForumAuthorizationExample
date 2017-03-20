using System;
using System.Collections.Generic;
using System.Security.Claims;
using Formum.Api.Authorization.Handlers;
using Formum.Api.Authorization.Models;
using Formum.Api.Authorization.Operations;
using Forum.Models;
using Microsoft.AspNetCore.Authorization;
using Xunit;

namespace Forum.UnitTests.Authorization
{
    public class PostAuthorizationHandlerTests
    {
        private const string Category = "PostAuthorizationHandler";
        private static Guid PostIdDefault => new Guid("bc5fb7fa-e938-4de7-aee8-bd60909f1189");
        private static Guid UserIdDefault => new Guid("bfc1a143-4208-42bc-8542-a359d18b505a");
        private static Guid InvalidUserIdDefault => new Guid("38c6ba6a-ac08-4389-8112-727a7825b159");
        private static DateTime CreatedAtDefault => new DateTime(2017,01,01);
        private static string TextDefault => "foo bar";

        //some test helpers
        public Post make_Post(Guid postId, Guid userId, DateTime createdAt, string text)
        {
            return new Post(postId,userId,createdAt,text);
        }

        public Post make_PostDefault()
        {
            return new Post(PostIdDefault,UserIdDefault,CreatedAtDefault,TextDefault);
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleEdit_WhenCalledWithResourceOwner_ShouldSucceed()
        {
            var resource = make_PostDefault();
            var authorizationModel = PostAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", UserIdDefault.ToString()) }));
            var requirement = PostOperations.Edit;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new PostAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            Assert.True(authorizationContext.HasSucceeded);
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleDelete_WhenCalledWithResourceOwner_ShouldSucceed()
        {
            var resource = make_PostDefault();
            var authorizationModel = PostAuthorizationModel.From(resource);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", UserIdDefault.ToString()) }));
            var requirement = PostOperations.Delete;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new PostAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            Assert.True(authorizationContext.HasSucceeded);
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleEdit_WhenCalledWithNonResourceOwner_ShouldFail()
        {
            var resource = make_PostDefault();
            var authorizationModel = PostAuthorizationModel.From(resource);
            //we use an invalid user this time
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", InvalidUserIdDefault.ToString()) }));
            var requirement = PostOperations.Edit;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new PostAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            Assert.False(authorizationContext.HasSucceeded);
        }

        [Fact]
        [Trait("Category", Category)]
        public async void HandleDelete_WhenCalledWithNonResourceOwner_ShouldFail()
        {
            var resource = make_PostDefault();
            var authorizationModel = PostAuthorizationModel.From(resource);
            //we use an invalid user this time
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim("sub", InvalidUserIdDefault.ToString()) }));
            var requirement = PostOperations.Delete;
            var authorizationContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, authorizationModel);
            var authorizationHandler = new PostAuthorizationHandler();

            await authorizationHandler.HandleAsync(authorizationContext);

            Assert.False(authorizationContext.HasSucceeded);
        }
    }
}
