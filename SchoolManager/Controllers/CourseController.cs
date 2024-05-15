using Microsoft.AspNetCore.Mvc;
using SchoolManager.Database;
using SchoolManager.Database.Entity;
using SchoolManager.Models.ViewModels.SchoolVM;
using SchoolManager.Resources.Interface;

namespace SchoolManager.Controllers
{
    public class CourseController : Controller
    {
        private readonly SchoolService _repository;

        public CourseController(ISchoolService repository)
        {
            _repository = (SchoolService)repository;
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
        public IActionResult Create(SchoolCreateCourseVM courseVM)
        {
            var course = new CourseRecord { Name = courseVM.Course.Name };

            if (ModelState.IsValid)
            {
                _repository.CourseService.AddCourseRecord(course);
                return RedirectToAction("Index", "School");
            }
            return View(new SchoolCreateCourseVM());
        }

        // GET
        [HttpGet("EditCourse/{courseId}")]
        public IActionResult Edit(Guid courseId)
        {
            var course = _repository.CourseService.GetCourse(courseId);

            if (course == null)
                return NotFound();

            var vm = new SchoolEditCourseVM(course);
            return View(vm);
        }

        [HttpPut("EditCourse/{id:Guid}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, CourseRecord newRecord)
        {
            var recordForEdit = _repository.CourseService.GetCourse(id);

            if (recordForEdit == null)
                return NotFound();

            if (_repository.CourseService.UpdateCourse(newRecord))
                return Ok();
            else
                return BadRequest();
        }

        // DELETE
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var course = _repository.CourseService.GetCourse(id);

            if (course == null)
                return NotFound();


            if (course.Groups.Count > 0)
            {
                TempData["ErrorMessage"] = "Неможливо видалити курс, оскільки він має прикріплені групи.";
                return RedirectToAction("Index", "School"); ;
            }

            _repository.CourseService.DeleteCourse(id);

            return RedirectToAction("Index", "School");
        }
    }
}
