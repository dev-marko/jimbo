using Forum.Domain.Models;
using Forum.Domain.ViewModels;
using Forum.Services.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Forum.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;

        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<User> FetchCurrentUser(string token)
        {
            string API_URL = "me";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync(API_URL);
            var jsonString = await response.Content.ReadAsStringAsync();
            User user = JsonConvert.DeserializeObject<User>(jsonString);
            return user;
        }

        public async Task<User> FetchUserByUsername(string username)
        {
            string API_URL = username;
            var response = await httpClient.GetAsync(API_URL);
            var jsonString = await response.Content.ReadAsStringAsync();
            User user = JsonConvert.DeserializeObject<User>(jsonString);
            return user;
        }

        public UserViewModel FetchUserViewModel(string username)
        {
            var user = FetchUserByUsername(username).Result;

            return new UserViewModel
            {
                Email = user.Email,
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}
