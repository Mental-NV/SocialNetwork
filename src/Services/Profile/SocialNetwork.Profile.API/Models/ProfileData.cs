namespace SocialNetwork.Profile.API.Models
{
    public class ProfileData
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int Id { get; internal set; }
    }
}
