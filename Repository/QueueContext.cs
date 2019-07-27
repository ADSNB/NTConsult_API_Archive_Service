using Repository.Entity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Repository
{
    public class QueueContext : DbContext
    {
        public QueueContext(DbContextOptions<QueueContext> options) : base(options) { }

        public DbSet<ProcessingQueueEntity> ProcessingQueue { get; set; }
        public DbSet<ProcessedQueueEntity> ProcessedQueue { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProcessingQueueEntity>().ToTable("AlteryxProcessingQueue");
            modelBuilder.Entity<ProcessedQueueEntity>().ToTable("AlteryxProcessedQueue");
        }
    }
}
