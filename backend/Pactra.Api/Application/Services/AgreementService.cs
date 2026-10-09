using Microsoft.EntityFrameworkCore;
using Pactra.Api.Application.Interfaces;
using Pactra.Api.Domain.Entities.Engagement;
using Pactra.Api.Domain.Enums;
using Pactra.Api.Infrastructure.Persistence;

public class AgreementService(PactraDbContext _db) : IAgreementService
{
    private readonly PactraDbContext _db = _db;
    public async Task SignAgreementAsync(long agreementId, long userId)
    {
        var agreement = await _db.Agreements
        .Include(a => a.Engagement)
        .Include(a => a.Signatures)
        .FirstOrDefaultAsync(a => a.Id == agreementId) 
        ?? throw new KeyNotFoundException("Agreement not found.");

        var engagement = agreement.Engagement;
        if (userId != engagement.ClientId && userId != engagement.ProviderId)
        {
            throw new InvalidOperationException($"User with ID {userId} is not part of this engagement.");
        }

        if (agreement.Status != AgreementStatus.PendingSignatures)
        {
            throw new InvalidOperationException("Agreement is not awaiting signatures.");
        }

        var signerRole = userId == engagement.ClientId
            ? AgreementSignerRole.Client
            : AgreementSignerRole.Provider;

        if (agreement.Signatures.Any(s => s.SignerRole == signerRole))
        {
            throw new InvalidOperationException("User has already signed the agreement.");
        }

        var signature = new AgreementSignature
        {
            AgreementId = agreement.Id,
            SignerId = userId,
            SignerRole = signerRole,
            SignedAt = DateTime.UtcNow
        };

        agreement.Signatures.Add(signature);

        var hasClientSigned = agreement.Signatures
            .Any(s => s.SignerRole == AgreementSignerRole.Client);

        var hasProviderSigned = agreement.Signatures
            .Any(s => s.SignerRole == AgreementSignerRole.Provider);

        if (hasClientSigned && hasProviderSigned)
        {
            agreement.Status = AgreementStatus.Signed;
        }

        await _db.SaveChangesAsync();
    }
}