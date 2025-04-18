using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace APIFrameworkNUnit.Helpers
{
    internal class ResponseAsserts
    {
        public static JArray ParseContentAsJArray(RestResponse response)
        {
            if (string.IsNullOrWhiteSpace(response.Content))
            {
                Assert.Fail("API Response was null or empty.  There was nothing to parse.");
            }

            try
            {
                JArray parsedResponse = JArray.Parse(response.Content!);
                return parsedResponse;
            }
            catch (JsonReaderException e)
            {
                Assert.Fail($"Failed to parse response content as JArray. Error: {e.Message}");
                throw;
            }
        }

        public static JObject ParseContentAsJObject(RestResponse response)
        {
            if (string.IsNullOrWhiteSpace(response.Content))
            {
                Assert.Fail("API Response was null or empty.  There was nothing to parse.");
            }

            try
            {
                JObject parsedResponse = JObject.Parse(response.Content!);
                return parsedResponse;
            }
            catch (JsonReaderException e)
            {
                Assert.Fail($"Failed to parse response content as JObject. Error: {e.Message}");
                throw;
            }
        }
    }
}
