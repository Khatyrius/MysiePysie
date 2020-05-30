using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MysiePysieMVC.Helper
{
    public class GetHelper<T> where T : class
    {
        public static string token { get; set; }
        public static IEnumerable<T> GetAll(string getLink)
        {
            IEnumerable<T> models = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Globals.API_LINK);
                client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", Globals.Token);
                var responseTask = client.GetAsync(getLink);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<T>>();
                    readTask.Wait();

                    models = readTask.Result;
                }
                else
                {
                    models = Enumerable.Empty<T>();
                }
            }
            return models;
        }

        public static T GetById(string getLink, int id)
        {
            T model = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Globals.API_LINK);
                client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", Globals.Token);
                var responseTask = client.GetAsync(getLink + "/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<T>();
                    readTask.Wait();

                    model = readTask.Result;
                }
            }
            return model;
        }

        public static bool Add(string getLink, int listId, int entityId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Globals.API_LINK);
                client.DefaultRequestHeaders.Authorization =
                     new AuthenticationHeaderValue("Bearer", Globals.Token);
                var responseTask = client.GetAsync(getLink + "/" + listId + "/" + entityId);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool Remove(string getLink, int listId, int entityId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Globals.API_LINK);
                client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", Globals.Token);
                var responseTask = client.GetAsync(getLink + "/" + listId + "/" + entityId);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }
    }
}