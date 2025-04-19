using RestSharp;
using APIFrameworkNUnit.Helpers;
using APIFrameworkNUnit.Assertions;
using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using System.Security.Cryptography.X509Certificates;
using APIFrameworkNUnit.Tests.JsonPlaceholderTests.JsonPlaceholderModels;

namespace APIFrameworkNUnit.Tests.JsonPlaceholderTests
{
    public class JsonPlaceholderTests
    {
        private JObject userToUpdate = new JObject();
        private JObject createdUser = new JObject();

        [Test, Order(1)]
        public void Step01_GETPlaceholder()
        {
            string path = "https://jsonplaceholder.typicode.com/posts";
            var response = RestfulHelpers.ExecuteGetRequest(path);

            Asserts.AssertResponseCode((int)response.StatusCode);

            //ResponseAsserts will make sure response content is not null.
            JArray userInformation = ResponseAsserts.ParseContentAsJArray(response);

            //Make sure each object has the correct structure.
            foreach (JObject userInformationObj in userInformation)
            {
                Assert.That(userInformationObj.ContainsKey("userId"), Is.True, "The JSON object does not containt the userId field.");
                Assert.That(userInformationObj.ContainsKey("id"), Is.True, "The JSON object does not containt the id field.");
                Assert.That(userInformationObj.ContainsKey("title"), Is.True, "The JSON object does not containt the title field.");
                Assert.That(userInformationObj.ContainsKey("body"), Is.True, "The JSON object does not containt the body field.");

                Assert.That(userInformationObj.Count, Is.EqualTo(4), "The JSON object contains more than 4 properties.");
            }

            //Now we check the data to validate no nulls are returned in the dataset.
            var nullsInDataset = userInformation
                .OfType<JObject>()
                .Where(u => u.Properties().Any(p => p.Value.Type == JTokenType.Null))
                .ToList();

            if (nullsInDataset.Any())
            {
                foreach (var item in userInformation)
                {
                    TestContext.WriteLine($"Nulls found in object: {item.ToString()}");
                }

                Assert.Fail($"Nulls found in dataset.  See Above.");
            }

            //Now we confirm the counts of userIds
            Dictionary<int, int> userIdCounts = new Dictionary<int, int>();

            foreach (var item in userInformation)
            {
                int userId = (int)item["userId"]!;
                if (userIdCounts.ContainsKey(userId))
                {
                    userIdCounts[userId]++;
                }
                else
                {
                    userIdCounts.Add(userId, 1);
                }
            }

            foreach (var kvp in userIdCounts)
            {
                if (kvp.Value != 10)
                {
                    Assert.Fail($"User ID {kvp.Key.ToString()} only has {kvp.Value.ToString()} entries.  10 were expected.");
                }
            }

            userToUpdate = userInformation.OfType<JObject>()
                .FirstOrDefault(x => x["id"]?.Value<int>() == 4)!;
        }

        [Test, Order(2)]
        public void Step02_PUTPlaceholder()
        {
            string id = userToUpdate["userId"]!.ToString();
            userToUpdate["title"] = "Mr. Manager";
            string payload = userToUpdate.ToString();
            string path = $"https://jsonplaceholder.typicode.com/posts/{id}";
            var response = RestfulHelpers.ExecutePutRequest(path, payload);

            Asserts.AssertResponseCode((int)response.StatusCode);

            JObject updatedRecord = ResponseAsserts.ParseContentAsJObject(response);

            Assert.IsNotNull(updatedRecord);
            Assert.That(updatedRecord["title"]!.ToString(), Is.EqualTo("Mr. Manager"), "The record was not updated properly.");

            //Typically i would pull back the data via a GET to https:/jsonplaceholder.typicode.com/posts/{id} and validate the title was changed.
            //However, this endpoint does not actually update the record, it simply mocks the response.
        }

        [Test, Order(3)]
        public void Step03_POSTPlaceholder()
        {
            JObject newUser = JsonObjects.newUserObject();
            newUser["userId"] = 5;
            newUser["title"] = "Toast";
            newUser["body"] = "Butter, Jelly, Peanut Butter, Nutella";

            string payload = newUser.ToString();
            string path = "https://jsonplaceholder.typicode.com/posts";
            var response = RestfulHelpers.ExecutePostRequest(path, payload);

            Asserts.AssertResponseCode((int)response.StatusCode);

            createdUser = ResponseAsserts.ParseContentAsJObject(response);

            Assert.IsNotNull(createdUser);
            Assert.That(createdUser["userId"]!.Value<int>, Is.EqualTo(5), "The userId was not set properly on user creation.");
            Assert.That(createdUser["title"]!.ToString(), Is.EqualTo("Toast"), "The title was not set properly on user creation.");
            Assert.That(createdUser["body"]!.ToString(), Is.EqualTo("Butter, Jelly, Peanut Butter, Nutella"), "The body was not set properly on user creation.");
        }

        [Test, Order(4)]
        public void Step04_DELETEPlaceholder()
        {
            string id = createdUser["id"]!.ToString();
            string path = $"https://jsonplaceholder.typicode.com/posts/{id}";
            var response = RestfulHelpers.ExecuteDeleteRequest(path);
            Asserts.AssertResponseCode((int)response.StatusCode);
        }
    }
}
