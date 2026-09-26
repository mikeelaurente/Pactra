namespace Pactra.Api.Domain.Enums;

public enum AuditAction
{
    Create,
    Update,
    Delete,

    SignAgreement,
    SubmitPayment,
    VerifyPayment,

    RequestCancellation,
    ResolveCancellation,

    SubmitCompletion,
    AcceptCompletion
}