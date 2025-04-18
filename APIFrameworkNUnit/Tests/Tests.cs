using RestSharp;
using APIFrameworkNUnit.Helpers;
using APIFrameworkNUnit.Assertions;
using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;

namespace APIFrameworkNUnit.Tests
{
    public class JsonPlaceholderTests
    {
        [Test, Order(1)]
        public void Step01_GETPlaceholder()
        {
            string path = "https://jsonplaceholder.typicode.com/posts";
            var response = RestfulHelpers.ExecuteGetRequest(path);

            Asserts.AssertResponseCode((int)response.StatusCode);

            //ResponseAsserts will make sure response content is not null.
            JArray userInformation = ResponseAsserts.ParseContentAsJArray(response);

            //Now we check the data to validate no nulls are returned in the dataset.
            var nullsInDataset = userInformation
                .OfType<JObject>()
                .Any(u => u.Properties().Any(p => p.Value.Type == JTokenType.Null));
        }
    }
}
