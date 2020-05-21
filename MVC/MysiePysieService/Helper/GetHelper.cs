using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MysiePysieService.Helper
{
    public class GetHelper<T> where T: class
    {
        public static IEnumerable<T> GetAll(string getLink)
        {
            IEnumerable<T> models = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Globals.API_LINK);
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
    }
}