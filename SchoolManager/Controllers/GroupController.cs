using Microsoft.AspNetCore.Mvc;
using SchoolManager.Database;
using SchoolManager.Database.Entity;
using SchoolManager.Models.ViewModels.SchoolVM;
using SchoolManager.Resources.Interface;

namespace SchoolManager.Controllers
{
    public class GroupController : Controller
    {
        private readonly SchoolService _repository;

        public GroupController(ISchoolService repository)
        {
            _repository = (SchoolService)repository;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var courses = await _repository.CourseService.GetCoursesAsync();
            return View(new SchoolCreateGroupVM(courses));
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SchoolCreateGroupVM groupVM)
        {
            var courseId = groupVM.Group.CourseId ?? throw new Exception("CourseId cannot be null");

            groupVM.Group.Course = _repository.CourseService.GetCourse(courseId);

            if (ModelState.IsValid)
            {
                _repository.GroupService.AddGroupRecord(groupVM.Group);
                return RedirectToAction("Index", "School");
            }
            return View(new SchoolCreateGroupVM(_repository.CourseService.GetCourses()));
        }

        // GET
        [HttpGet("EditGroup/{groupId}")]
        public IActionResult Edit(Guid groupId)
        {
            return NotFound();
        }

        // PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GroupRecord newGroup)
        {
            return NotFound();
        }

        // DELETE
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var group = _repository.GroupService.GetGroup(id);

            if (group == null)
                return NotFound();

            if (group.Students.Count > 0)
            {
                TempData["ErrorMessage"] = "Неможливо видалити групу, оскільки вона має студентів.";
                return RedirectToAction("Index", "School");
            }

            _repository.GroupService.DeleteGroup(id);

            return RedirectToAction("Index", "School");
        }
    }
}
