using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TJ.Models
{
    public partial class Course
    {
        public Course()
        {
            StudentCourses = new HashSet<StudentCourse>();
        }

        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Code { get; set; } = null!;

        [InverseProperty("Course")]
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
