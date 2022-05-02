namespace Jim.Core.Database.Models
{
    public interface IDatabaseEntity
    {
        public long Id { get; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastModified { get; set; }
    }
}