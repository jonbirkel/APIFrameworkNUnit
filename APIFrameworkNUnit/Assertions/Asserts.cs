using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace APIFrameworkNUnit.Assertions
{
    public class Asserts
    {
        public static void AssertResponseCode(int responseCode)
        {
            var successfulCodes = new[] { 200, 201, 202, 204 };

            if (!successfulCodes.Contains(responseCode))
            {
                Assert.Fail($"Assertion Failed.  Response Code received was {responseCode}");
            }
            else
            { 
                Assert.Pass(); 
            }
        }
    }
}
