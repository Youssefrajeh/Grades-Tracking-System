/**
  Class Name: SchemaValidator
  Purpose: Validates Course and Evaluation objects against a JSON schema definition.
  Coder: Youssef Rajeh-1196323
         Abdul Marouf - 1144451
  Date:June, 2025
 */

using GradesTrackingSystem.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Collections.Generic;
using System.IO;

namespace GradesTrackingSystem.Utils
{
    public static class SchemaValidator
    {
        private static readonly string schemaPath = "Data/schema.json";

        public static bool IsValidCourse(Course course, out IList<string> errorMessages)
        {
            errorMessages = new List<string>();

            if (!File.Exists(schemaPath))
            {
                errorMessages.Add("Schema file not found.");
                return false;
            }

            string schemaJson = File.ReadAllText(schemaPath);
            JSchema schema = JSchema.Parse(schemaJson);

            // Serialize Course to JSON then parse as JObject
            string json = JsonConvert.SerializeObject(course);
            JObject courseObj = JObject.Parse(json);

            bool isValid = courseObj.IsValid(schema, out errorMessages);
            return isValid;
        }

        public static bool IsValidEvaluation(Evaluation eval, out IList<string> errorMessages)
        {
            errorMessages = new List<string>();

            if (!File.Exists(schemaPath))
            {
                errorMessages.Add("Schema file not found.");
                return false;
            }

            string schemaJson = File.ReadAllText(schemaPath);
            JSchema schema = JSchema.Parse(schemaJson);

            // Wrap evaluation inside a dummy course object for schema validation
            var dummyCourse = new
            {
                Code = "TEST-0000",
                Evaluations = new[] { eval }
            };

            string json = JsonConvert.SerializeObject(dummyCourse);
            JObject courseObj = JObject.Parse(json);

            return courseObj.IsValid(schema, out errorMessages);
        }

    }
}
