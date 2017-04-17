using System;
using System.Configuration;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json.Linq;


namespace WhitePagesExample
{
    public class IdentityCheck
    {
        public void Check()
        {
            // Build the URI
            UriBuilder uri = new UriBuilder();
            uri.Scheme = "https";
            uri.Host = "proapi.whitepages.com";
            uri.Path = "/3.1/identity_check.json";

            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters.Add("api_key", ConfigurationManager.AppSettings.Get("ID_CHECK_API_KEY"));

            parameters.Add("billing.name", "Drama Number");
            parameters.Add("billing.phone", "6464806649");
            parameters.Add("billing.address.street_line_1", "302 Gorham Ave");
            parameters.Add("billing.address.city", "Ashland");
            parameters.Add("billing.address.state_code", "MT");
            parameters.Add("billing.address.postal_code", "59004");
            parameters.Add("billing.address.country_code", "US");
            parameters.Add("shipping.name", "Drama Number");
            parameters.Add("shipping.phone", "6464806649");
            parameters.Add("shipping.address.street_line_1", "302 Gorham Ave");
            parameters.Add("shipping.address.city", "Ashland");
            parameters.Add("shipping.address.state_code", "MT");
            parameters.Add("shipping.address.postal_code", "59004");
            parameters.Add("shipping.address.country_code", "US");
            parameters.Add("email_address", "medjalloh1@yahoo.com");
            parameters.Add("ip_address", "108.194.128.165");

            uri.Query = parameters.ToString();

            using (var httpClient = new HttpClient())
            {
                string rawJson = null;

                try
                {
                    rawJson = httpClient.GetStringAsync(uri.Uri).Result;
                }
                catch (AggregateException agEx)
                {
                    Console.WriteLine("****     IDENTITY CHECK FAILED   *****");
                    Console.WriteLine(agEx.InnerException.Message);
                    Console.WriteLine("(Check ID_CHECK_API_KEY value in App.config)");
                    Console.WriteLine();
                    return;
                }

                // Parse JSON response
                var jsonMap = JObject.Parse(rawJson);

                var availableChecks = new[] {"billing_name_checks", "billing_phone_checks", "billing_address_checks",
                                              "shipping_name_checks", "shipping_phone_checks", "shipping_address_checks",
                                              "email_address_checks", "ip_address_checks"};
                // Display results
                foreach (var check in availableChecks)
                {
                    Console.WriteLine(jsonMap[check].ToString());
                }
            }
        }
    }
}
