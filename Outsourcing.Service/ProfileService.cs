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
    public interface IProfileService
    {

        IEnumerable<Profile> GetProfiles();
        Profile GetProfileById(int profileId);
        void CreateProfile(Profile profile);
        void EditProfile(Profile profileToEdit);
        void DeleteProfile(int profileId);
        void SaveProfile();
        IEnumerable<ValidationResult> CanAddProfile(Profile profile);
    }
    public class ProfileService: IProfileService
    {
        #region Field
        private readonly IProfileRepository profileRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public ProfileService(IProfileRepository profileRepository, IUnitOfWork unitOfWork)
        {
            this.profileRepository = profileRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Profile> GetProfiles()
        {
            var profiles = profileRepository.GetAll().Where(t => !t.Deleted);
            return profiles;
        }

        public Profile GetProfileById(int pictureId)
        {
            var profile = profileRepository.GetById(pictureId);
            return profile;
        }

        public void CreateProfile(Profile profile)
        {
            profileRepository.Add(profile);
            SaveProfile();
        }

        public void EditProfile(Profile profileToEdit)
        {
            profileRepository.Update(profileToEdit);
            SaveProfile();
        }

        public void DeleteProfile(int profileId)
        {
            //Get picture by id.
            var profile = profileRepository.GetById(profileId);
            if (profile != null)
            {
                profile.Deleted = true;
                profileRepository.Update(profile);
                SaveProfile();
            }
        }

        public void SaveProfile()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddProfile(Profile profile)
        {

            //    yield return new ValidationResult("Picture", "ErrorString");
            return null;
        }

        #endregion
    }
}
