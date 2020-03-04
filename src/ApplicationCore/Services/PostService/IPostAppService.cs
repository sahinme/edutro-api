using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Entities;
using Microsoft.EgitimAPI.ApplicationCore.Services.PostService.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.PostService
{
    public interface IPostAppService
    {
        Task CreatePost(CreatePostDto input);
        Task UpdatePost(UpdatePost input);
        Task<List<PostDto>> GetAllPosts();
        Task<List<PostDto>> GetEntityPost(EntityType entityType,long id);
        Task<PostDto> GetPostById(long id);
        Task<List<PostDto>> GetPostByCategory(long categoryId);
        Task<List<PostDto>> GetPostByTitle(string title);
        Task<bool> SuspendPost(long id);
        Task<bool> ActivatePost(long id);
        Task DeletePost(long id);
    }
}