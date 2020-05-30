using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MysiePysieMVC.Helper
{
    public class UpdateHelper<T> where T : class
    {
        public static bool UpdateEntity(string updateLink, T entity)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Globals.API_LINK);
                client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", Globals.Token);
                var responseTask = client.PutAsJsonAsync(updateLink, entity);
                responseTask.Wait();

                var result = responseTask.Result;
                return result.IsSuccessStatusCode;
            }
        }
    }
}