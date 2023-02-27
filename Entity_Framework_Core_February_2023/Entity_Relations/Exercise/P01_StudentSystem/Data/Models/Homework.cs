namespace P01_StudentSystem.Data.Models;

public enum ContentType
{
    Application,
    Pdf,
    Zip
}

public class Homework
{
    public int	HomeworkId { get; set; }

    public string Content { get; set; } = null!;

    public ContentType ContentType { get; set; }

    public DateTime SubmissionTime { get; set; }

    public int StudentId { get; set; }

    public virtual Student Student { get; set; } = null!;

    public int CourseId { get; set; }

    public virtual Course Course { get; set; } = null!;
}
