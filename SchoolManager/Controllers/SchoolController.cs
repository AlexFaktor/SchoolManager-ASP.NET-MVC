using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManager.Database.Database;
using SchoolManager.Database.Entity;
using SchoolManager.Models.ViewModels.SchoolVM;

namespace SchoolManager.Controllers
{
    public class SchoolController : Controller
    {
        private readonly SchoolDbContext _context;
        private readonly SchoolRepository _repository;

        public SchoolController(SchoolDbContext context)
        {
            _context = context;
            _repository = new SchoolRepository(context);
        }

        // GET: SchoolController
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var schoolIndexVM = new SchoolIndexVM();

            schoolIndexVM.Courses = await _context.Courses.ToListAsync();

            return View(schoolIndexVM);
        }

        // GET: SchoolController Course/
        [HttpGet("Index/{courseId}")]
        public async Task<IActionResult> Index(Guid courseId)
        {
            var schoolIndexVM = new SchoolIndexVM();

            schoolIndexVM.Courses = await _context.Courses.ToListAsync();
            schoolIndexVM.Groups = await _context.Groups.Where(g => g.CourseId == courseId).ToListAsync();

            schoolIndexVM.CourseId = courseId;

            return View(schoolIndexVM);
        }

        // GET: SchoolController Group/
        [HttpGet("Index/{courseId}/{groupId}")]
        public async Task<IActionResult> Index(Guid courseId, Guid groupId)
        {
            var schoolIndexVM = new SchoolIndexVM();

            schoolIndexVM.Courses = await _context.Courses.ToListAsync();
            schoolIndexVM.Groups = await _context.Groups.Where(g => g.CourseId == courseId).ToListAsync();
            schoolIndexVM.Students = await _context.Students.Where(s => s.GroupId == groupId).ToListAsync();

            schoolIndexVM.CourseId = courseId;
            schoolIndexVM.GroupId = groupId;

            return View(schoolIndexVM);
        }

        // GET: SchoolController Student/
        [HttpGet("Index/{courseId}/{groupId}/{studentId}")]
        public async Task<IActionResult> Index(Guid courseId, Guid groupId, Guid studentId)
        {
            var schoolIndexVM = new SchoolIndexVM();

            schoolIndexVM.Courses = await _context.Courses.ToListAsync();
            schoolIndexVM.Groups = await _context.Groups.Where(g => g.CourseId == courseId).ToListAsync();
            schoolIndexVM.Students = await _context.Students.Where(s => s.GroupId == groupId).ToListAsync();

            schoolIndexVM.CourseId = courseId;
            schoolIndexVM.GroupId = groupId;
            schoolIndexVM.StudentId = studentId;

            return View(schoolIndexVM);
        }

        // GET: SchoolController/CreateCourse
        public IActionResult CreateCourse()
        {
            return View(new SchoolCreateCourseVM());
        }
        // GET: SchoolController/CreateGroup
        public IActionResult CreateGroup()
        {
            return View(new SchoolCreateGroupVM(_context.Courses.ToList()));
        }
        // GET: SchoolController/CreateStudent
        public IActionResult CreateStudent()
        {
            return View(new SchoolCreateStudentVM(_context.Groups.ToList()));
        }

        // POST: SchoolController/CreateCourse
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse(SchoolCreateCourseVM courseVM)
        {
            var course = new CourseRecord { Name = courseVM.Course.Name };

            if (ModelState.IsValid)
            {
                _repository.CourseService.AddCourseRecord(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(new SchoolCreateCourseVM());
        }

        // POST: SchoolController/CreateGroup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGroup(SchoolCreateGroupVM groupVM)
        {
            groupVM.Group.Course = _context.Courses.First(c => c.Id == groupVM.Group.CourseId);

            if (ModelState.IsValid)
            {
                _repository.GroupService.AddGroupRecord(groupVM.Group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(new SchoolCreateGroupVM(_context.Courses.ToList()));
        }

        // POST: SchoolController/CreateGroup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudent(SchoolCreateStudentVM studentVM)
        {
            studentVM.Student.Group = _context.Groups.First(c => c.Id == studentVM.Student.GroupId);


            if (ModelState.IsValid)
            {
                _repository.StudentService.AddStudentRecord(studentVM.Student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(new SchoolCreateStudentVM(_context.Groups.ToList()));
        }

        // GET: SchoolController/EditCourse
        [HttpGet("EditCourse/{courseId}")]
        public IActionResult EditCourse(Guid courseId)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
                return NotFound();

            var vm = new SchoolEditCourseVM(course);
            return View(vm);
        }

        // PUT: SchoolController/EditCourse
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCoursePut(SchoolEditCourseVM vm)
        {
            var newRecord = vm.NewCourse;

            var recordForEdit = _context.Courses.FirstOrDefault(c => c.Id == newRecord.Id);
            if (recordForEdit == null)
                return NotFound();

            recordForEdit.Name = newRecord.Name;
            _context.SaveChanges();
            return Ok();
        }

        // GET: SchoolController/EditGroup
        [HttpGet("EditGroup/{groupId}")]
        public async Task<IActionResult> EditGroup(Guid groupId)
        {
            return NotFound();
        }

        // PUT: SchoolController/EditGroup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGroup(GroupRecord newGroup)
        {
            return NotFound();
        }

        // GET: SchoolController/EditStudent
        [HttpGet("EditStudent/{studentId}")]
        public async Task<IActionResult> EditStudent(Guid studentId)
        {
            return NotFound();
        }

        // PUT: SchoolController/EditStudent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStudent(StudentRecord newStudent)
        {
            return NotFound();
        }

        // DELETE: SchoolController/DeleteCourse
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var course = _context.Courses.Include(c => c.Groups).FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            if (course.Groups.Count > 0)
            {
                TempData["ErrorMessage"] = "Неможливо видалити курс, оскільки він має прикріплені групи.";
                return RedirectToAction(nameof(Index));
            }

            _repository.CourseService.DeleteCourse(id);
            _repository.DbSaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // DELETE: SchoolController/DeleteGroup
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var group = _context.Groups.Include(g => g.Students).FirstOrDefault(g => g.Id == id);

            if (group == null)
            {
                return NotFound();
            }

            if (group.Students.Count > 0)
            {
                TempData["ErrorMessage"] = "Неможливо видалити групу, оскільки вона має студентів.";
                return RedirectToAction(nameof(Index));
            }

            _repository.GroupService.DeleteGroup(id);
            _repository.DbSaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // DELETE: SchoolController/DeleteStudent
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var student = _context.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            _repository.StudentService.DeleteStudent(id);
            _repository.DbSaveChanges();

            return RedirectToAction(nameof(Index));
        }


        private bool CourseExists(Guid id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
