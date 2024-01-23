namespace CampusCrafter.Models;

public class CandidateApplicationBuilder
{
    public int Id { get; set; }
    
    public Candidate? Applicant { get; set; }
    public DateTime Date { get; set; }
    
    public CandidateApplicationStatus Status { get; set; }
    public string? RejectReason { get; set; } = null!;
    
    public Major? Major { get; set; }
    public StudyType StudyType { get; set; }

    public CandidateApplication Build()
    {
        return new CandidateApplication
        {
            Applicant = Applicant,
            Major = Major,
            StudyType = StudyType,
            RejectReason = RejectReason,
            Date = Date,
            Id = Id
        };
    }
}