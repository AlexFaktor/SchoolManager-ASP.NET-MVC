using Microsoft.AspNetCore.Mvc;
using SchoolManager.Database.Entity;
using SchoolManager.Database.Services;
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

        // GET
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateCourseVM());
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCourseVM courseVM)
        {
            var course = new CourseRecord { Name = courseVM.Course.Name };

            if (ModelState.IsValid)
            {
                _service.Course.Add(course);
                return RedirectToAction("Index", "School");
            }
            return View(new CreateCourseVM());
        }

        // GET
        [HttpGet("EditCourse/{courseId}")]
        public IActionResult Edit(Guid courseId)
        {
            var course = _service.Course.Get(courseId);

            if (course == null)
                return NotFound();

            var vm = new EditCourseVM(course);
            return View(vm);
        }

        // PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPut(Guid id, EditCourseVM vm)
        {
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

        // DELETE
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var course = _service.Course.Get(id);
            var isCourseHasGroup = _service.Group.GetAll(course!.Id).Count > 0;

            if (course == null)
                return NotFound();


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
