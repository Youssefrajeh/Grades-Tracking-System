/**
  Class Name:JsonHandler
  Purpose: Provides utility methods for loading and saving course data to/from a JSON file.
  Coder: Youssef Rajeh-1196323
         Abdul Marouf - 1144451
  Date: June, 2025
 */

using GradesTrackingSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace GradesTrackingSystem.Utils
{
    public static class JsonHandler
    {
        //  This points to your actual project folder's Data directory
        private static readonly string folderPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data");

        private static readonly string filePath = Path.Combine(folderPath, "grades.json");

        // Call this first in Program.cs
        public static void EnsureFileExistsOrExit()
        {
            if (!File.Exists(filePath))
            {
                Console.Write("Grades data file grades.json not found. Create new file (y/n): ");
                string input = (Console.ReadLine() ?? "").Trim().ToLower();

                if (input == "y")
                {
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    File.WriteAllText(filePath, "[]");
                    Console.WriteLine("\nNew data set created. Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Exiting program...");
                    Environment.Exit(0);
                }
            }
        }

        public static List<Course> LoadCourses()
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Course>>(json) ?? new List<Course>();
        }

        public static void SaveCourses(List<Course> courses)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string json = JsonConvert.SerializeObject(courses, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
    }
}



