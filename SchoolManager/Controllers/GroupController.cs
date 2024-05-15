using Microsoft.AspNetCore.Mvc;
using SchoolManager.Database;
using SchoolManager.Database.Entity;
using SchoolManager.Models.SchoolModels;
using SchoolManager.Models.ViewModels.CourseVM;
using SchoolManager.Models.ViewModels.GroupVM;
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
            return View(new CreateGroupVM(courses));
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateGroupVM groupVM)
        {
            var courseId = groupVM.Group.CourseId ?? throw new Exception("CourseId cannot be null");

            groupVM.Group.Course = _repository.CourseService.GetCourse(courseId);

            if (ModelState.IsValid)
            {
                _repository.GroupService.AddGroupRecord(groupVM.Group);
                return RedirectToAction("Index", "School");
            }
            return View(new CreateGroupVM(_repository.CourseService.GetCourses()));
        }

        // GET
        [HttpGet("EditGroup/{groupId}")]
        public IActionResult Edit(Guid groupId)
        {
            var group = _repository.GroupService.GetGroup(groupId);

            if (group == null)
                return NotFound();

            var vm = new EditGroupVM(group);
            return View(vm);
        }

        // PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPut(Guid id, EditGroupVM vm)
        {
            var recordForEdit = _repository.GroupService.GetGroup(id);

            if (recordForEdit == null)
                return NotFound();

            vm.NewGroup.Id = id;
            vm.NewGroup.CourseId = recordForEdit.CourseId;
            vm.NewGroup.Course = recordForEdit.Course;
            vm.NewGroup.Students = recordForEdit.Students;

            if (_repository.GroupService.UpdateGroup(vm.NewGroup))
                return RedirectToAction("Index", "School");
            else
                return BadRequest();
        }

        // DELETE
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var group = _repository.GroupService.GetGroup(id);
            var isGroupHasStudents = _repository.StudentService.GetStudents(group!.Id).Count > 0;

            if (group == null)
                return NotFound();

            if (isGroupHasStudents)
            {
                TempData["ErrorMessage"] = "Неможливо видалити групу, оскільки вона має студентів.";
                return RedirectToAction("Index", "School");
            }

            _repository.GroupService.DeleteGroup(id);

            return RedirectToAction("Index", "School");
        }
    }
}
