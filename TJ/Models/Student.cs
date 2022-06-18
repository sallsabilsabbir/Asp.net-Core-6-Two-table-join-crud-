using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TJ.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentCourses = new HashSet<StudentCourse>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Enrolled { get; set; }

        [InverseProperty("Student")]
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
        
    }
}
