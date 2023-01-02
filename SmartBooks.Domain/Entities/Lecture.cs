namespace SmartBooks.Domain.Entities;

public class Lecture
{
    
    public long Id { get; set; }
    public long SubjectId { get; set; }
    public string Name { get; set; } = "Lecture";
    public int TotalTasks { get; set; } = 0;
    public int FinishedTasks { get; set; } = 0;
    public decimal? Score { get; set; }
    
    
    public Subject? Subject { get; set; }
}