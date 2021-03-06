﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using Models;

namespace Denodo
{
    public class DenodoContext: IDenodoContext
    {
        const string MEDIA_TYPE = "application/json";
        readonly string _denodoBaseUri;
        readonly string _username;
        readonly string _password;
        public DenodoContext(string denodoBaseUri, string username=null, string password=null)
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

        private void ThrowException(HttpResponseMessage responseMessage)
        {
            DataContractJsonSerializer errorDeserializer = new DataContractJsonSerializer(typeof(DenodoError));
            var denodoError = (DenodoError)errorDeserializer.ReadObject(responseMessage.Content.ReadAsStreamAsync().Result);
            throw new Exception(denodoError.ReadAll());
        }
        public List<T> GetData<T>(string viewUri) where T:class 
        {
            using (HttpClient httClient = CreateClient())
            {
                 HttpResponseMessage responseMessage = httClient.GetAsync(viewUri).Result;
                if(!responseMessage.IsSuccessStatusCode)
                    ThrowException(responseMessage);

                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(DenodoResponse<List<T>>));
                var denodoResponse = (DenodoResponse<List<T>>)deserializer.ReadObject(responseMessage.Content.ReadAsStreamAsync().Result);
                return denodoResponse.elements;
            }
        }

        public T GetData<T>(string viewUri, string id) where T:class 
        {
            using (HttpClient httClient = CreateClient())
            {
                HttpResponseMessage responseMessage = httClient.GetAsync(viewUri + "/" + id).Result;
                if (!responseMessage.IsSuccessStatusCode)
                    ThrowException(responseMessage);

                DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(DenodoResponse<T>));
                var denodoResponse =
                    (DenodoResponse<T>) deserializer.ReadObject(responseMessage.Content.ReadAsStreamAsync().Result);
                return denodoResponse.elements;
            }
        }

        public bool Insert<T>(string viewUri, T t) where T:class
        {
            using (HttpClient httpClient= CreateClient())
            {
                HttpResponseMessage responseMessage = httpClient.PostAsJsonAsync(viewUri, t).Result;
                if(!responseMessage.IsSuccessStatusCode)
                ThrowException(responseMessage);
            }
            return true;
        }

        public bool Delete(string viewUri, string id)
        {
            using (HttpClient httpClient = CreateClient())
            {
                HttpResponseMessage responseMessage = httpClient.DeleteAsync(viewUri + "/" + id).Result;
                if (!responseMessage.IsSuccessStatusCode)
                    ThrowException(responseMessage);
            }
            return true;
        }

        public bool Update<T>(string viewUri, string id, T t) where T : class
        {
            using (HttpClient httpClient = CreateClient())
            {
                HttpResponseMessage responseMessage = httpClient.PutAsJsonAsync(viewUri + "/" + id, t).Result;
                if (!responseMessage.IsSuccessStatusCode)
                    ThrowException(responseMessage);
            }
            return true;
        }
    }
}
