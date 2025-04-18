using NUnit.Framework;
using RestSharp;
using FluentAssertions;
using System.Security.Cryptography.X509Certificates;

namespace APIFrameworkNUnit.Helpers
{
    public class RestfulHelpers
    {

        public static RestResponse ExecuteGetRequest(string path)
        {
            var client = new RestClient(path);
            var request = new RestRequest(path, Method.Get);

            var response = client.Execute(request);

            return response;
        }

        public static RestResponse ExecutePutRequest(string path, string payload)
        {
            var client = new RestClient(path);
            var request = new RestRequest(path, Method.Put);

            request.AddStringBody(payload, DataFormat.Json);

            var response = client.Execute(request);
            return response;
        }

        public static RestResponse ExecutePostRequest(string path, string payload)
        {
            var client = new RestClient(path);
            var request = new RestRequest(path, Method.Post);

            request.AddStringBody(payload, DataFormat.Json);

            var response = client.Execute(request);
            return response;
        }

        public static RestResponse ExecuteDeleteRequest(string path, string payload)
        {
            var client = new RestClient(path);
            var request = new RestRequest(path, Method.Delete);

            request.AddStringBody(payload, DataFormat.Json);

            var response = client.Execute(request);
            return response;
        }
    }
}
