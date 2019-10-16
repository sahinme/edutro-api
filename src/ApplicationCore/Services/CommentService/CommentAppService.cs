using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Comments;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.CommentService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.Dto;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.CommentService
{
    public class CommentAppService:ICommentAppService
    {
        private readonly IAsyncRepository<Comment> _commentRepository;

        public CommentAppService(IAsyncRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }
        
        public async Task CreateComment(CreateCommentDto input)
        {
            var comment = new Comment
            {
                EntityId = input.EntityId,
                EntityName = input.EntityName,
                Content = input.Content,
                UserId = input.CommentatorId
            };
            await _commentRepository.AddAsync(comment);
        }

        public async Task UpdateComment(UpdateCommentDto input)
        {
            var comment = await _commentRepository.GetByIdAsync(input.Id);
            comment.Content = input.Content;
            comment.ModifiedDate=DateTime.Now;
            await _commentRepository.UpdateAsync(comment);
        }

        public async Task<PagedResultDto<CommentDto>> GetEntityComments(long entityId, string entityName)
        {
            var comments = await _commentRepository.GetAll()
                .Where(x => x.EntityId == entityId && x.EntityName == entityName && x.IsDeleted==false)
                .Include(x=>x.User)
                .Select(x => new CommentDto
                {
                    Id = x.Id,
                    Content = x.Content,
                    Date = x.CreatedDate,
                    Commentator = new CommentatorDto
                    {
                        Id = x.User.Id,
                        FullName = x.User.Name+" "+x.User.Surname,
                    }
                }).ToListAsync();
            
            var data = new PagedResultDto<CommentDto>
            {
                Results = comments,
                Count = comments.Count
            };
            return data;
        }

        public async Task DeleteComment(long id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            comment.IsDeleted = true;
            await _commentRepository.UpdateAsync(comment);
        }
    }
}