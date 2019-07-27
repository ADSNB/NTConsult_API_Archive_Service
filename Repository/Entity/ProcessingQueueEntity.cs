using System;

namespace Repository.Entity
{
    public class ProcessingQueueEntity : IEntity
    {
        public int Id { get; set; }
        public int ProcessingStatus { get; set; }
        public int CodUser { get; set; }
        public string Email { get; set; }
        public string CodWorkflow { get; set; }
        public string Questions { get; set; }
        public string OutputPath { get; set; }
        public string AlteryxJob { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
