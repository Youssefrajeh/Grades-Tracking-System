/**
 Class Name: Program
 Purpose: Main entry point for the Grades Tracking System application.
           Handles User interface interactions to manage courses and evaluations,
           calculates grade summaries, and ensures persistent JSON storage.
  Coder: Youssef Rajeh-1196323
         Abdul Marouf - 1144451
  
  Date: June, 2025
 */

using GradesTrackingSystem.Models;
using GradesTrackingSystem.Utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GradesTrackingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            JsonHandler.EnsureFileExistsOrExit();
            List<Course> courses = JsonHandler.LoadCourses();
      
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n                       ~ GRADES TRACKING SYSTEM ~\n");
                Console.WriteLine("+-------------------------------------------------------------------------+");
                Console.WriteLine("|                          Grades Summary                                 |");
                Console.WriteLine("+-------------------------------------------------------------------------+");

                if (courses.Count == 0)
                {
                    Console.WriteLine("\nThere are currently no saved courses.");
                }
                else
                {
                    Console.WriteLine("\n#. Course\t  Marks Earned\t   Out Of\tPercent\n");
                    for (int i = 0; i < courses.Count; i++)
                    {
                        var c = courses[i];
                        double earned = 0, weight = 0;
                        foreach (var e in c.Evaluations)
                        {
                            if (e.EarnedMarks != null)
                            {
                                double percentEval = (double)e.EarnedMarks / e.OutOf * 100;
                                earned += percentEval * e.Weight / 100;
                                weight += e.Weight;
                            }
                        }
                        double percent = weight > 0 ? 100 * earned / weight : 0;
                        Console.WriteLine($"{i + 1}. {c.Code}\t\t  {earned:F1}\t     {weight:F1}\t   {percent:F1}");
                    }
                }

                Console.WriteLine("\n---------------------------------------------------------------------------");
                Console.WriteLine("Press # from the above list to view/edit/delete a specific course.");
                Console.WriteLine("Press A to add a new course.");
                Console.WriteLine("Press X to quit.");
                Console.WriteLine("---------------------------------------------------------------------------");
                Console.Write("Enter a command: ");

                string choice = (Console.ReadLine() ?? "").Trim().ToUpper();

                if (choice == "X") break;

                if (choice == "A")
                {
                    while (true)
                    {
                        Console.Write("Enter a course code: ");
                        string code = (Console.ReadLine() ?? "").Trim(); 

                        var newCourse = new Course { Code = code };

                        if (SchemaValidator.IsValidCourse(newCourse, out var errors))
                        {
                            courses.Add(newCourse);
                            Console.WriteLine("Course added.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Invalid course code.");
                        }
                    }

                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                }
                else if (int.TryParse(choice, out int index) && index >= 1 && index <= courses.Count)
                {
                    Course selected = courses[index - 1];
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("\n                       ~ GRADES TRACKING SYSTEM ~\n");
                        Console.WriteLine("+-------------------------------------------------------------------------+");
                        Console.WriteLine($"|                        {selected.Code} Evaluations                            |"); 
                        Console.WriteLine("+-------------------------------------------------------------------------+");

                        if (selected.Evaluations.Count == 0)
                        {
                            Console.WriteLine($"\nThere are currently no evaluations for {selected.Code}.\n");
                        }
                        else
                        {
                            Console.WriteLine("#.\nEvaluation\tMarks Earned\tOut Of\tPercent\t  Course Marks\t Weight/100\n");
                            for (int i = 0; i < selected.Evaluations.Count; i++)
                            {
                                var e = selected.Evaluations[i];
                                string earnedStr = e.EarnedMarks?.ToString("F1") ?? "N/A";
                                string percentStr = "N/A";
                                string courseMarkStr = "0.0";

                                if (e.EarnedMarks != null && e.OutOf > 0)
                                {
                                    double percentEval = (double)e.EarnedMarks / e.OutOf * 100;
                                    double courseMarks = percentEval * e.Weight / 100;
                                    percentStr = percentEval.ToString("F1");
                                    courseMarkStr = courseMarks.ToString("F1");
                                }

                                Console.WriteLine($"{i + 1}. {e.Description ?? "Untitled"}\t\t{earnedStr}\t    {e.OutOf}\t   {percentStr}\t\t  {courseMarkStr}\t\t{e.Weight}\n");
                            }
                        }

                        Console.WriteLine("---------------------------------------------------------------------------");
                        Console.WriteLine("Press D to delete this course.");
                        Console.WriteLine("Press A to add a new evaluation.");
                        Console.WriteLine("Press # from the above list to edit/delete a specific evaluation.");
                        Console.WriteLine("Press X to return to the main menu.");
                        Console.WriteLine("\n---------------------------------------------------------------------------");
                        Console.Write("Enter command: ");
                        string evalChoice = (Console.ReadLine() ?? "").Trim().ToUpper();

                        if (evalChoice == "X") break;

                        if (evalChoice == "D")
                        {
                            Console.Write($"Delete {selected.Code}? (y/n): ");
                            if ((Console.ReadLine() ?? "").ToLower() == "y")
                            {
                                courses.RemoveAt(index - 1);
                                break;
                            }
                        }
                        else if (evalChoice == "A")
                        {
                            while (true)
                            {
                                var eval = new Evaluation();

                                Console.Write("Enter a description: ");
                                eval.Description = Console.ReadLine() ?? "Untitled";

                                Console.Write("Enter the 'out of' mark: ");
                                int.TryParse(Console.ReadLine(), out int outOf);
                                eval.OutOf = outOf;

                                Console.Write("Enter the % weight: ");
                                double.TryParse(Console.ReadLine(), out double weight);
                                eval.Weight = weight;

                                Console.Write("Enter marks earned or press ENTER to skip: ");
                                string earnedInput = Console.ReadLine() ?? "";
                                eval.EarnedMarks = string.IsNullOrWhiteSpace(earnedInput)
                                    ? null
                                    : (double.TryParse(earnedInput, out double earned) && earned >= 0 ? earned : null);

                                if (SchemaValidator.IsValidEvaluation(eval, out var evalErrors))
                                {
                                    selected.Evaluations.Add(eval);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("ERROR: Invalid evaluation.");

                                }
                            }
                        }
                        else if (int.TryParse(evalChoice, out int evalIndex) &&
                                 evalIndex >= 1 && evalIndex <= selected.Evaluations.Count)
                        {
                            var selectedEval = selected.Evaluations[evalIndex - 1];
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("+-------------------------------------------------------------------------+");
                                Console.WriteLine($"|                          {selected.Code} {selectedEval.Description}                            |");
                                Console.WriteLine("+-------------------------------------------------------------------------+");

                                string earnedStr = selectedEval.EarnedMarks?.ToString("F1") ?? "N/A";
                                string percentStr = "N/A";
                                string courseMarkStr = "0.0";

                                if (selectedEval.EarnedMarks != null && selectedEval.OutOf > 0)
                                {
                                    double percentEval = (double)selectedEval.EarnedMarks / selectedEval.OutOf * 100;
                                    double courseMarks = percentEval * selectedEval.Weight / 100;
                                    percentStr = percentEval.ToString("F1");
                                    courseMarkStr = courseMarks.ToString("F1");
                                }

                                Console.WriteLine($"\nMarks Earned\tOut Of\tPercent\tCourse Marks\tWeight/100");
                                Console.WriteLine($"{earnedStr}\t\t{selectedEval.OutOf}\t{percentStr}\t{courseMarkStr}\t\t{selectedEval.Weight}");

                                Console.WriteLine("\n---------------------------------------------------------------------------");
                                Console.WriteLine("Press D to delete this evaluation.");
                                Console.WriteLine("Press E to edit this evaluation.");
                                Console.WriteLine("Press X to return to the previous menu.");
                                Console.WriteLine("\n---------------------------------------------------------------------------");
                                Console.Write("Enter a command: ");

                                string subChoice = (Console.ReadLine() ?? "").Trim().ToUpper();

                                if (subChoice == "X") break;

                                if (subChoice == "D")
                                {
                                    Console.Write($"Delete {selectedEval.Description}? (y/n): ");
                                    if ((Console.ReadLine() ?? "").ToLower() == "y")
                                    {
                                        selected.Evaluations.RemoveAt(evalIndex - 1);
                                        break;
                                    }
                                }
                                else if (subChoice == "E")
                                {
                                    while (true)
                                    {
                                        Console.Write($"Enter marks earned out of {selectedEval.OutOf}, press ENTER to leave unassigned: ");
                                        string input = Console.ReadLine() ?? "";

                                        double? newMarks = null;

                                        if (!string.IsNullOrWhiteSpace(input))
                                        {
                                            if (double.TryParse(input, out double parsed))
                                                newMarks = parsed;
                                        }

                                        var testEval = new Evaluation
                                        {
                                            Description = selectedEval.Description,
                                            Weight = selectedEval.Weight,
                                            OutOf = selectedEval.OutOf,
                                            EarnedMarks = newMarks
                                        };

                                        if (SchemaValidator.IsValidEvaluation(testEval, out var errors))
                                        {
                                            selectedEval.EarnedMarks = newMarks;
                                            Console.WriteLine("Marks updated.");
                                            Console.WriteLine("Press ENTER to return to the evaluation menu...");
                                            Console.ReadLine();
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("ERROR: Invalid 'marks earned' value.");
                                            Console.Write($"Enter marks earned out of {selectedEval.OutOf}, press E: ");
                                            string retry = Console.ReadLine() ?? "";

                                            if (retry.ToUpper() != "E")
                                                break; // Exit if not E
                                        }
                                    }
                                }







                            }

                        }
                    }
                }
            }

            JsonHandler.SaveCourses(courses);
            Console.WriteLine("Changes saved. Goodbye!");
        }
    }
}
