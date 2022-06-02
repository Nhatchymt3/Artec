using System;

namespace Outsourcing.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        OutsourcingEntities Get();
    }
}
