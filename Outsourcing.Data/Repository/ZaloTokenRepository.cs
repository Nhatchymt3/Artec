using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Repository
{

    public class ZaloTokenRepository : RepositoryBase<ZaloToken>, IZaloTokenRepository
    {
        public ZaloTokenRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
        
    }
    public interface IZaloTokenRepository : IRepository<ZaloToken>
    {
    }
}
