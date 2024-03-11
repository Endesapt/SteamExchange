using Server.ResponseModels;

namespace Server.Services.Interfaces
{
    public interface IUserService
    {
        Task EnsureUserCreated(long userId);
        Task<bool> UpdateInventory(long userId);

        ProfileResponseModel? GetProfile(long userId);
    }
}
