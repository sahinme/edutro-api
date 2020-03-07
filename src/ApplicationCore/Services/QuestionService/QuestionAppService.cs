using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Entities;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Answers;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Questions;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.AnswerService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.QuestionService.Dto;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.QuestionService
{
    public class QuestionAppService:IQuestionAppService
    {
        private readonly IAsyncRepository<Question> _questionRepository;
        private readonly IAsyncRepository<Answer> _answerRepository;

        public QuestionAppService(IAsyncRepository<Question> questionRepository,
            IAsyncRepository<Answer> answerRepository)
        {
            _questionRepository = questionRepository;
            _answerRepository = answerRepository;
        }
        
        public async Task<List<QuestionDto>> GetEntityQuestions(long entityId, EntityType entityType)
        {
            var questions = await _questionRepository.GetAll()
                .Where(x => x.EntityId == entityId && x.EntityType == entityType && x.IsDeleted == false).Select(x =>
                    new QuestionDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description,
                        CreatedDateTime = x.CreatedDate
                    }).ToListAsync();
            return questions;
        }

        public async Task<QuestionDetail> GetQuestionById(long id)
        {
            var question = await _questionRepository.GetAll().Where(x => x.Id == id)
                .Include(x=>x.User)
                .Include(x => x.Answers)
                .Select(x => new QuestionDetail
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    CreatedDate = x.CreatedDate,
                    UserName = x.User.Name + " " + x.User.Surname,
                    Answers =  x.Answers.Select(t => new AnswerDto
                        {
                            Id = t.Id,
                            Description = t.Description,
                            CreatedDate = t.CreatedDate,
                            EntityType = t.EntityType
                        }).ToList()
                }).FirstOrDefaultAsync();
            return question;
        }

        public async Task<Question> CreateQuestion(CreateQuestionDto input)
        {
            var question = new Question
            {
                Title = input.Title,
                Description = input.Description,
                EntityType = input.EntityType,
                EntityId = input.EntityId,
                UserId = input.UserId
            };
            await _questionRepository.AddAsync(question);
            return question;
        }

        public async Task<Answer> CreateAnswer(CreateAnswerDto input)
        {
            var answer = new Answer
            {
                Description = input.Description,
                EntityType = input.EntityType,
                EntityId = input.EntityId,
                QuestionId = input.QuestionId
            };
            await _answerRepository.AddAsync(answer);
            return answer;
        }
    }
}