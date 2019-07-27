using Microsoft.EntityFrameworkCore;
using Repository.Entity;

namespace Repository.EntityRepository
{
    public class ProcessingQueueRepository : GenericRepository<ProcessingQueueEntity>, IProcessingQueueRepository
    {
        public ProcessingQueueRepository(QueueContext dbContext) : base(dbContext) { }
    }
}
