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
    public interface IFileService
    {

        IEnumerable<File> GetFiles();
        File GetFileById(int pictureId);
        void CreateFile(File picture);
        void EditFile(File pictureToEdit);
        void DeleteFile(int pictureId);
        void SaveFile();
        IEnumerable<ValidationResult> CanAddFile(File picture);

    }
    public class FileService: IFileService
    {
        #region Field
        private readonly IFileRepository fileRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public FileService(IFileRepository fileRepository, IUnitOfWork unitOfWork)
        {
            this.fileRepository = fileRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<File> GetFiles()
        {
            var files = fileRepository.GetAll();
            return files;
        }

        public File GetFileById(int fileId)
        {
            var picture = fileRepository.GetById(fileId);
            return picture;
        }

        public void CreateFile(File file)
        {
            fileRepository.Add(file);
            SaveFile();
        }

        public void EditFile(File fileToEdit)
        {
            fileRepository.Update(fileToEdit);
            SaveFile();
        }

        public void DeleteFile(int fileId)
        {
            //Get picture by id.
            var file = fileRepository.GetById(fileId);
            if (file != null)
            {
                fileRepository.Delete(file);
                SaveFile();
            }
        }

        public void SaveFile()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddFile(File file)
        {

            //    yield return new ValidationResult("Picture", "ErrorString");
            return null;
        }

        #endregion
    }
}
