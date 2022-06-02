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
    public interface IBlogTagMappingService
    {

        IEnumerable<BlogTagMapping> GetBlogTagMappings();
        BlogTagMapping GetBlogTagMappingById(int BlogTagMappingId);
        void CreateBlogTagMapping(BlogTagMapping BlogTagMapping);
        void EditBlogTagMapping(BlogTagMapping BlogTagMappingToEdit);
        void DeleteBlogTagMapping(int BlogTagMappingId);
        void SaveBlogTagMapping();
        IEnumerable<ValidationResult> CanAddBlogTagMapping(BlogTagMapping BlogTagMapping);

    }
    public class BlogTagMappingService : IBlogTagMappingService
    {
        #region Field
        private readonly IBlogTagMappingRepository BlogTagMappingRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public BlogTagMappingService(IBlogTagMappingRepository BlogTagMappingRepository, IUnitOfWork unitOfWork)
        {
            this.BlogTagMappingRepository = BlogTagMappingRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<BlogTagMapping> GetBlogTagMappings()
        {
            var BlogTagMappings = BlogTagMappingRepository.GetAll();
            return BlogTagMappings;
        }

        public BlogTagMapping GetBlogTagMappingById(int BlogTagMappingId)
        {
            var BlogTagMapping = BlogTagMappingRepository.GetById(BlogTagMappingId);
            return BlogTagMapping;
        }

        public void CreateBlogTagMapping(BlogTagMapping BlogTagMapping)
        {
            BlogTagMappingRepository.Add(BlogTagMapping);
            SaveBlogTagMapping();
        }

        public void EditBlogTagMapping(BlogTagMapping BlogTagMappingToEdit)
        {
            BlogTagMappingRepository.Update(BlogTagMappingToEdit);
            SaveBlogTagMapping();
        }

        public void DeleteBlogTagMapping(int BlogTagMappingId)
        {
            //Get BlogTagMapping by id.
            var BlogTagMapping = BlogTagMappingRepository.GetById(BlogTagMappingId);
            if (BlogTagMapping != null)
            {
                BlogTagMappingRepository.Delete(BlogTagMapping);
                SaveBlogTagMapping();
            }
        }

        public void SaveBlogTagMapping()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddBlogTagMapping(BlogTagMapping BlogTagMapping)
        {

            //    yield return new ValidationResult("BlogTagMapping", "ErrorString");
            return null;
        }

        #endregion
    }
}
