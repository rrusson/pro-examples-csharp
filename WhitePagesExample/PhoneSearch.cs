using System;
using System.Configuration;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json.Linq;


namespace WhitePagesExample
{
    public class PhoneSearch
    {
        public void Search(string phone)
        {
            // Build the URI                        -----NOTE: This method fails for PhoneSearch.  URI needs to be URI-encoded (not "&", but "&amp;")
            //UriBuilder uri = new UriBuilder();
            //uri.Scheme = "https";
            //uri.Host = "proapi.whitepages.com";
            //uri.Path = "/3.0/phone";

            string apiKey = ConfigurationManager.AppSettings.Get("PHONE_SEARCH_API_KEY");

            //var parameters = HttpUtility.ParseQueryString(string.Empty);
            //parameters.Add("api_key", apiKey);
            //parameters.Add("phone", phone);
            //uri.Query = parameters.ToString();

            string url = ConfigurationManager.AppSettings.Get("WP_API_URL");    //"https://proapi.whitepages.com/3.0/phone?phone={0}&amp;api_key={1}"
            var wpUri = new Uri(String.Format(url, phone, apiKey));

            using(var httpClient = new HttpClient())
            {
                string rawJson = null;

                try
                {
                    //rawJson = httpClient.GetStringAsync(uri.Uri).Result;
                    rawJson = httpClient.GetStringAsync(wpUri).Result;
                }
                catch (AggregateException agEx)
                {
                    Console.WriteLine("****     PHONE SEARCH FAILED   *****");
                    Console.WriteLine(agEx.InnerException.Message);
                    Console.WriteLine("(Check PHONE_SEARCH_API_KEY value in App.config)");
                    Console.WriteLine();
                    return;
                }

                // Parse JSON response
                var jsonMap = JObject.Parse(rawJson);

                // Display results
                Console.WriteLine(jsonMap);
            }
        }
    }
}
