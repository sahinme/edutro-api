using System.Collections.Generic;
using Microsoft.EgitimAPI.ApplicationCore.Entities;
using Microsoft.EgitimAPI.ApplicationCore.Entities.PostComments;
using Microsoft.EgitimAPI.ApplicationCore.Services.Category.Dto;

namespace Microsoft.EgitimAPI.ApplicationCore.Services.PostService.Dto
{
    public class PostDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public string ShortDescription { get; set; }
        public EntityDto EntityDto { get; set; }
        public CategoryDto Category { get; set; }
        public List<PostCommentDto> PostComments { get; set; }
    }

    public class EntityDto
    {
        public long Id { get; set; }
        public string EntityName { get; set; }
        public string EntityProfession { get; set; }
        public string LogoPath { get; set; }
    }
}