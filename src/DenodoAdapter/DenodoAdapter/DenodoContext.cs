using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Web.Http;

namespace DenodoAdapter
{
    public class DenodoContext : IDenodoContext
    {
        const string MEDIA_TYPE = "application/json";
        readonly string _denodoBaseUri;
        readonly string _username;
        readonly string _password;
        public DenodoContext(string denodoBaseUri, string username = null, string password = null)
        {
            _denodoBaseUri = denodoBaseUri;
            _username = username;
            _password = password;
        }

        private HttpClient CreateClient()
        {
            HttpClient httpClient;
            if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_password))
                httpClient = new HttpClient();
            else
            {
                NetworkCredential credentials = new NetworkCredential(_username, _password);
                var handler = new HttpClientHandler() { Credentials = credentials };
                httpClient = new HttpClient(handler);
            }
            httpClient.BaseAddress = new Uri(_denodoBaseUri);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MEDIA_TYPE));
            return httpClient;
        }

        public List<T> SearchData<T>(string viewUri, string filter) where T : class
        {
            using (HttpClient httClient = CreateClient())
            {
                string uri = viewUri + "?$filter=" + filter;
                HttpResponseMessage responseMessage = httClient.GetAsync(uri).Result;
                if (!responseMessage.IsSuccessStatusCode)
                    throw new HttpResponseException(responseMessage); 

                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(DenodoResponse<List<T>>));
                var denodoResponse = (DenodoResponse<List<T>>)deserializer.ReadObject(responseMessage.Content.ReadAsStreamAsync().Result);
                return denodoResponse.elements;
            }
        }
        public List<T> GetData<T>(string viewUri) where T : class
        {
            using (HttpClient httClient = CreateClient())
            {
                HttpResponseMessage responseMessage = httClient.GetAsync(viewUri).Result;
                if (!responseMessage.IsSuccessStatusCode)
                    throw new HttpResponseException(responseMessage); 

                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(DenodoResponse<List<T>>));
                var denodoResponse = (DenodoResponse<List<T>>)deserializer.ReadObject(responseMessage.Content.ReadAsStreamAsync().Result);
                return denodoResponse.elements;
            }
        }

        public T GetData<T>(string viewUri, string id) where T : class
        {
            using (HttpClient httClient = CreateClient())
            {
                HttpResponseMessage responseMessage = httClient.GetAsync(viewUri + "/" + id).Result;
                if (!responseMessage.IsSuccessStatusCode)
                    throw new HttpResponseException(responseMessage);

                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(T));
                var result =
                    (T) deserializer.ReadObject(responseMessage.Content.ReadAsStreamAsync().Result);
                return result;
            }
        }

        public bool Insert<T>(string viewUri, T t) where T : class
        {
            using (HttpClient httpClient = CreateClient())
            {
                HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync(viewUri, t).Result;
                if (!responseMessage.IsSuccessStatusCode)
                    throw new HttpResponseException(responseMessage);
            }
            return true;
        }

        public bool Delete(string viewUri, string id)
        {
            using (HttpClient httpClient = CreateClient())
            {
                HttpResponseMessage responseMessage = httpClient.DeleteAsync(viewUri + "/" + id).Result;
                if (!responseMessage.IsSuccessStatusCode)
                    throw new HttpResponseException(responseMessage);
            }
            return true;
        }

        public bool Update<T>(string viewUri, string id, T t) where T : class
        {
            using (HttpClient httpClient = CreateClient())
            {
                HttpResponseMessage responseMessage = httpClient.PutAsJsonAsync(viewUri + "/" + id, t).Result;
                if (!responseMessage.IsSuccessStatusCode)
                    throw new HttpResponseException(responseMessage);
            }
            return true;
        }
    }
}
