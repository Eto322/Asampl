using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BLL.Integration
{
    public class GitManager
    {
        private string clientId = "Iv1.b2ac3e65b4db782c";
        private string clientSecret = "d5b82b2e4401fd98ec11b8229106de278a4233e6";
        private string redirectUri = "http://localhost:12345/";
        private string scope = "user,repo";

        /* string authUrl = $"https://github.com/login/oauth/authorize?client_id={clientId}&redirect_uri={redirectUri}&scope={scope}&state=random_string";*/

        private void AuthenticateWithGitHubButton()
        {
            string authUrl =
                $"https://github.com/login/oauth/authorize?client_id={clientId}&redirect_uri={redirectUri}&scope={scope}&state=random_string";
            System.Diagnostics.Process.Start(authUrl);
        }

        private async Task<string> ExchangeCodeForAccessToken(string code)
        {
            string clientSecret = "your_client_secret";
            string redirectUri = "http://localhost:12345/";
            string requestUri = "https://github.com/login/oauth/access_token";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var postData = new Dictionary<string, string>
                {
                    { "client_id", clientId },
                    { "client_secret", clientSecret },
                    { "code", code },
                    { "redirect_uri", redirectUri }
                };

                var response = await client.PostAsync(requestUri, new FormUrlEncodedContent(postData));
                var responseString = await response.Content.ReadAsStringAsync();

                // Отримання токена доступу з відповіді
                var tokenResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseString);
                return tokenResponse.ContainsKey("access_token") ? tokenResponse["access_token"] : null;
            }
        }
    }
}