using Repository.EntityRepository;

public interface IUnitOfWork
{
    IProcessingQueueRepository ProcessingQueueRepository { get; }
    IProcessedQueueRepository ProcessedQueueRepository { get; }
    void Save();
}