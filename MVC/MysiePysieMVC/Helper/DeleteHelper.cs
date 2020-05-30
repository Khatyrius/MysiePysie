using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MysiePysieMVC.Helper
{
    public class DeleteHelper
    {
        public static bool DeleteEntity(string deleteLink, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Globals.API_LINK);
                client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", Globals.Token);
                var responseTask = client.DeleteAsync(deleteLink + "/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                return result.IsSuccessStatusCode;
            }
        }
    }
}