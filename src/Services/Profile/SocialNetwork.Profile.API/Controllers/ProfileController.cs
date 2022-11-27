using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Profile.Domain.Models;

namespace SocialNetwork.Profile.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> logger;

        public ProfileController(ILogger<ProfileController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public IEnumerable<ProfileData> GetProfileData()
        {
            yield return new ProfileData 
            { 
                Id = 1, 
                Login = "Mental", 
                FirstName = "Ruslan", 
                LastName = "Galiev", 
                Birthday = new DateTime(1985, 7, 24), 
                City = "Nizhnevartovsk", 
                Country = "Russian Federation" 
            };
            logger.LogInformation("Requested profile data");
        }
    }
}
