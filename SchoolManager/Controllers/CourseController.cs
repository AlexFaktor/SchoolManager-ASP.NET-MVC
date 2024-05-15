using Microsoft.AspNetCore.Mvc;
using SchoolManager.Database;
using SchoolManager.Database.Entity;
using SchoolManager.Models.ViewModels.CourseVM;
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
                _repository.CourseService.AddCourseRecord(course);
                return RedirectToAction("Index", "School");
            }
            return View(new CreateCourseVM());
        }

        // GET
        [HttpGet("EditCourse/{courseId}")]
        public IActionResult Edit(Guid courseId)
        {
            var course = _repository.CourseService.GetCourse(courseId);

            if (course == null)
                return NotFound();

            var vm = new EditCourseVM(course);
            return View(vm);
        }

        // PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPut(Guid id,EditCourseVM vm)
        {
            var recordForEdit = _repository.CourseService.GetCourse(id);

            if (recordForEdit == null)
                return NotFound();

            vm.NewCourse.Id = id;
            vm.NewCourse.Groups = recordForEdit.Groups;

            if (_repository.CourseService.UpdateCourse(vm.NewCourse))
                return RedirectToAction("Index", "School");
            else
                return BadRequest();
        }

        // DELETE
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var course = _repository.CourseService.GetCourse(id);
            var isCourseHasGroup = _repository.GroupService.GetGroups(course!.Id).Count > 0;

            if (course == null)
                return NotFound();


            if (isCourseHasGroup)
            {
                TempData["ErrorMessage"] = "Неможливо видалити курс, оскільки він має прикріплені групи.";
                return RedirectToAction("Index", "School"); ;
            }

            _repository.CourseService.DeleteCourse(id);

            return RedirectToAction("Index", "School");
        }
    }
}
