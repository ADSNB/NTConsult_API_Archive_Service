using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.EntityRepository;

public class UnitOfWork : IUnitOfWork, System.IDisposable
{
    private readonly QueueContext _dbContext;
    private IProcessingQueueRepository _processingQueueRepository;
    private IProcessedQueueRepository _processedQueueRepository;

    public UnitOfWork(QueueContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IProcessingQueueRepository ProcessingQueueRepository
    {
        get { return _processingQueueRepository ?? (_processingQueueRepository = new ProcessingQueueRepository(_dbContext)); }
    }

    public IProcessedQueueRepository ProcessedQueueRepository
    {
        get { return _processedQueueRepository ?? (_processedQueueRepository = new ProcessedQueueRepository(_dbContext)); }
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        System.GC.SuppressFinalize(this);
    }
}