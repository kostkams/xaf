using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace XAF.RestClient
{
    public class RestClient : IRestClient
    {
        private HttpClient client;

        public bool IsLoggedIn { get; private set; }

        public void Login(Uri uri, object credentials)
        {
            if (IsLoggedIn && client?.DefaultRequestHeaders.Authorization != null)
                return;

            Setup();

            AsyncHelpers.RunSync(async () =>
            {
                var json = JsonConvert.SerializeObject(credentials);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());

                var byteArray = Encoding.ASCII.GetBytes(credentials.ToString());
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                IsLoggedIn = true;
            });
        }

        public object Get(Uri uri)
        {
            if (IsLoggedIn && client?.DefaultRequestHeaders.Authorization == null)
                return null;

            object result = null;
            AsyncHelpers.RunSync(async () =>
            {
                var response = await client.GetAsync(uri);
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException(await response.Content.ReadAsStringAsync());

                result = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
            });

            return result;
        }

        public object Post(Uri uri, object content)
        {
            throw new NotImplementedException();
        }

        public object Patch(Uri uri, object content)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Uri uri)
        {
            throw new NotImplementedException();
        }

        public bool Logout(Uri uri)
        {
            if (client == null)
                return true;

            client.Dispose();
            client = null;
            
            IsLoggedIn = false;

            return true;
        }

        private void Setup()
        {
            client = new HttpClient {MaxResponseContentBufferSize = 256000};
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}