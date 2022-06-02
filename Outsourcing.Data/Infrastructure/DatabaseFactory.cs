namespace Outsourcing.Data.Infrastructure
{
public class DatabaseFactory : Disposable, IDatabaseFactory
{
    private OutsourcingEntities dataContext;
    public OutsourcingEntities Get()
    {
        return dataContext ?? (dataContext = new OutsourcingEntities());
    }
    protected override void DisposeCore()
    {
        if (dataContext != null)
            dataContext.Dispose();
    }
}
}
