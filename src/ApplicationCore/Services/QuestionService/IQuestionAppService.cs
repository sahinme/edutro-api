using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Entities;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Answers;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Questions;
using Microsoft.EgitimAPI.ApplicationCore.Services.AnswerService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.QuestionService.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.QuestionService
{
    public interface IQuestionAppService
    {
        Task<List<QuestionDto>> GetEntityQuestions(long entityId, EntityType entityType);
        Task<QuestionDetail> GetQuestionById(long id);
        Task<Question> CreateQuestion(CreateQuestionDto input);
        Task<Answer> CreateAnswer(CreateAnswerDto input);
    }
}