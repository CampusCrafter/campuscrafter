using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CampusCrafter.Models;

public record CandidateApplication
{
    [Key] public int Id { get; set; }
    
    [ForeignKey("Applicant")] public string? ApplicantId { get; set; }
    public required Candidate? Applicant { get; set; }
    
    public DateTime Date { get; set; }
    public CandidateApplicationStatus Status { get; set; }
    [MaxLength(1024)] public string? RejectReason { get; set; } = null!;
    
    [ForeignKey("Major")] public int? MajorId { get; set; }
    public Major? Major { get; set; }
    public required StudyType StudyType { get; set; }

    public decimal Score => Major?.StudyPlans.FirstOrDefault(plan => plan.StudyType == StudyType)?.AcceptanceCriteria.ScoreWeights
        .Join(Applicant.Progresses, scoreWeight => scoreWeight.ProgressType, progress => progress.Type,
            (scoreWeight, progress) => new { progress.Type, progress.Score, scoreWeight.Weight })
        .Sum(data => data.Score * data.Weight) ?? throw new Exception("No study plan assigned");
}