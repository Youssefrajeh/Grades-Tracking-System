/**
  Class Name: Course
  Purpose: Represents a course with its code and related evaluations.
  Coder: Youssef Rajeh-1196323
         Abdul Marouf - 1144451
  Date:June, 2025
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradesTrackingSystem.Models
{
    public class Course
    {
        public string? Code { get; set; }
        public List<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
    }
}
