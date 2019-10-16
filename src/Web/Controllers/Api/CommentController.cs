using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EgitimAPI.ApplicationCore.Services.CommentService;
using Microsoft.EgitimAPI.ApplicationCore.Services.CommentService.Dto;

namespace Microsoft.EgitimAPI.Web.Controllers.Api
{
    public class CommentController:BaseApiController
    {
        private readonly ICommentAppService _commentAppService;

        public CommentController(ICommentAppService commentAppService)
        {
            _commentAppService = commentAppService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentDto input)
        {
            try
            {
                await _commentAppService.CreateComment(input);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEntityComments(long entityId, string entityName)
        {
            var comments = await _commentAppService.GetEntityComments(entityId, entityName);
            return Ok(comments);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComment(long commentId)
        {
            await _commentAppService.DeleteComment(commentId);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto input)
        {
            await _commentAppService.UpdateComment(input);
            return Ok();
        }
    }
}