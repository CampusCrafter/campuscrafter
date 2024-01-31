namespace CampusCrafter.Models;

public class CandidateApplicationBuilder
{
    public Candidate? Applicant { get; set; }
    public DateTime Date { get; set; }
    
    public CandidateApplicationStatus Status { get; set; }
    
    public string? RejectReason { get; set; } = null!;
    
    public Major? Major { get; set; }
    public StudyType StudyType { get; set; }

    public string? ApplicantId { get; set; }
    
    public int? MajorId { get; set; }

    public CandidateApplication Build()
    {
        if ((Major == null && MajorId == null) || (Applicant == null && ApplicantId == null))
        {
            throw new ArgumentNullException();
        }
        return new CandidateApplication
        {
            Applicant = Applicant,
            ApplicantId = ApplicantId,
            Major = Major,
            MajorId = MajorId,
            StudyType = StudyType,
            RejectReason = RejectReason,
            Date = Date,
            Status = Status
        };
    }
}