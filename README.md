# TJ

# course table:
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
  
  # student table
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
  # view model
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
  
  
  
