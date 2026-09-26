namespace Pactra.Api.Domain.Entities;

public class AgreementSignature
{
    public long Id { get; set; }
    public long AgreementId { get; set; }
    public long SignerId { get; set; }
    public string SignerRole { get; set; } = string.Empty;
    public DateTimeOffset SignedAt { get; set; }
    public Agreement Agreement { get; set; } = null!;
    public User Signer { get; set; } = null!;

}