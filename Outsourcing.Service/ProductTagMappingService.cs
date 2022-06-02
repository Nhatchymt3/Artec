using Outsourcing.Core.Common;
using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using Outsourcing.Data.Repository;
using System.Collections.Generic;

namespace Outsourcing.Service
{
    public interface IProductTagMappingService
    {

        IEnumerable<ProductTagMapping> GetProductTagMappings();
        ProductTagMapping GetProductTagMappingById(int ProductTagMappingId);
        void CreateProductTagMapping(ProductTagMapping ProductTagMapping);
        void EditProductTagMapping(ProductTagMapping ProductTagMappingToEdit);
        void DeleteProductTagMapping(int ProductTagMappingId);
        void SaveProductTagMapping();
        IEnumerable<ValidationResult> CanAddProductTagMapping(ProductTagMapping ProductTagMapping);

    }
    public class ProductTagMappingService : IProductTagMappingService
    {
        #region Field
        private readonly IProductTagMappingRepository ProductTagMappingRepository;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Ctor
        public ProductTagMappingService(IProductTagMappingRepository ProductTagMappingRepository, IUnitOfWork unitOfWork)
        {
            this.ProductTagMappingRepository = ProductTagMappingRepository;
            this.unitOfWork = unitOfWork;
        }
        #endregion

        #region BaseMethod

        public IEnumerable<ProductTagMapping> GetProductTagMappings()
        {
            var ProductTagMappings = ProductTagMappingRepository.GetAll();
            return ProductTagMappings;
        }

        public ProductTagMapping GetProductTagMappingById(int ProductTagMappingId)
        {
            var ProductTagMapping = ProductTagMappingRepository.GetById(ProductTagMappingId);
            return ProductTagMapping;
        }

        public void CreateProductTagMapping(ProductTagMapping ProductTagMapping)
        {
            ProductTagMappingRepository.Add(ProductTagMapping);
            SaveProductTagMapping();
        }

        public void EditProductTagMapping(ProductTagMapping ProductTagMappingToEdit)
        {
            ProductTagMappingRepository.Update(ProductTagMappingToEdit);
            SaveProductTagMapping();
        }

        public void DeleteProductTagMapping(int ProductTagMappingId)
        {
            //Get ProductTagMapping by id.
            var ProductTagMapping = ProductTagMappingRepository.GetById(ProductTagMappingId);
            if (ProductTagMapping != null)
            {
                ProductTagMappingRepository.Delete(ProductTagMapping);
                SaveProductTagMapping();
            }
        }

        public void SaveProductTagMapping()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<ValidationResult> CanAddProductTagMapping(ProductTagMapping ProductTagMapping)
        {

            //    yield return new ValidationResult("ProductTagMapping", "ErrorString");
            return null;
        }

        #endregion
    }
}
