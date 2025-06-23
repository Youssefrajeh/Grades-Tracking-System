/**
 Class Name: Evaluation
 Purpose: Represents an evaluation component with a description, weight, total marks, and earned marks.
 Coder: Youssef Rajeh-1196323
        Abdul Marouf - 1144451
 Date: June, 2025
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradesTrackingSystem.Models
{
    public class Evaluation
    {
        public string? Description { get; set; }
        public double Weight { get; set; }
        public int OutOf { get; set; }
        public double? EarnedMarks { get; set; }
    }
}
