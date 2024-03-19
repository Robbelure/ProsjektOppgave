﻿using ReviewHubAPI.Mappers.Interface;
using ReviewHubAPI.Models.DTO;
using ReviewHubAPI.Models.Entity;
using ReviewHubAPI.Repositories.Interface;
using ReviewHubAPI.Services.Interface;

namespace ReviewHubAPI.Services
{
    public class CommentService : IcommentService
    {
        private IcommentRepository _commentRep;
        private IMapper<CommentEntity, CommentDTO> _commentmapper;
        public CommentService(IcommentRepository commentRep, IMapper<CommentEntity,CommentDTO> commentmapper)
        {
            _commentRep = commentRep;
            _commentmapper = commentmapper;
        }

       

        public async Task<CommentDTO> AddNewComment(CommentDTO dto)
        {
            var comment = await _commentRep.AddNewComment(dto);

            return _commentmapper.MapToDTO(comment) ?? null!;
        }
        public async Task<ICollection<CommentDTO>> GetAllComents(int PageSize, int Pagenummer)
        {
            var comments = await _commentRep.GetAllComents(PageSize, Pagenummer);
            ICollection<CommentDTO> commentsdto = new List<CommentDTO>();

            if(comments != null)
            {
                foreach (var comment in comments) 
                {
                    commentsdto.Add(_commentmapper.MapToDTO(comment));
                }
            }

            return commentsdto ?? null!;
        }

        public async Task<ICollection<CommentDTO>> GetAllComentsByReviewId(int ReviewId)
        {
            var comments = await _commentRep.GetAllComentsByReviewId(ReviewId);
            ICollection<CommentDTO> commentsdto = new List<CommentDTO>();

            if (comments != null)
            {
                foreach (var comment in comments)
                {
                    commentsdto.Add(_commentmapper.MapToDTO(comment));
                }
            }

            return commentsdto ?? null!;
        }

        public async Task<ICollection<CommentDTO>> GetAllComentsByUserId(int UserId)
        {
            var comments = await _commentRep.GetAllComentsByUserId(UserId);
            ICollection<CommentDTO> commentsdto = new List<CommentDTO>();

            if (comments != null)
            {
                foreach (var comment in comments)
                {
                    commentsdto.Add(_commentmapper.MapToDTO(comment));
                }
            }

            return commentsdto ?? null!;
        }

        public async Task<CommentDTO> DeleteCommentById(int Id)
        {
            var comment = await _commentRep.GetCommentById(Id);

            if (comment != null)
            {
                await _commentRep.DeleteCommentById(Id);

            }

            return _commentmapper.MapToDTO(comment!) ?? null!;
        }

        public async Task<CommentDTO> UpdateComment(int Id, CommentDTO dto)
        {
            var updatedate = DateTime.Now;
            var comment = await _commentRep.GetCommentById(Id);

            if (comment != null)
            {
                comment.Updated = updatedate;
                comment.Title = dto.Title;
                comment.Comment = dto.Comment;

            }

            return _commentmapper.MapToDTO(comment!) ?? null!;
        }
    }
}
