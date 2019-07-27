using Microsoft.EntityFrameworkCore;
using Repository.Entity;

namespace Repository.EntityRepository
{
    public class ProcessedQueueRepository : GenericRepository<ProcessedQueueEntity>, IProcessedQueueRepository
    {
        public ProcessedQueueRepository(QueueContext dbContext) : base(dbContext) { }
    }
}
