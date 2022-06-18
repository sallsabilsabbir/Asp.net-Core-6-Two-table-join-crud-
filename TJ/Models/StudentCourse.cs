using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TJ.Models
{
    [Table("StudentCourse")]
    public partial class StudentCourse
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        [InverseProperty("StudentCourses")]
        public virtual Course Course { get; set; } = null!;
        [ForeignKey("StudentId")]
        [InverseProperty("StudentCourses")]
        public virtual Student Student { get; set; } = null!;

        
    }
}
