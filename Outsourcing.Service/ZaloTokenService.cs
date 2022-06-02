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
    public interface IZaloTokenService
    {
        IEnumerable<ZaloToken> GetAll();
        ZaloToken UpdateToken(ZaloToken update);
        void Create(ZaloToken zaloToken);
        void Delete(int Id);
        void Save();

    }
    public class ZaloTokenService : IZaloTokenService
    {
        #region Field
        private readonly IZaloTokenRepository _zaloTokenRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public ZaloTokenService(IZaloTokenRepository zaloTokenRepository, IUnitOfWork unitOfWork)
        {
            this._zaloTokenRepository = zaloTokenRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod
        public IEnumerable<ZaloToken> GetAll()
        {
            return _zaloTokenRepository.GetAll();
        }

        public ZaloToken UpdateToken(ZaloToken update)
        {
            _zaloTokenRepository.Update(update);
            Save();
            return update;
        }

        public void Create(ZaloToken zaloToken)
        {
            _zaloTokenRepository.Add(zaloToken);
            Save();
        }

        
        public void Delete(int Id)
        {
            //Get picture by id.
            var zalotoken  = _zaloTokenRepository.GetById(Id);
            if (zalotoken != null)
            {
                _zaloTokenRepository.Delete(zalotoken);
                Save();
            }
        }
        

        public void Save()
        {
            unitOfWork.Commit();
        }        
        #endregion
    }
}
