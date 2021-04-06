using RealEstate.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(RealEstate.Services.UserService))]
namespace RealEstate.Services
{
    class UserService : IUserService
    {
        public User User { get; }

        public UserService()
        {
            User = new User
            {
                Name = "Margaret Novakowska",
                Description = "Hi! I am Margaret, I really like traveling to really different countries, most often I am looking for flats that have very friendly landlords in a good location.",
                Location = "Los Angeles",
                ProfileImageUrl = "https://www.bravotv.com/sites/bravo/files/styles/cast_head_shot_square_computer/public/2020/11/rhod-season-5-headshot-jennifer-moon.jpg?itok=LYF_XnAl",
                BannerImageUrl = "http://1.bp.blogspot.com/-yTbsTLEwnGA/TicrahG435I/AAAAAAAAB8c/T0X8KLS8Jfg/s1600/800px-Azerbajian_landscape.jpg"
            };
        }
    }
}
