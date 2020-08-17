using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EgitimAPI.ApplicationCore.Entities;
using Microsoft.EgitimAPI.ApplicationCore.Services.AnswerService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.QuestionService;
using Microsoft.EgitimAPI.ApplicationCore.Services.QuestionService.Dto;

namespace Microsoft.EgitimAPI.Web.Controllers.Api
{
    public class QuestionController:BaseApiController
    {
        private readonly IQuestionAppService _questionAppService;

        public QuestionController(IQuestionAppService questionAppService)
        {
            _questionAppService = questionAppService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetEntityQuestions(long entityId, EntityType entityType)
        {
           var result = await _questionAppService.GetEntityQuestions(entityId, entityType);
           return Ok(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUserQuestions(long userId)
        {
            var result = await _questionAppService.GetUserQuestions(userId);
            return Ok( new {success=true,message="Questions successfuly fetched",result} );
        }
        
        [HttpGet]
        public async Task<IActionResult> GetQuestionDetail(long id)
        {
            var result = await _questionAppService.GetQuestionById(id);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateQuestion(CreateQuestionDto input)
        {
                var result = await _questionAppService.CreateQuestion(input);
                return Ok(result);
            
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateAnswer(CreateAnswerDto input)
        {
            var result = await _questionAppService.CreateAnswer(input);
            return Ok(result);
            
        }
    }
}