using Pactra.Api.Domain.Enum;

namespace Pactra.Api.Domain.Entities;

public class RequirementSubmission
{
    public long Id { get; set; }

    public long RequirementId { get; set; }
    public long SubmittedBy { get; set; }

    public string FileUrl { get; set; } = string.Empty;
    public string? Notes { get; set; }

    public RequirementSubmissionStatus Status { get; set; }

    public DateTimeOffset SubmittedAt { get; set; }

    public DateTimeOffset? ReviewedAt { get; set; }
    public long? ReviewedBy { get; set; }

    public Requirement Requirement { get; set; } = null!;
    public User Submitter { get; set; } = null!;
    public User? Reviewer { get; set; }
}