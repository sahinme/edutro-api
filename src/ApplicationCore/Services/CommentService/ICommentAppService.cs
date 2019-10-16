using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Comments;
using Microsoft.EgitimAPI.ApplicationCore.Services.CommentService.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.CommentService
{
    public interface ICommentAppService
    {
        Task CreateComment(CreateCommentDto input);
        Task<PagedResultDto<CommentDto>> GetEntityComments(long entityId, string entityName);
        Task DeleteComment(long id);
        Task UpdateComment(UpdateCommentDto input);
    }
}