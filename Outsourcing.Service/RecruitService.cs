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
    public interface IRecruitService
    {
        IEnumerable<Recruit> GetRecruits();
        Recruit GetRecruitById(int RecruitId);
        void CreateRecruit(Recruit obj);
        void EditRecruit(Recruit obj);
        void DeleteRecruit(int id);
        void SaveChange();
    }
    public class RecruitService : IRecruitService
    {
        #region Field
        private readonly IRecruitRepository RecruitRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public RecruitService(IRecruitRepository _RecruitRepository, IUnitOfWork unitOfWork)
        {
            this.RecruitRepository = _RecruitRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Recruit> GetRecruits()
        {
            var Recruits = RecruitRepository.GetMany(t => !t.Deleted);
            return Recruits;
        }

        public Recruit GetRecruitById(int RecruitId)
        {
            var Recruit = RecruitRepository.GetById(RecruitId);
            return Recruit;
        }

        public void CreateRecruit(Recruit Recruit)
        {
            RecruitRepository.Add(Recruit);
            SaveChange();
        }

        public void EditRecruit(Recruit RecruitToEdit)
        {
            RecruitRepository.Update(RecruitToEdit);
            SaveChange();
        }

        public void DeleteRecruit(int RecruitId)
        {
            //Get picture by id.
            var Recruit = RecruitRepository.GetById(RecruitId);
            if (Recruit != null)
            {
                Recruit.Deleted = true;
                RecruitRepository.Update(Recruit);
                SaveChange();
            }
        }

        public void SaveChange()
        {
            unitOfWork.Commit();
        }
        #endregion
    }
}
