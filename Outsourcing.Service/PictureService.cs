using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outsourcing.Core.Common;
using Outsourcing.Data.Models;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Repository;
using Outsourcing.Service.Properties;

namespace Outsourcing.Service
{
    public interface IPictureService
    {

        IEnumerable<Picture> GetPictures();
        Picture GetPictureById(int pictureId);
        void CreatePicture(Picture picture);
        void EditPicture(Picture pictureToEdit);
        void DeletePicture(int pictureId);
        void SavePicture();
        IEnumerable<ValidationResult> CanAddPicture(Picture picture);

    }
    public class PictureService : IPictureService
    {
        #region Field
        private readonly IPictureRepository pictureRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public PictureService(IPictureRepository pictureRepository, IUnitOfWork unitOfWork)
        {
            this.pictureRepository = pictureRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Picture> GetPictures()
        {
            var pictures = pictureRepository.GetAll();
            return pictures;
        }

        public Picture GetPictureById(int pictureId)
        {
            var picture = pictureRepository.GetById(pictureId);
            return picture;
        }

        public void CreatePicture(Picture picture)
        {
            pictureRepository.Add(picture);
            SavePicture();
        }

        public void EditPicture(Picture pictureToEdit)
        {
            pictureRepository.Update(pictureToEdit);
            SavePicture();
        }

        public void DeletePicture(int pictureId)
        {
            //Get picture by id.
            var picture = pictureRepository.GetById(pictureId);
            if (picture != null)
            {
                pictureRepository.Delete(picture);
                SavePicture();
            }
        }

        public void SavePicture()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddPicture(Picture picture)
        {
        
            //    yield return new ValidationResult("Picture", "ErrorString");
            return null;
        }

        #endregion
    }
}
