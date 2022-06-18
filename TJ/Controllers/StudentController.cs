using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TJ.Data;
using TJ.Models;
using TJ.ViewModel;

namespace TJ.Controllers
{
    public class StudentController : Controller
    {
     
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
              return _context.Students != null ? 
                          View(await _context.Students.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Students'  is null.");
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(x=>x.StudentCourses).ThenInclude(y=>y.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            var courses = _context.Courses.Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Id.ToString()
            }).ToList();

            CreateStudentViewModel vm = new CreateStudentViewModel();
            vm.Courses = courses;

            return View(vm);
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateStudentViewModel vm)
        {
            var student = new Student
            {
                Name = vm.Name,
                Enrolled = vm.Enrolled
            };
            var selectedCourse = vm.Courses.Where(x => x.Selected).Select(y => y.Value).ToList();
            foreach(var item in selectedCourse)
            {
                student.StudentCourses.Add(new StudentCourse()
                {
                    CourseId = int.Parse(item)
                });
            }
            _context.Students.Add(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(x => x.StudentCourses).Where(y => y.Id==id).FirstOrDefaultAsync();

            var selectedIds = student.StudentCourses.Select(x=>x.CourseId).ToList();

            var items = _context.Courses.Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Id.ToString(),
                Selected = selectedIds.Contains(x.Id)
            }).ToList();
            CreateStudentViewModel vm = new CreateStudentViewModel();
            vm.Name = student.Name;
            vm.Enrolled= student.Enrolled;
            vm.Courses = items;
            return View(vm);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateStudentViewModel vm)
        {
            // working map
            // j ts select korbo oita add hoiye jabe 
            // j ta unselect kora hobe oita remove hoiye jabe
            var student = _context.Students.Find(vm.Id);
            student.Name = vm.Name;
            student.Enrolled = vm.Enrolled;

            // agher j student ta store ase oita k select and nevigate korar jonno 
            var studentById = _context.Students.Include(x => x.StudentCourses).FirstOrDefault(y => y.Id == vm.Id);
            // agher student er infor mation edit er jonno j id ta ase seta k select kora
            var existingIds = studentById.StudentCourses.Select(x=> x.CourseId).ToList();
            // selected id or selected id update kora abong update data send through the id 
            var selectedIds = vm.Courses.Where(x=>x.Selected).Select(y=>y.Value).Select(int.Parse).ToList();
            //  select id to add new data
            var toAdd = selectedIds.Except(existingIds);
            // select id to remove data   
            var toRemove = existingIds.Except(selectedIds);
            
            // to update data or remove previous data and add new data son selected id
            student.StudentCourses = student.StudentCourses.Where(x=>!toRemove.Contains(x.CourseId)).ToList();
            
            // to add new datas 
            foreach(var item in toAdd)
            {
                //send data on midel table 
                student.StudentCourses.Add(new StudentCourse()
                {
                    // send updated data in the course id   
                    CourseId = item
                });
            }

            
            _context.Students.Update(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
