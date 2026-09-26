namespace Pactra.Api.Domain.Enum;

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