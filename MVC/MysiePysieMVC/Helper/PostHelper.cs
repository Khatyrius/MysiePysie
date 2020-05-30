using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MysiePysieMVC.Helper
{
    public class PostHelper<T> where T : class
    {
        public static bool PostEntity(string postLink, T entity)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Globals.API_LINK);
                client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", Globals.Token);
                var responseTask = client.PostAsJsonAsync(postLink, entity);
                responseTask.Wait();

                var result = responseTask.Result;
                return result.IsSuccessStatusCode;
            }
        }

        public static string GetToken(string postLink, T entity)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Globals.API_LINK);

                var responseTask = client.PostAsJsonAsync(postLink, entity);
                responseTask.Wait();

                var result = responseTask.Result.Content.ReadAsStringAsync().Result.ToString();
                return result.ToString();
            }
        }
    }
}