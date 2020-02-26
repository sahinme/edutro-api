using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Entities;
using Microsoft.EgitimAPI.ApplicationCore.Services.Question.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.Question
{
    public interface IQuestionAppService
    {
        Task<List<QuestionDto>> GetEntityQuestions(long entityId, EntityType entityType);
    }
}