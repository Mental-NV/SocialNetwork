using SocialNetwork.Profile.Domain.Models;

namespace SocialNetwork.Portal.ApiServices
{
    public interface IProfileApiService
    {
        Task<IEnumerable<ProfileData>> GetProfilesAsync();
    }
}
