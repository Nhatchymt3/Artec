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
    public interface IPetProfileService
    {

        IEnumerable<PetProfile> GetPetProfiles();
        PetProfile GetPetProfileById(int petProfileId);
        void CreatePetProfile(PetProfile petProfile);
        void EditPetProfile(PetProfile petProfileToEdit);
        void DeletePetProfile(int petProfileId);
        void SavePetProfile();
        IEnumerable<ValidationResult> CanAddPetProfile(PetProfile petProfile);

    }
    public class PetProfileService : IPetProfileService
    {
        #region Field
        private readonly IPetProfileRepository petprofileRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public PetProfileService(IPetProfileRepository _petprofileRepository, IUnitOfWork unitOfWork)
        {
            this.petprofileRepository = _petprofileRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<PetProfile> GetPetProfiles()
        {
            var petprofiles = petprofileRepository.GetAll().Where(t => !t.Deleted);
            return petprofiles;
        }

        public PetProfile GetPetProfileById(int pictureId)
        {
            var petprofile = petprofileRepository.GetById(pictureId);
            return petprofile;
        }

        public void CreatePetProfile(PetProfile petprofile)
        {
            petprofileRepository.Add(petprofile);
            SavePetProfile();
        }

        public void EditPetProfile(PetProfile petprofileToEdit)
        {
            petprofileRepository.Update(petprofileToEdit);
            SavePetProfile();
        }

        public void DeletePetProfile(int petprofileId)
        {
            //Get picture by id.
            var petprofile = petprofileRepository.GetById(petprofileId);
            if (petprofile != null)
            {
                petprofile.Deleted = true;
                petprofileRepository.Update(petprofile);
                SavePetProfile();
            }
        }

        public void SavePetProfile()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddPetProfile(PetProfile petprofile)
        {

            //    yield return new ValidationResult("Picture", "ErrorString");
            return null;
        }

        #endregion
    }
}
