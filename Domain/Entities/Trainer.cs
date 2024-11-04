namespace Domain.Entities;

public class Trainer : User
{
    public string Specialization { get; set; }
    public int ExperienceYears { get; set; }
    public int Rating { get; set; }
    
    public List<Class> Classes { get; set; } = new List<Class>();
    public List<Service> Services { get; set; } = new List<Service>();
}