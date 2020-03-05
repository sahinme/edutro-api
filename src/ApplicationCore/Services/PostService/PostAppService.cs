using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EgitimAPI.ApplicationCore.Entities;
using Microsoft.EgitimAPI.ApplicationCore.Entities.Posts;
using Microsoft.EgitimAPI.ApplicationCore.Interfaces;
using Microsoft.EgitimAPI.ApplicationCore.Services.BlobService;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category.Dto;
using Microsoft.EgitimAPI.ApplicationCore.Services.PostService.Dto;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.PostService
{
    public class PostAppService:IPostAppService
    {
        private readonly IAsyncRepository<Post> _postRepository;
        private readonly IBlobService _blobService;

        public PostAppService(IAsyncRepository<Post> postRepository,IBlobService blobService)
        {
            _postRepository = postRepository;
            _blobService = blobService;
        }
        
        public async Task CreatePost(CreatePostDto input)
        {
            var post = new Post
            {
                Content = input.Content,
                Title = input.Title,
                CategoryId = input.CategoryId,
                ShortDescription = input.ShortDescription,
                PostState = PostState.Active,
                ImagePath = await _blobService.InsertFile(input.ImageFile)
            };
            if (input.EducatorId != null)
            {
                post.EducatorId = input.EducatorId;
                post.EntityType = EntityType.Educator;
            }

            if (input.TenantId != null)
            {
                post.TenantId = input.TenantId;
                post.EntityType = EntityType.Tenant;
            }
            await _postRepository.AddAsync(post);
        }

        public async Task<bool> SuspendPost(long id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null) return false;
            post.PostState = PostState.Suspended;
            await _postRepository.UpdateAsync(post);
            return true;
        }
        
        public async Task<bool> ActivatePost(long id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null) return false;
            post.PostState = PostState.Active;
            await _postRepository.UpdateAsync(post);
            return true;
        }
        
        public async Task DeletePost(long id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null) throw new Exception("Post bulunamadi");
            post.IsDeleted = true;
        }
        
        public async Task UpdatePost(UpdatePost input)
        {
            var post = await _postRepository.GetByIdAsync(input.Id);
            post.Title = input.Title;
            post.ShortDescription = input.ShortDescription;
            post.Content = input.Content;
            post.CategoryId = input.CategoryId;

            if (input.ImageFile != null)
            {
                var imagePath =  await _blobService.InsertFile(input.ImageFile);
                post.ImagePath = imagePath;
            }

            await _postRepository.UpdateAsync(post);
        }

        public async Task<List<PostDto>> GetAllPosts()
        {
            var posts = await _postRepository.GetAll().Where(x => x.IsDeleted == false && x.PostState == PostState.Active && x.Tenant!=null)
                .Include(x=> x.Tenant)
                .Include(x=>x.PostComments).ThenInclude(x=>x.User)
                .Select(x => new PostDto
                {
                    Id = x.Id,
                    Content = x.Content,
                    Title = x.Title,
                    CreatedDate = x.CreatedDate,
                    ImagePath =BlobService.BlobService.GetImageUrl(x.ImagePath),
                    ShortDescription = x.ShortDescription,
                    Category = new CategoryDto
                    {
                        Id = x.Category.Id,
                        DisplayName = x.Category.DisplayName
                    },
                   EntityDto = new EntityDto
                   {
                       Id = x.Tenant.Id,
                       EntityName = x.Tenant.TenantName,
                       EntityProfession = x.Tenant.Title,
                       LogoPath = BlobService.BlobService.GetImageUrl(x.Tenant.LogoPath)
                   },
                   PostComments = x.PostComments.Where(c=>c.IsDeleted==false).Select(c=>new PostCommentDto
                   {
                       Content = c.Content,
                       CreatedDate = c.CreatedDate,
                       UserName = c.User.Name
                   }).ToList()
                }).ToListAsync();
            
            var posts2 = await _postRepository.GetAll().Where(x => x.IsDeleted == false && x.PostState == PostState.Active && x.EducatorId!=null)
                .Include(x=> x.Educator)
                .Include(x=>x.PostComments).ThenInclude(x=>x.User)
                .Select(x => new PostDto
                {
                    Id = x.Id,
                    Content = x.Content,
                    Title = x.Title,
                    ImagePath =BlobService.BlobService.GetImageUrl(x.ImagePath),
                    ShortDescription = x.ShortDescription,
                    Category = new CategoryDto
                    {
                        Id = x.Category.Id,
                        DisplayName = x.Category.DisplayName
                    },
                    EntityDto = new EntityDto
                    {
                        Id = x.Educator.Id,
                        EntityName = x.Educator.Name+" "+x.Educator.Surname,
                        EntityProfession = x.Educator.Profession,
                        LogoPath = BlobService.BlobService.GetImageUrl(x.Educator.ProfileImagePath)
                    },
                    PostComments = x.PostComments.Where(c=>c.IsDeleted==false).Select(c=>new PostCommentDto
                    {
                        Content = c.Content,
                        CreatedDate = c.CreatedDate,
                        UserName = c.User.Name
                    }).ToList()
                }).ToListAsync();
            var mergedList = posts.Union(posts2).ToList();
            return mergedList;
        }

        public async Task<List<PostDto>> GetPostByCategory(long categoryId)
        {
            var posts = await _postRepository.GetAll().Include(x=>x.Tenant).Include(x=>x.Educator)
                .Include(x=>x.PostComments).ThenInclude(x=>x.User)
                .Where(x => x.IsDeleted == false && x.PostState == PostState.Active && x.CategoryId==categoryId).Select(x => new PostDto
                {
                    Id = x.Id,
                    Content = x.Content,
                    Title = x.Title,
                    ImagePath =BlobService.BlobService.GetImageUrl(x.ImagePath),
                    ShortDescription = x.ShortDescription,
                    Category = new CategoryDto
                    {
                        Id = x.Category.Id,
                        DisplayName = x.Category.DisplayName
                    },
                    EntityDto = x.EntityType == EntityType.Tenant ? new EntityDto
                    {
                        Id = x.Tenant.Id,
                        EntityName = x.Tenant.TenantName,
                        EntityProfession = x.Tenant.Title,
                        LogoPath = BlobService.BlobService.GetImageUrl(x.Tenant.LogoPath)
                    } : new EntityDto
                    {
                        Id   = x.Educator.Id,
                        EntityName = x.Educator.Name+" "+x.Educator.Surname,
                        EntityProfession = x.Educator.Profession,
                        LogoPath = BlobService.BlobService.GetImageUrl(x.Educator.ProfileImagePath)
                    },
                    PostComments = x.PostComments.Where(c=>c.IsDeleted==false).Select(c=>new PostCommentDto
                    {
                        Content = c.Content,
                        CreatedDate = x.CreatedDate,
                        UserName = c.User.Name
                    }).ToList()
                }).ToListAsync();
            return posts;
        }

        public async Task<List<PostDto>> GetPostByTitle(string title)
        {
            var posts = await _postRepository.GetAll().Include(x=>x.Tenant).Include(x=>x.Educator)
                .Include(x=>x.PostComments).ThenInclude(x=>x.User)
                .Where(x => (x.IsDeleted == false && x.PostState == PostState.Active) && (x.Title.Contains(title) || x.ShortDescription.Contains(title))).Select(x => new PostDto
                {
                    Id = x.Id,
                    Content = x.Content,
                    Title = x.Title,
                    ImagePath =BlobService.BlobService.GetImageUrl(x.ImagePath),
                    ShortDescription = x.ShortDescription,
                    Category = new CategoryDto
                    {
                        Id = x.Category.Id,
                        DisplayName = x.Category.DisplayName
                    },
                    EntityDto = x.EntityType == EntityType.Tenant ? new EntityDto
                    {
                        Id = x.Tenant.Id,
                        EntityName = x.Tenant.TenantName,
                        EntityProfession = x.Tenant.Title,
                        LogoPath = BlobService.BlobService.GetImageUrl(x.Tenant.LogoPath)
                    } : new EntityDto
                    {
                        Id   = x.Educator.Id,
                        EntityName = x.Educator.Name+" "+x.Educator.Surname,
                        EntityProfession = x.Educator.Profession,
                        LogoPath = BlobService.BlobService.GetImageUrl(x.Educator.ProfileImagePath)
                    },
                    PostComments = x.PostComments.Where(c=>c.IsDeleted==false).Select(c=>new PostCommentDto
                    {
                        Content = c.Content,
                        CreatedDate = x.CreatedDate,
                        UserName = c.User.Name
                    }).ToList()
                }).ToListAsync();
            return posts;
        }
        
        public async Task<List<PostDto>> GetEntityPost(EntityType entityType,long id)
        {
            var posts = await _postRepository.GetAll().Where(x => x.IsDeleted == false && (x.EntityType == entityType) &&
                                                                  (x.TenantId == id || x.EducatorId == id))
                .Include(x=>x.Tenant)
                .Include(x=>x.Educator)
                .Include(x=>x.PostComments).ThenInclude(x=>x.User)
                .Select(x => new PostDto
                {
                    Id = x.Id,
                    Content = x.Content,
                    Title = x.Title,
                    ImagePath =BlobService.BlobService.GetImageUrl(x.ImagePath),
                    CreatedDate = x.CreatedDate,
                    ShortDescription = x.ShortDescription,
                    Category = new CategoryDto
                    {
                        Id = x.Category.Id,
                        DisplayName = x.Category.DisplayName
                    },
                    EntityDto = x.EntityType == EntityType.Tenant ? new EntityDto
                    {
                        Id = x.Tenant.Id,
                        EntityName = x.Tenant.TenantName,
                        EntityProfession = x.Tenant.Title,
                        LogoPath = BlobService.BlobService.GetImageUrl(x.Tenant.LogoPath)
                    } : new EntityDto
                    {
                        Id   = x.Educator.Id,
                        EntityName = x.Educator.Name+" "+x.Educator.Surname,
                        EntityProfession = x.Educator.Profession,
                        LogoPath = BlobService.BlobService.GetImageUrl(x.Educator.ProfileImagePath)
                    },
                    PostComments = x.PostComments.Where(c=>c.IsDeleted==false).Select(c=>new PostCommentDto
                    {
                        Content = c.Content,
                        CreatedDate = x.CreatedDate,
                        UserName = c.User.Name
                    }).ToList()
                }).ToListAsync();
            return posts;
        }
        
        public async Task<PostDto> GetPostById(long id)
        {
            var prevPost = await _postRepository.GetByIdAsync(id);
            if (prevPost.TenantId != null)
            {
                var post = await _postRepository.GetAll().Include(x=>x.Tenant)
                    .Include(x=>x.PostComments).ThenInclude(x=>x.User)
                    .Where(x => x.IsDeleted == false && x.PostState == PostState.Active && x.Id==id).Select(x => new PostDto
                    {
                        Id = x.Id,
                        Content = x.Content,
                        ImagePath =BlobService.BlobService.GetImageUrl(x.ImagePath),
                        Title = x.Title,
                        CreatedDate = x.CreatedDate,
                        ShortDescription = x.ShortDescription,
                        Category = new CategoryDto
                        {
                            Id = x.Category.Id,
                            DisplayName = x.Category.DisplayName
                        },
                        EntityDto = new EntityDto
                        {
                            Id = x.Tenant.Id,
                            EntityName = x.Tenant.TenantName,
                            EntityProfession = x.Tenant.Title,
                            LogoPath = BlobService.BlobService.GetImageUrl(x.Tenant.LogoPath)
                        },
                        PostComments = x.PostComments.Where(c=>c.IsDeleted==false).Select(c=>new PostCommentDto
                        {
                            Content = c.Content,
                            CreatedDate = x.CreatedDate,
                            UserName = c.User.Name
                        }).ToList()
                    }).FirstOrDefaultAsync();
                return post;
            }
            
            var post2 = await _postRepository.GetAll().Include(x=>x.Educator)
                .Include(x=>x.PostComments).ThenInclude(x=>x.User)
                .Where(x => x.IsDeleted == false && x.PostState == PostState.Active && x.Id==id).Select(x => new PostDto
                {
                    Id = x.Id,
                    Content = x.Content,
                    Title = x.Title,
                    ImagePath =BlobService.BlobService.GetImageUrl(x.ImagePath),
                    CreatedDate = x.CreatedDate,
                    ShortDescription = x.ShortDescription,
                    Category = new CategoryDto
                    {
                        Id = x.Category.Id,
                        DisplayName = x.Category.DisplayName
                    },
                    EntityDto = new EntityDto
                    {
                        
                        Id = x.Educator.Id,
                        EntityName = x.Educator.Name+" "+x.Educator.Surname,
                        EntityProfession = x.Educator.Profession,
                        LogoPath = BlobService.BlobService.GetImageUrl(x.Educator.ProfileImagePath)
                    },
                    PostComments = x.PostComments.Where(c=>c.IsDeleted==false).Select(c=>new PostCommentDto
                    {
                        Content = c.Content,
                        CreatedDate = x.CreatedDate,
                        UserName = c.User.Name
                    }).ToList()
                }).FirstOrDefaultAsync();
            return post2;
        }
    }
}