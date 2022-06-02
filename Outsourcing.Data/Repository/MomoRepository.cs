using Outsourcing.Data.Infrastructure;
using Outsourcing.Data.Models;
using System;
using System.Linq.Expressions;
namespace Outsourcing.Data.Repository
{
    public class MomoRepository : RepositoryBase<MomoModel>, IMomoRepository
    {
        public MomoRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IMomoRepository : IRepository<MomoModel>
    {

    }
}