using Microsoft.AspNetCore.Mvc;
using SchoolManager.Database;
using SchoolManager.Database.Entity;
using SchoolManager.Models.ViewModels.SchoolVM;
using SchoolManager.Resources.Interface;

namespace SchoolManager.Controllers
{
    public class CourseController : Controller
    {
        private readonly SchoolRepository _repository;

        public CourseController(ISchoolRepository repository)
        {
            _repository = (SchoolRepository)repository;
        }

        // GET
        [HttpGet]
        public IActionResult Create()
        {
            return View(new SchoolCreateCourseVM());
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SchoolCreateCourseVM courseVM)
        {
            var course = new CourseRecord { Name = courseVM.Course.Name };

            if (ModelState.IsValid)
            {
                _repository.CourseService.AddCourseRecord(course);
                _repository.DbSaveChangesAsync();
                return RedirectToAction("Index", "School");
            }
            return View(new SchoolCreateCourseVM());
        }

        // GET
        [HttpGet("EditCourse/{courseId}")]
        public async Task<IActionResult> Edit(Guid courseId)
        {
            var courses = await _repository.CourseService.GetCoursesAsync();

            var course = courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
                return NotFound();

            var vm = new SchoolEditCourseVM(course);
            return View(vm);
        }

        // PUT
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SchoolEditCourseVM vm)
        {
            var newRecord = vm.NewCourse;

            var courses = await _repository.CourseService.GetCoursesAsync();

            var recordForEdit = courses.FirstOrDefault(c => c.Id == newRecord.Id);
            if (recordForEdit == null)
                return NotFound();

            recordForEdit.Name = newRecord.Name;
            _repository.DbSaveChangesAsync();
            return Ok();
        }

        // DELETE
        public async Task<IActionResult> Delete(Guid id)
        {
            var courses = await _repository.CourseService.GetCoursesAsync();

            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var course = courses.FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            if (course.Groups.Count > 0)
            {
                TempData["ErrorMessage"] = "Неможливо видалити курс, оскільки він має прикріплені групи.";
                return RedirectToAction("Index", "School"); ;
            }

            _repository.CourseService.DeleteCourse(id);
            _repository.DbSaveChanges();

            return RedirectToAction("Index", "School");
        }


    }
}
