namespace Pactra.Api.Application.Interfaces;

public interface IAgreementService
{
    public Task SignAgreementAsync(long agreementId, long userId);
}