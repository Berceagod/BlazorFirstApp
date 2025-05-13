namespace NuclearDataManager.Models
{
    public class ResearchSubmission
    {
        public int Id { get; set; }
        public float Temperature { get; set; }
        public DateTime DateSubmitted { get; set; } = DateTime.Now;
    }

}
