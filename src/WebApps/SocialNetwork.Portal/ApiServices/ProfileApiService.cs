using SocialNetwork.Profile.Domain.Models;
using System.Text.Json;

namespace SocialNetwork.Portal.ApiServices
{
    public class ProfileApiService : IProfileApiService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly Uri profileApiBaseUrl;

        public ProfileApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            var profileApiBaseUrlString = configuration["ApiServices:ProfileApiBaseUrl"];

            if (string.IsNullOrEmpty(profileApiBaseUrlString))
            {
                throw new ApplicationException($"{nameof(profileApiBaseUrl)} cannot be null");
            }

            profileApiBaseUrl = new Uri(profileApiBaseUrlString);
        }

        public async Task<IEnumerable<ProfileData>> GetProfilesAsync()
        {
            using (var httpClient = httpClientFactory.CreateClient())
            {
                httpClient.BaseAddress = profileApiBaseUrl;
                HttpResponseMessage response = await httpClient.GetAsync("/profile", HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                return await JsonSerializer.DeserializeAsync<IEnumerable<ProfileData>>(await response.Content.ReadAsStreamAsync());
            }
        }
    }
}
