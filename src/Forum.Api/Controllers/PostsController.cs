using Forum.Interfaces;
using System;
using System.Threading.Tasks;
using Formum.Api.Authorization.Models;
using Formum.Api.Authorization.Operations;
using Forum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Controllers
{
    public class PostsController : Controller
    {
        public IPostRepository PostRepository { get; private set; }
        public IAuthorizationService AuthorizationService { get; private set; }
        public PostsController(IPostRepository postRepository, IAuthorizationService authorizationService)
        {
            PostRepository = postRepository;
            AuthorizationService = authorizationService;
        }

        [HttpGet("{postId:Guid}", Name = "GetPost")]
        public async Task<IActionResult> GetPost(Guid postId)
        {
            var post = await PostRepository.Get(postId);

            if (post == null)
            {
                return NotFound();
            }

            return Json(post);
        }

        [HttpPost(Name = "AddPost")]
        public async Task<IActionResult> AddPost([FromBody] Post post)
        {
            await PostRepository.Add(post);

            return CreatedAtAction("GetPost", new {postId = post.Id}, post);
        }

        [HttpPut("{postId:Guid}", Name = "EditPost")]
        public async Task<IActionResult> EditPost([FromBody] Post post)
        {
            var entity = await PostRepository.Get(post.Id);

            if (entity == null)
            {
                return NotFound();
            }

            //now for authZ
            var authorizationModel = PostAuthorizationModel.From(post);
            if (!await AuthorizationService.AuthorizeAsync(User, authorizationModel, PostOperations.Edit))
            {
                //forbidden since authorization handler challenged the request
                return new ForbidResult();
            }

            await PostRepository.Update(post);

            return NoContent();
        }

        [HttpDelete("{postId:Guid}")]
        public async Task<IActionResult> DeletePost(Guid postId)
        {
            var entity = await PostRepository.Get(postId);

            if (entity == null)
            {
                return NotFound();
            }

            //now for authZ
            var authorizationModel = PostAuthorizationModel.From(entity);
            if (!await AuthorizationService.AuthorizeAsync(User, authorizationModel, PostOperations.Edit))
            {
                //forbidden since authorization handler challenged the request
                return new ForbidResult();
            }

            await PostRepository.Delete(postId);

            return NoContent();
        }
    }
}
