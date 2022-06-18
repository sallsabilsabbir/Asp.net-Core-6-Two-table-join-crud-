using Microsoft.AspNetCore.Mvc.Rendering;

namespace TJ.ViewModel
{
    public class CreateStudentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Enrolled { get; set; }

        public IList<SelectListItem> Courses { get; set; }
    }
}
