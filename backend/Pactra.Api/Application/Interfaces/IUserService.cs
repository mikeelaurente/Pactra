namespace Pactra.Api.Application.Interfaces;

public interface IUserService
{
    Task<bool> BecomeProviderAsync(long userID);
}