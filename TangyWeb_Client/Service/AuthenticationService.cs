using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Tangy_Common;
using Tangy_Models.DTO;
using TangyWeb_Client.Service.IService;

namespace TangyWeb_Client.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(HttpClient httpClient, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<SignInResponseDTO> LoginUser(SignInRequestDTO signInRequestDTO)
        {
            var content = JsonConvert.SerializeObject(signInRequestDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/account/signin", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SignInResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                await _localStorageService.SetItemAsync(StaticData.Local_Token, result.Token);
                await _localStorageService.SetItemAsync(StaticData.Local_UserDetails, result.User);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                ((AuthStateProvider)_authenticationStateProvider).NotifyUserLoggedIn(result.Token);

                return new SignInResponseDTO
                {
                    IsAuthSuccessful = true
                };
            }
            else
            {
                return result;
            }
        }

        public async Task LogoutUser()
        {
            await _localStorageService.RemoveItemAsync(StaticData.Local_Token);
            await _localStorageService.RemoveItemAsync(StaticData.Local_UserDetails);
            _httpClient.DefaultRequestHeaders.Authorization = null;

            ((AuthStateProvider)_authenticationStateProvider).NotifyUserLogOut();
        }

        public async Task<SignUpResponseDTO> RegisterUser(SignUpRequestDTO signUpRequestDTO)
        {
            var content = JsonConvert.SerializeObject(signUpRequestDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/account/signup", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SignUpResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return new SignUpResponseDTO
                {
                    IsRegistrationSuccessful = true
                };
            }
            else
            {
                return new SignUpResponseDTO
                {
                    IsRegistrationSuccessful = false,
                    Errors = result.Errors
                };
            }
        }
    }
}
