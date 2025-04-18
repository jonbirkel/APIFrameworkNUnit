using System.Security.Cryptography.X509Certificates;
using NUnit.Framework.Internal;
using RestSharp;

namespace APIFrameworkNUnit.Helpers
{
    public class RestfulHelpers
    {

        public static RestResponse ExecuteGetRequest(string path)
        {
            var client = new RestClient(path);
            var request = new RestRequest(path, Method.Get);

            var response = client.Execute(request);

            LogResponse(response);

            return response;
        }

        public static RestResponse ExecutePutRequest(string path, string payload)
        {
            var client = new RestClient(path);
            var request = new RestRequest(path, Method.Put);

            request.AddStringBody(payload, DataFormat.Json);

            var response = client.Execute(request);

            LogResponse(response);

            return response;
        }

        public static RestResponse ExecutePostRequest(string path, string payload)
        {
            var client = new RestClient(path);
            var request = new RestRequest(path, Method.Post);

            request.AddStringBody(payload, DataFormat.Json);

            var response = client.Execute(request);

            LogResponse(response);

            return response;
        }

        public static RestResponse ExecuteDeleteRequest(string path, string payload)
        {
            var client = new RestClient(path);
            var request = new RestRequest(path, Method.Delete);

            request.AddStringBody(payload, DataFormat.Json);

            var response = client.Execute(request);

            LogResponse(response);

            return response;
        }

        public static void LogResponse(RestResponse response)
        {
            TestContext.WriteLine($"Status Code   : {response.StatusCode} ({(int)response.StatusCode})");
            TestContext.WriteLine($"Is Successful : {response.IsSuccessful}");
            TestContext.WriteLine($"Error Message : {response.ErrorMessage ?? "None"}");
            TestContext.WriteLine($"Content       :\n{response.Content}");
        }

    }
}
