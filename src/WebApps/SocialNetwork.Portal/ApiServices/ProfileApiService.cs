using SocialNetwork.Profile.Domain.Models;
using System.Text.Json;

namespace SocialNetwork.Portal.ApiServices
{
    public class ProfileApiService : IProfileApiService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ProfileApiService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<IEnumerable<ProfileData>> GetProfilesAsync()
        {
            var httpClient = httpClientFactory.CreateClient("ProfileApiClient");
            return await httpClient.GetFromJsonAsync<IEnumerable<ProfileData>>("/profile");
        }
    }
}
