namespace SmartBooks.Domain.Entities;

public class Subject
{
    public long Id { get; set; }
    public string Name { get; set; } = "Subject";
    public ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
}