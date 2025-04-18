using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SaintMichel.Services
{
    public class User_DAO
    {
        private readonly HttpClient _httpClient;

        public User_DAO()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://saintmichel.alwaysdata.net")
            };
        }

        public async Task<User> GetUserByPseudo(string pseudo)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/getUserByPseudo/{pseudo}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.StatusCode}. Unable to retrieve user data.");
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                {
                    Console.WriteLine("API response is empty.");
                    return null;
                }

                var user = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (user == null)
                {
                    Console.WriteLine("Failed to deserialize user data.");
                }
                else
                {
                    Console.WriteLine($"User found: {user.prenom} {user.nom}");
                }

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                var response = await _httpClient.GetAsync("/getAllUsers");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.StatusCode}. Unable to retrieve users.");
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                {
                    Console.WriteLine("API response is empty.");
                    return null;
                }

                var rootUser = JsonSerializer.Deserialize<List<User>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return rootUser ?? new List<User>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<User> GetUserById(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/getUserById/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.StatusCode}. Unable to retrieve user.");
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                {
                    Console.WriteLine("API response is empty.");
                    return null;
                }

                var user = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<RootUserMessages> CheckUserByPseudo(string pseudo)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/checkUserByPseudo/{pseudo}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.StatusCode}. Unable to check user.");
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                {
                    Console.WriteLine("API response is empty.");
                    return null;
                }

                var message = JsonSerializer.Deserialize<RootUserMessages>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return message;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<RootUserMessages> VerifyUser(string pseudo, string password)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/verifyUser/{pseudo}/{password}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.StatusCode}. Unable to verify user.");
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                {
                    Console.WriteLine("API response is empty.");
                    return null;
                }

                var message = JsonSerializer.Deserialize<RootUserMessages>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return message;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<RootUserMessages> AccountActivation(string email, string code)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/accountActivation?email={Uri.EscapeDataString(email)}&code={Uri.EscapeDataString(code)}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.StatusCode}. Unable to activate account.");
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                {
                    Console.WriteLine("API response is empty.");
                    return null;
                }

                var message = JsonSerializer.Deserialize<RootUserMessages>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return message;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<RootUserMessages> CheckEmail(string email)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/checkEmail?email={email}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.StatusCode}. Unable to check email.");
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                {
                    Console.WriteLine("API response is empty.");
                    return null;
                }

                var message = JsonSerializer.Deserialize<RootUserMessages>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return message;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<User> RegisterUser(User user)
        {
            try
            {
                var json = JsonSerializer.Serialize(user);  // Serialize the user object to JSON
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/registerUser", data);  // Send the data to the PHP API

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.StatusCode}. Unable to register user.");
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                {
                    Console.WriteLine("API response is empty.");
                    return null;
                }

                // Deserialize the response into a User object
                var registeredUser = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return registeredUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }


        public async Task<User> GetUserIdByPseudo(string pseudo)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/GetUserIdByPseudo/{pseudo}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.StatusCode}. Unable to retrieve user ID.");
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                {
                    Console.WriteLine("API response is empty.");
                    return null;
                }

                var user = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<User> GetUserDetails(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/GetUserDetails/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.StatusCode}. Unable to retrieve user details.");
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                {
                    Console.WriteLine("API response is empty.");
                    return null;
                }

                var user = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<User> SetUserDetails(User user)
        {
            try
            {
                var json = JsonSerializer.Serialize(user);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/setUserDetails", data);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {response.StatusCode}. Unable to update user details.");
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                {
                    Console.WriteLine("API response is empty.");
                    return null;
                }

                var detailsUser = JsonSerializer.Deserialize<User>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return detailsUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return null;
            }
        }
    }
}
