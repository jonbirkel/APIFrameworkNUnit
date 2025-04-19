using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace APIFrameworkNUnit.Tests.JsonPlaceholderTests.JsonPlaceholderModels
{
    public class JsonObjects
    {
        public static JObject newUserObject()
        {
            JObject newUser = new JObject
            {
                {"id", null},
                {"userId", null},
                {"title", null},
                {"body", null}
            };

            return newUser;
        }
    }
}
