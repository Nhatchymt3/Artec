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
    public interface ILocationService
    {
        IEnumerable<Location> GetLocations();
        Location GetLocationById(int LocationId);
        void CreateLocation(Location obj);
        void EditLocation(Location obj);
        void DeleteLocation(int id);
        void SaveChange();
    }
    public class LocationService : ILocationService
    {
        #region Field
        private readonly ILocationRepository LocationRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public LocationService(ILocationRepository _LocationRepository, IUnitOfWork unitOfWork)
        {
            this.LocationRepository = _LocationRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<Location> GetLocations()
        {
            var Locations = LocationRepository.GetMany(t => !t.Deleted);
            return Locations;
        }

        public Location GetLocationById(int LocationId)
        {
            var Location = LocationRepository.GetById(LocationId);
            return Location;
        }

        public void CreateLocation(Location Location)
        {
            LocationRepository.Add(Location);
            SaveChange();
        }

        public void EditLocation(Location LocationToEdit)
        {
            LocationRepository.Update(LocationToEdit);
            SaveChange();
        }

        public void DeleteLocation(int LocationId)
        {
            //Get picture by id.
            var Location = LocationRepository.GetById(LocationId);
            if (Location != null)
            {
                Location.Deleted = true;
                LocationRepository.Update(Location);
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
