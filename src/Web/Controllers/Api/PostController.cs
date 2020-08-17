using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EgitimAPI.ApplicationCore.Entities;
using Microsoft.EgitimAPI.ApplicationCore.Services.PostService;
using Microsoft.EgitimAPI.ApplicationCore.Services.PostService.Dto;


namespace Microsoft.EgitimAPI.Web.Controllers.Api
{
    public class PostController:BaseApiController
    {
        private readonly IPostAppService _postAppService;
        
        public  PostController(IPostAppService postAppService)
        {
            _postAppService = postAppService;
        }
        
        [HttpGet]
        public async Task<List<PostDto>>  GetAllPosts()
        {
            return await _postAppService.GetAllPosts();
        }

        [HttpGet("by-id")]
        public async Task<PostDto>  GetPost(long id)
        {
            return await   _postAppService.GetPostById(id);
        }
        
        [HttpGet]
        public async Task<List<PostDto>>  GetPostByCategory(long categoryId)
        {
            return await  _postAppService.GetPostByCategory(categoryId);
        }
        
        [HttpGet]
        public async Task<List<PostDto>>  GetPostByTitle(string title)
        {
            return await  _postAppService.GetPostByTitle(title);
        }
        [HttpGet]
        public async Task<List<PostDto>>  GetEntityPost(EntityType entityType,long id)
        {
            return await  _postAppService.GetEntityPost(entityType,id);
        }
        
        
        [HttpDelete]
        public async Task<IActionResult>  DeletePost(long id)
        {
            await _postAppService.DeletePost(id);
            return Ok("deleted post");
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostDto input)
        {
            try
            {
                await _postAppService.CreatePost(input);
                return Ok("Created Tenant");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdatePost([FromForm] UpdatePost input)
        {
            try
            {
                await _postAppService.UpdatePost(input);
                return Ok("Created Tenant");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> SuspendPost(long id)
        {
            try
            {
                var result = await _postAppService.SuspendPost(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> ActivatePost(long id)
        {
            try
            {
                var result = await _postAppService.ActivatePost(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}