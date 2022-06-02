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
    public interface ITagService
    {
        IEnumerable<Tag> GetTags();
        Tag GetTagById(int TagId);
        void CreateTag(Tag Tag);
        void EditTag(Tag TagToEdit);
        void DeleteTags(int TagId);
        void SaveTag();
        IEnumerable<ValidationResult> CanAddTag(Tag Tag);

    }
    public class TagService : ITagService
    {

        #region Field
        private readonly ITagRepository tagRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public TagService(ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            this.tagRepository = tagRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod
        public IEnumerable<Tag> GetTags()
        {
            var Tags = tagRepository.GetAll();
            return Tags;
        }
        public Tag GetTagById(int TagId)
        {
            var Tag = tagRepository.GetById(TagId);
            return Tag;
        }

        public void CreateTag(Tag Tag)
        {
            tagRepository.Add(Tag);
            SaveTag();
        }

        public void EditTag(Tag TagToEdit)
        {
            tagRepository.Update(TagToEdit);
            SaveTag();
        }

        public void DeleteTags(int TagId)
        {
            //Get Tag by id.
            var Tag = tagRepository.GetById(TagId);
            if (Tag != null)
            {
                tagRepository.Delete(Tag);
                SaveTag();
            }
        }

        public void SaveTag()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddTag(Tag Tag)
        {

            //    yield return new ValidationResult("Tag", "ErrorString");
            return null;
        }

        #endregion
    }
}
