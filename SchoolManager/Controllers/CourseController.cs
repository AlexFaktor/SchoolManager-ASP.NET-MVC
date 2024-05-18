using Microsoft.AspNetCore.Mvc;
using SchoolManager.Database.Entity;
using SchoolManager.Database.Services;
using SchoolManager.Models.SchoolModels;
using SchoolManager.Models.ViewModels.CourseVM;
using SchoolManager.Resources.Interface;

namespace SchoolManager.Controllers
{
    public class CourseController : Controller
    {
        private readonly ISchoolService<CourseService, GroupService, StudentService> _service;

        public CourseController(ISchoolService<CourseService, GroupService, StudentService> repository)
        {
            _service = repository;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateCourseVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCourseVM courseVM)
        {
            if (_service.Course.Get(courseVM.Course.Name) != null)
            {
                ModelState.AddModelError("Course.Name", "Курс з таким ім'ям вже існує. Введіть інше ім'я.");
            }

            var course = new CourseRecord { Name = courseVM.Course.Name };

            if (ModelState.IsValid)
            {
                _service.Course.Add(course);
                return RedirectToAction("Index", "School");
            }
            return View(new CreateCourseVM());
        }

        [HttpGet("EditCourse/{courseId}")]
        public IActionResult Edit(Guid courseId)
        {
            var course = _service.Course.Get(courseId);

            if (course == null)
                return NotFound();

            var vm = new EditCourseVM(course);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPut(Guid id, EditCourseVM vm)
        {
            if (_service.Course.Get(vm.NewCourse.Name) != null)
            {
                ModelState.AddModelError("NewCourse.Name", "Курс з таким ім'ям вже існує. Введіть інше ім'я.");
            }

            var recordForEdit = _service.Course.Get(id);

            if (recordForEdit == null)
                return NotFound();

            vm.NewCourse.Id = id;
            vm.NewCourse.Groups = recordForEdit.Groups;

            if (_service.Course.Update(vm.NewCourse))
                return RedirectToAction("Index", "School");
            else
                return BadRequest();
        }

        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var course = _service.Course.Get(id);

            if (course == null)
                return NotFound();

            var isCourseHasGroup = _service.Group.GetAll(course.Id).Count > 0;

            if (isCourseHasGroup)
            {
                TempData["ErrorMessage"] = "Неможливо видалити курс, оскільки він має прикріплені групи.";
                return RedirectToAction("Index", "School"); ;
            }

            _service.Course.Delete(id);

            return RedirectToAction("Index", "School");
        }
    }
}
