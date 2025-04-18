using NUnit.Framework;
using RestSharp;
using FluentAssertions;
using APIFrameworkNUnit.Helpers;

namespace APIFrameworkNUnit.Tests
{
    public class JsonPlaceholderTests
    {
        [Test, Order(1)]
        public void GetPost_ShouldReturnExpectedData()
        {
            var client = new RestClient("https://jsonplaceholder.typicode.com");
            var request = new RestRequest("/posts/1", Method.Get);

            var response = client.Execute(request);

            response.IsSuccessful.Should().BeTrue();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Content.Should().Contain("sunt aut facere");
        }

        [Test, Order(2)]
        public void GetPost2()
        {
            string path = "";
            var response = RestfulHelpers.ExecuteGetRequest(path);
            response.IsSuccessful.Should().BeTrue();
        }
    }
}
