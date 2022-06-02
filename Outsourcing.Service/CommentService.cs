using Outsourcing.Core.Common;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Service
{
    public interface ICommentService
    {

        IEnumerable<Comment> GetComments();
        Comment GetCommentById(int pictureId);
        void CreateComment(Comment picture);
        void EditComment(Comment pictureToEdit);
        void DeleteComment(int pictureId);
        void SaveComment();
        IEnumerable<ValidationResult> CanAddPicture(Comment picture);

    }
    public class CommentService: ICommentService
    {
        #region Field
        private readonly ICommentRepository commontRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public CommentService(ICommentRepository commontRepository, IUnitOfWork unitOfWork)
        {
            this.commontRepository = commontRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Comment> GetComments()
        {
            var comments = commontRepository.GetAll();
            return comments;
        }

        public Comment GetCommentById(int commentId)
        {
            var comment = commontRepository.GetById(commentId);
            return comment;
        }

        public void CreateComment(Comment comment)
        {
            commontRepository.Add(comment);
            SaveComment();
        }

        public void EditComment(Comment commentToEdit)
        {
            commontRepository.Update(commentToEdit);
            SaveComment();
        }

        public void DeleteComment(int commentId)
        {
            //Get picture by id.
            var comment = commontRepository.GetById(commentId);
            if (comment != null)
            {
                comment.Deleted = true;
                commontRepository.Update(comment);
                SaveComment();
            }           
        }


        public void SaveComment()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddPicture(Comment comment)
        {

            //    yield return new ValidationResult("Picture", "ErrorString");
            return null;
        }

        #endregion
    }

}
