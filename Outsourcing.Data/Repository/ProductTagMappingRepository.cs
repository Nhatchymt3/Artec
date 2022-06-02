using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;

namespace Outsourcing.Data.Repository
{
    public class ProductTagMappingRepository : RepositoryBase<ProductTagMapping>, IProductTagMappingRepository
    {
        public ProductTagMappingRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IProductTagMappingRepository : IRepository<ProductTagMapping>
    {

    }
}
