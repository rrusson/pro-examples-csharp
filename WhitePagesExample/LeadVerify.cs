using System;
using System.Configuration;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json.Linq;


namespace WhitePagesExample
{
    public class LeadVerify
    {
        public void Verify()
        {
            // Build the URI
            UriBuilder uri = new UriBuilder();
            uri.Scheme = "https";
            uri.Host = "proapi.whitepages.com";
            uri.Path = "/3.1/lead_verify.json";

            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters.Add("api_key", ConfigurationManager.AppSettings.Get("LEAD_VERIFY_API_KEY"));

            parameters.Add("name", "Drama Number");
            parameters.Add("phone", "6464806649");
            parameters.Add("email_address", "medjalloh1@yahoo.com");
            parameters.Add("address_city", "Ashland");
            parameters.Add("address.postal_code", "59004");
            parameters.Add("address.state_code", "MT");
            parameters.Add("address.street_line_1", "302 Gorham Ave");
            parameters.Add("ip_address", "108.194.128.165");

            uri.Query = parameters.ToString();
            using(var httpClient = new HttpClient())
            {
                string rawJson = null;

                try
                {
                    rawJson = httpClient.GetStringAsync(uri.Uri).Result;
                }
                catch (AggregateException agEx)
                {
                    Console.WriteLine("****     LEAD VERIFY FAILED   *****");
                    Console.WriteLine(agEx.InnerException.Message);
                    Console.WriteLine("(Check LEAD_VERIFY_API_KEY value in App.config)");
                    Console.WriteLine();
                    return;
                }            


                // Parse JSON response
                var jsonMap = JObject.Parse(rawJson);

                var availableChecks = new [] {"name_checks", "phone_checks", "address_checks", "email_address_checks",
                                              "ip_address_checks"};

                // Display phone check results
                foreach (var check in availableChecks) {
                    Console.WriteLine(jsonMap[check].ToString());
                }
            }
        }
    }
}
